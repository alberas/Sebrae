using System.Data.SqlClient;

namespace Sebrae.Repositorio
{
    public class Conta
    {

        private static string getConnectionString()
        {
            ConfigurationManager cm = new ConfigurationManager();
            cm.GetConnectionString("SebraeDb");
            return "Server=(localdb)\\mssqllocaldb;Database=Sebrae;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public static Model.Conta RetornaConta(int id)
        {
            using (SqlConnection conn = new SqlConnection(getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM  conta WHERE id = @id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Model.Conta c =  new Model.Conta()
                        {
                            Id = Int16.Parse(reader.GetValue(0).ToString()),
                            Nome = reader.GetString(1),
                            Descricao = reader.GetString(2)

                        };

                        return c;
                    }
                    return new Model.Conta();
                }
            }
        }

        public static List<Model.Conta> RetornaContas()
        {
            using (SqlConnection conn = new SqlConnection(getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM  conta", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Model.Conta> contaList = new List<Model.Conta>(); 
                    while (reader.Read())
                    {

                        contaList.Add( new Model.Conta()
                        {
                            Id = Int16.Parse(reader.GetValue(0).ToString()),
                            Nome = reader.GetString(1),
                            Descricao = reader.GetString(2)

                        });
                    }

                    return contaList;
                }
            }
        }

        public static Model.Conta Inserir(Model.Conta conta)
        {
            using (SqlConnection conn = new SqlConnection(getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO conta(nome, descricao) VALUES(@nome, @descricao); SELECT SCOPE_IDENTITY()", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("nome", conta.Nome);
                    cmd.Parameters.AddWithValue("descricao", conta.Descricao);

                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    SqlDataReader reader = cmd.ExecuteReader();
                    conta.Id = id;

                    return conta;
                }
            }
        }

        public static Model.Conta Atualizar(Model.Conta conta)
        {
            using (SqlConnection conn = new SqlConnection(getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO conta(nome, descricao) VALUES(@nome, @descricao)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("nome", conta.Nome);
                    cmd.Parameters.AddWithValue("descricao", conta.Descricao);
                    cmd.ExecuteNonQuery();


                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT SCOPE_IDENTITY()";

                    int id = (int)cmd.ExecuteScalar();

                    SqlDataReader reader = cmd.ExecuteReader();
                    conta.Id = id;

                    return conta;
                }
            }
        }
        public static List<Model.Conta> Excluir(Model.Conta conta)
        {
            using (SqlConnection conn = new SqlConnection(getConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM  conta WHERE id = @id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("id", conta.Id);
                    cmd.ExecuteNonQuery();

                    return RetornaContas();
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Sebrae.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();

        private readonly ILogger<ContaController> _logger;

        public ContaController(ILogger<ContaController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "RetornaContas")]
        public IEnumerable<Model.Conta> GetContas()
        {
         
            return Repositorio.Conta.RetornaContas();
        
        }
        

        [HttpPost(Name = "CriarConta")]
        public Model.Conta Post([FromQuery] string nome, [FromQuery] string descricao)
        {

            Model.Conta c = new Model.Conta();
            c.Nome = nome;
            c.Descricao = descricao;

            Repositorio.Conta.Inserir(c);
            return c;

        }

        [HttpPut(Name = "AtualizarConta")]
        public Model.Conta Put([FromQuery] int id, [FromQuery] string nome, [FromQuery] string descricao)
        {

            Model.Conta c = new Model.Conta();
            c.Nome = nome;
            c.Descricao = descricao;

            Repositorio.Conta.Atualizar(c);
            return c;

        }

        [HttpDelete(Name = "ExcluirConta")]
        public Model.Conta Delete([FromQuery] int id, [FromQuery] string nome, [FromQuery] string descricao)
        {

            Model.Conta c = new Model.Conta();
            c.Nome = nome;
            c.Descricao = descricao;

            Repositorio.Conta.Excluir(c);
            return c;

        }

    }
}
using Microsoft.AspNetCore.Mvc;
using RestASPNET.Model;
using RestASPNET.Services.Implementations;

namespace RestASPNET.Controllers
{
    /* Mapeia as requisições de http://localhost:{porta}/api/persons/v1/
    Por padrão o ASP.NET Core mapeia todas as classes que extendem Controller
    pegando a primeira parte do nome da classe em lower case [Person]Controller
    e expõe como endpoint REST
    */


    //Versionamento de API
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //Declaração do serviço usado
        private IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }



        //Mapeia as requisições GET para http://localhost:{porta}/api/persons/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/persons/v1/{id}
        //recebendo um ID como no Path da requisição
        //Get com parâmetros para o FindById --> Busca Por ID
        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
            
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            if (person == null) return NotFound();
            return new ObjectResult(_personService.Create(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Person person)
        {
            if (person == null) return NotFound();
            return new ObjectResult(_personService.Update(person));
        }

        //Mapeia as requisições DELETE para http://localhost:{porta}/api/persons/v1/{id}
        //recebendo um ID como no Path da requisição
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController()
        {
        }

        /*
        GET all action
          -Responds only to the HTTP GET verb, as denoted by the [HttpGet] attribute.
          -Queries the service for all pizza and automatically returns data with a Content-Type of application/json.
        */
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
          PizzaService.GetAll();

        /*
        GET by Id action / retrieve single pizza
          -Responds only to the HTTP GET verb, as denoted by the [HttpGet] attribute.
          -Requires that the id parameter's value is included in the URL segment after pizza/. Remember, the /pizza pattern was defined by the controller-level [Route] attribute.
          -Queries the database for a pizza matching the provided id parameter.
        */
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if(pizza == null)
                return NotFound();

            return pizza;
        }

        /*
        POST action
        */


        /*
        PUT action
        */

        /*
        DELETE action
        */
    }
}
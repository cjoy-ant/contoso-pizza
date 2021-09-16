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

        /*ASP.NET Core action result	HTTP status code	Description
          Ok is implied	              200	              A product matching the provided id parameter exists in the in-memory cache.
                                                        The product is included in the response body in the media type as defined in the accept HTTP request header (JSON by default).
          NotFound	                  404	              A product matching the provided id parameter doesn't exist in the in-memory cache.*/


        /*
        POST action
          -By passing the item (in this example, a Pizza) into the method as a parameter, 
          -ASP.NET Core will automatically convert any application/json that is sent to the endpoint into a populated .NET Pizza object.
          -Responds only to the HTTP POST verb, as denoted by the [HttpPost] attribute.
          -Inserts the request body's Pizza object into the in-memory cache.
        */
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {            
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        /*ASP.NET Core action result	HTTP status code	Description
          CreatedAtAction	            201	              The pizza was added to the in-memory cache.
                                                        The pizza is included in the response body in the media type as defined in the accept HTTP request header (JSON by default).
          BadRequest is implied	       400	            The request body's pizza object is invalid.*/

        /*
        PUT action
          -Responds only to the HTTP PUT verb, as denoted by the [HttpPut] attribute.
          -Requires that the id parameter's value is included in the URL segment after pizza/.
          -Returns IActionResult because the ActionResult return type isn't known until runtime. 
          -The BadRequest, NotFound, and NoContent methods return BadRequestResult, NotFoundResult, and NoContentResult types, respectively.
        */
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if(existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);           

            return NoContent();
        }
        /*ASP.NET Core action result	HTTP status code	Description
          NoContent	                  204	              The pizza was updated in the in-memory cache.
          BadRequest	                400	              The request body's Id value doesn't match the route's id value.
          BadRequest is implied	      400	              The request body's Pizza object is invalid.*/
        

        /*
        DELETE action
          -Responds only to the HTTP DELETE verb, as denoted by the [HttpDelete] attribute.
          -Requires that id parameter's value is included in the URL segment after pizza/.
          -Returns IActionResult because the ActionResult return type isn't known until runtime. 
          -The NotFound and NoContent methods return NotFoundResult and NoContentResult types, respectively.
          -Queries the in-memory cache for a pizza matching the provided id parameter.
        */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }
        /*ASP.NET Core action result	HTTP status code	Description
          NoContent	                  204	              The pizza was deleted from the in-memory cache.
          NotFound	                  404	              A pizza matching the provided id parameter doesn't exist in the in-memory.*/


    }
}

/*
CRUD ACTIONS IN ASP.NET CORE

HTTP action verb	  CRUD operation	  ASP.NET Core attribute
GET	                Read	            [HttpGet]
POST	              Create	          [HttpPost]
PUT	                Update	          [HttpPut]
DELETE	            Delete	          [HttpDelete]
*/
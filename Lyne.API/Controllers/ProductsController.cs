using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

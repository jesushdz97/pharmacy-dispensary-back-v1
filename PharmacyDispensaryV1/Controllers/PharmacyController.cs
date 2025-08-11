using Microsoft.AspNetCore.Mvc;
using PharmacyDispensaryV1.Application;
using PharmacyDispensaryV1.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyDispensaryV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController(IPharmacyService pharmacyService) : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService = pharmacyService;

        // GET: api/<PharmacyController>
        [HttpGet]
        public async Task<IEnumerable<Pharmacy>> Get()
        {
            return await _pharmacyService.List();
        }

        // GET api/<PharmacyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PharmacyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PharmacyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PharmacyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

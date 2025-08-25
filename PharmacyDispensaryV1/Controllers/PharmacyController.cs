using Microsoft.AspNetCore.Mvc;
using PharmacyDispensaryV1.Application;
using PharmacyDispensaryV1.Data.Dto.Request;
using PharmacyDispensaryV1.Data.Entities;
using PharmacyDispensaryV1.Data.Mappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyDispensaryV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController(IPharmacyService pharmacyService, ILogger<PharmacyController> logger) : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService = pharmacyService;


        // GET: api/<PharmacyController>
        [HttpGet("test")]
        public ActionResult Test()
        {
            logger.LogInformation("TEST");
            return Ok();
        }


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
        public async Task<Pharmacy> Post([FromBody] PharmacyCreate request)
        {
            var pharmacy = request.ToPharmacyCreate();
            await _pharmacyService.Save(pharmacy);
            return pharmacy;
        }

        // PUT api/<PharmacyController>/5
        [HttpPut]
        public void Put([FromBody] PharmacyUpdate request)
        {
            var pharmacy = request.ToPharmacyUpdate();
            _pharmacyService.Update(pharmacy);
        }

        // DELETE api/<PharmacyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

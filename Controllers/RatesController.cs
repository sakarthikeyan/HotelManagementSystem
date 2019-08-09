using System.Collections.Generic;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using SunmathiTech.HRMS.Models;
using SunmathiTech.HRMS.DAL.MongoDB;

namespace SunmathiTech.HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : HMSApiControllerBase
    {
        private readonly RateService _service;

        public RatesController(RateService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ExpandoObject> Get(int? offset, int? limit, string filter)
        {
            if (offset != null)
                _offset = (int)offset;
            if (limit != null)
                _limit = (int)limit;
            _filter = filter;
            return _service.Get(_limit, _offset, _filter);
        }

        [HttpGet("{id:length(24)}", Name = "GetRate")]
        public ActionResult<Rate> Get(string id)
        {
            var items = _service.Get(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        [HttpPost]
        public ActionResult<Rate> Create(Rate items)
        {
            _service.Create(items);

            return CreatedAtRoute("GetRate", new { id = items.Id.ToString() }, items);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Rate item)
        {
            var items = _service.Get(id);

            if (items == null)
            {
                return NotFound();
            }

            _service.Update(id, item);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var items = _service.Get(id);

            if (items == null)
            {
                return NotFound();
            }

            _service.Remove(items.Id);

            return NoContent();
        }
    }
}
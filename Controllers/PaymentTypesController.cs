using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using SunmathiTech.HRMS.Models;
using SunmathiTech.HRMS.DAL.MongoDB;

namespace SunmathiTech.HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypesController : HMSApiControllerBase
    {
        private readonly PaymentTypeService _service;

        public PaymentTypesController(PaymentTypeService service)
        {
            try
            {
                _service = service;
            }
            catch (Exception e)
            {
                //Do nothing.
                throw e;
            }
        }

        [HttpGet]
        public ActionResult<ExpandoObject> Get(int? offset, int? limit)
        {
            if (offset != null)
                _offset = (int)offset;
            if (limit != null)
                _limit = (int)limit;
            return _service.Get(_limit, _offset);
        }

        [HttpGet("{id:length(24)}", Name = "GetPaymentType")]
        public ActionResult<PaymentType> Get(string id)
        {
            var items = _service.Get(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        [HttpPost]
        public ActionResult<PaymentType> Create(PaymentType items)
        {
            _service.Create(items);

            return CreatedAtRoute("GetPaymentType", new { id = items.Id.ToString() }, items);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, PaymentType item)
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
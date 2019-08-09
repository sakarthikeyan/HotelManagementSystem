using System.Collections.Generic;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using SunmathiTech.HRMS.Models;
using SunmathiTech.HRMS.DAL.MongoDB;

namespace SunmathiTech.HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipsController : HMSApiControllerBase
    {
        private readonly RelationshipService _service;

        public RelationshipsController(RelationshipService service)
        {
            _service = service;
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

        [HttpGet("{id:length(24)}", Name = "GetRelationship")]
        public ActionResult<Relationship> Get(string id)
        {
            var items = _service.Get(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        [HttpPost]
        public ActionResult<Relationship> Create(Relationship items)
        {
            _service.Create(items);

            return CreatedAtRoute("GetRelationship", new { id = items.Id.ToString() }, items);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Relationship item)
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
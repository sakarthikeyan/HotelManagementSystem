using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SunmathiTech.HRMS.Models;
using SunmathiTech.HRMS.DAL.MongoDB;

namespace SunmathiTech.HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly BaseMongoDBService _baseMongoDBService;

        public GenericController(CategoryService categoriesService)
        {
            var config = GlobalConfiguration.Configuration;
            config.Services.Replace(typeof(IHttpControllerSelector), new MyControllerSelector(config));
            _baseMongoDBService = new BaseMongoDBService();
        }

        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            return _baseMongoDBService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetCategory")]
        public ActionResult<Category> Get(string id)
        {
            var categories = _baseMongoDBService.Get(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        [HttpPost]
        public ActionResult<Category> Create(Category categories)
        {
            _baseMongoDBService.Create(categories);

            return CreatedAtRoute("GetCategory", new { id = categories.Id.ToString() }, categories);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Category category)
        {
            var categories = _baseMongoDBService.Get(id);

            if (categories == null)
            {
                return NotFound();
            }

            _baseMongoDBService.Update(id, category);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var categories = _baseMongoDBService.Get(id);

            if (categories == null)
            {
                return NotFound();
            }

            _baseMongoDBService.Remove(categories.Id);

            return NoContent();
        }
    }
}
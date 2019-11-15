using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MotionsContext _context;

        public CategoryController(MotionsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<CategoryDto>> GetCategories()
        {
            var response = new List<CategoryDto>();

            _context.MotionCategories.ToList().ForEach((MotionCategory c) => response.Add(new CategoryDto(c)));

            return response;
        }
    }
}
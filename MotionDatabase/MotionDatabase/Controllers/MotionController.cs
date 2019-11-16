﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionController
    {
        private readonly MotionsContext _context;

        public MotionController(MotionsContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("search")]
        public ActionResult<MotionSearchResultDto> Search(MotionSearchDto request)
        {
            var query = _context.Motions.AsQueryable();

            if (request.Categories != null && request.Categories.Count > 0)
            {
                query = query.Where(m => request.Categories.Contains(m.CategoryId));
            }

            if (request.ExplicitMode == 0)
            {
                query = query.Where(m => m.IsExplicit == false);
            } 
            else if (request.ExplicitMode == 2)
            {
                query = query.Where(m => m.IsExplicit == true);
            }

            if (request.Tags != null && request.Tags.Count > 0)
            {
                if (request.AllTags)
                {
                    query = query.Where(m => request.Tags.All(t => m.Tags.Any(mt => mt.MotionTagId == t)));
                } 
                else
                {
                    query = query.Where(m => request.Tags.Any(t => m.Tags.Any(mt => mt.MotionTagId == t)));
                }
            }

            query = query
                .Where(m => m.State == MotionState.Approved)
                .Include(m => m.Category)
                .Include(m => m.Tags)
                    .ThenInclude(t => t.MotionTag);


            var result = new MotionSearchResultDto(query.ToList())
            {
                Query = request
            };
            return result;
        }
    }
}

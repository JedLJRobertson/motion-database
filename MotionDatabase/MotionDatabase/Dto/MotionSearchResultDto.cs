using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchResultDto
    {
        public MotionSearchDto Query { get; set; }
        public List<MotionSearchItemDto> Results { get; set; }

        public MotionSearchResultDto(List<Motion> motions)
        {
            Results = new List<MotionSearchItemDto>();

            foreach (var motion in motions)
            {
                Results.Add(new MotionSearchItemDto(motion));
            }
        }
    }
}

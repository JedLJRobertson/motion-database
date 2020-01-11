using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchDto
    {
        public List<int> Categories { get; set; }
        public List<MotionDifficulty> Difficulties { get; set; }
        public List<int> Tags { get; set; }
        public bool AllTags { get; set; }
        // 0 = All ages, 1 = include explicit motions, 2 = explicit only
        public int SuitabilityMode { get; set; }
        public bool SuitabilityIncludeUncategorised { get; set; }
        [Range(0, int.MaxValue)]
        public int? StartFrom { get; set; }
        [Range(1, 50)]
        public int? NumberResults { get; set; }
    }
}

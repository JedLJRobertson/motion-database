using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchDto
    {
        public List<int> Categories { get; set; }
        public List<int> Tags { get; set; }
        public bool AllTags { get; set; }
        // 0 = All ages, 1 = include explicit motions, 2 = explicit only
        public int ExplicitMode { get; set; }
    }
}

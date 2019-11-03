using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchDto
    {
        public int? CategoryId { get; set; }
        public List<int> Tags { get; set; }
        public bool AllTags { get; set; }
    }
}

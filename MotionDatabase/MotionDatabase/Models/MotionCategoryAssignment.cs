using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Models
{
    public class MotionCategoryAssignment
    {
        public int MotionId { get; set; }
        public Motion Motion { get; set; }
        public int CategoryId { get; set; }
        public MotionCategory Category { get; set; }
    }
}

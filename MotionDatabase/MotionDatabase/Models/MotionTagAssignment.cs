using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabase.Models
{
    public class MotionTagAssignment
    {
        public int MotionId { get; set; }
        public int MotionTagId { get; set; }

        public Motion Motion { get; set; }
        public MotionTag MotionTag { get; set; }
    }
}

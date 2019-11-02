using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabase.Models
{
    public class MotionTagRelated
    {
        public int TagAId { get; set; }
        public MotionTag TagA { get; set; }

        public int TagBId { get; set; }
        public MotionTag TagB { get; set; }
    }
}

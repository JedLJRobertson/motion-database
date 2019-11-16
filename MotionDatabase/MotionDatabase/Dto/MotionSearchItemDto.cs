using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool IsExplicit { get; set; }
        public IList<TagDto> Tags { get; set; }
    }
}

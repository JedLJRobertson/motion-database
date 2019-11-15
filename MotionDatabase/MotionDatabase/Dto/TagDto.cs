using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class TagDto
    {
        public TagDto(MotionTag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
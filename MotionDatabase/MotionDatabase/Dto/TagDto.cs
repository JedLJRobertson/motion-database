using System.Collections.Generic;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class TagDto
    {
        public TagDto(MotionTag tag)
        {
            Id = tag.Id;
            Name = tag.Name;

            Synonyms = new List<string>();
            if (tag.MotionTagSynonyms != null)
            {
                foreach (var syn in tag.MotionTagSynonyms)
                {
                    Synonyms.Add(syn.Name);
                }
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Synonyms { get; set; }
    }
}
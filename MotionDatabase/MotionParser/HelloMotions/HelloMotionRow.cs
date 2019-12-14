using System;
using System.Collections.Generic;
using System.Text;

namespace MotionParser.HelloMotions
{
    class HelloMotionRow
    {
        public string City { get; set; }
        public string Country { get; set; }
        public int International { get; set; }
        public string Tournament { get; set; }
        public string Round { get; set; }
        public string Motion { get; set; }
        public string Infoslide { get; set; }

        public string Topic_Area_1 { get; set; }
        public string Topic_Area_2 { get; set; }
        public string Topic_Area_3 { get; set; }

        public List<string> GetCategories()
        {
            var result = new List<string>();

            if (Topic_Area_1 != null && Topic_Area_1.Length > 1)
            {
                result.Add(Topic_Area_1);
            }
            if (Topic_Area_2 != null && Topic_Area_2.Length > 1)
            {
                result.Add(Topic_Area_2);
            }
            if (Topic_Area_3 != null && Topic_Area_3.Length > 1)
            {
                result.Add(Topic_Area_3);
            }
            return result;
        }

        public string Topic_Area_Specific_1 { get; set; }
        public string Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class NewLog
    {
        public int LogID { get; set; }
        public int PersonalLibraryID { get; set; }
        public string FormatType { get; set; }
        public int TotalTime { get; set; }
        public string Note { get; set; }
        public int IsCompleted { get; set; } = 0;
    }
}

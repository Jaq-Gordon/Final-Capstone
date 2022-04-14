using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public long ISBN { get; set; }
      
        
    }

    public class PersonalBook : Book
    {
        public PersonalBook() : base() { }
        public int PersonalLibraryId { get; set; }
        public int IsCompleted { get; set; }

        public string IsCompletedStr
        {
            get
            {
                if (this.IsCompleted == 1)
                {
                    return "Finished";
                }
                else
                {
                    return "Immersed";
                }
            }
        }

    }
    public class BookHistoryObj : PersonalBook
    {
        public BookHistoryObj() : base() { }
        public int TotalTime { get; set; }
       
        
            
    }
}

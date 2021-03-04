using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PricePerDay { get; set; }
        public int Availability { get; set; }
    }
}

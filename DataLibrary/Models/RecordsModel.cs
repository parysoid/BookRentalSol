using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class RecordsModel
    {
        public int Id { get; set; }
        public int RecordNumber { get; set; }
        public int BookId { get; set; }
        public int PricePerDay { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string RentStart { get; set; }
        public string RentEnd { get; set; }
        public int Invoiced { get; set; }
        public string InvoicedDate { get; set; }

    }
}

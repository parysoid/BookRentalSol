using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static DataLibrary.BusinessLogic.RecordsProcessor;
using System.Configuration;
using System.Data;
using DataLibrary.DataAccess;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;
using BookRentalJOB.Models;



namespace BookRentalJOB
{
    class Program
    {        
        private static string _con = SqlDataAccess.GetConnectionString();
        public static void Main()
        {
            InvoiceRents();

            var mapper = new ModelToTableMapper<BookModel>();
            mapper.AddMapping(c => c.Availability, "Availability");
                      
            using (var dep = new SqlTableDependency<BookModel>(_con, "Book", mapper: mapper)) 
            {
                dep.OnChanged += Changed;
                dep.Start();

                Console.WriteLine("Pro ukončení stiskněte libovolnou klávesu");
                Console.ReadKey();

                dep.Stop();
            }
        }

        public static void Changed(object sender, RecordChangedEventArgs<BookModel> e)
        {
            var changedEntity = e.Entity;

            Console.WriteLine("DML operation: " + e.ChangeType);
            Console.WriteLine("ID: " + changedEntity.Id);
            Console.WriteLine("Name: " + changedEntity.Availability);

            CheckBooks(changedEntity.Availability, changedEntity.Id);           
        }

        private static void CheckBooks(int availability, int bookId)
        {
            if (availability == 2)
            {
                int recordsCreated = CreateRecord(bookId);
                Console.WriteLine("Byl vytvořen " + recordsCreated + " záznam o půjčení knihy - " + bookId);
            }

            if (availability == 3)
            {
                int recordsEnded = EndRecord(bookId);
                Console.WriteLine("Byl ukončen " + recordsEnded + " záznam o vrácení knihy - " + bookId);
            }
        }

        private static void InvoiceRents()
        {
            var data = LoadInvoicedRecords();

            foreach (var row in data)
            {
                List<RecordsModel> record = new List<RecordsModel>();

                record.Add(new RecordsModel
                {
                    Id = row.Id,
                    RecordNumber = row.RecordNumber,
                    BookId = row.Id,
                    Title = row.Title,
                    Author = row.Author,
                    PricePerDay = row.PricePerDay,
                    RentStart = DateTime.Parse(row.RentStart).ToString("dd.MM.yyyy H:m:s"),
                    RentEnd = DateTime.Parse(row.RentEnd).ToString("dd.MM.yyyy H:m:s"),
                    Invoiced = row.Invoiced,
                    Price = GetPrice(row.PricePerDay, row.RentStart, row.RentEnd)
                }); ; ;
                
                Sender sender = new Sender();
                sender.ProcessAsync(record);

                InvoiceRecord(row.Id);

                Console.WriteLine("Záznam číslo: " + row.RecordNumber + " prošel fakturací a faktura byla odeslána emailem zákazníkovi.");
            }
        }
        
        private static int GetPrice(int pricePerDay, string rentStart, string rentEnd)
        {           
            var start = DateTime.Parse(rentStart);
            var end = DateTime.Parse(rentEnd);
                      
            return ((end - start).Days + 1) * pricePerDay;            
        }
        
    }

}


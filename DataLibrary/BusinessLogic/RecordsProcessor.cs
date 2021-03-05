using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using static DataLibrary.BusinessLogic.BookProcessor;


namespace DataLibrary.BusinessLogic
{    
    public static class RecordsProcessor
    {
        public static int CreateRecord (int bookId)
        {
            RecordsModel data = new RecordsModel
            {
                RecordNumber = new Random().Next(100000000, 999999999),
                BookId = bookId,
                RentStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")                
            };

            string sql = @"INSERT INTO dbo.Records (RecordNumber, BookId, RentStart)
                           VALUES (@RecordNumber, @BookId, @RentStart);";

            /* nastevení knihy do stavu půjčená */
            SetAvailability(bookId,0);

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int EndRecord(int bookId)
        {
            RecordsModel data = new RecordsModel
            {
                RentEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                BookId = bookId
            };

            string sql = @"UPDATE dbo.Records
                           SET RentEnd = @RentEnd
                           WHERE BookId = @BookId;";

            /* nastevení knihy zpět do stavu volná */
            SetAvailability(bookId, 1);

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<RecordsModel> LoadInvoicedRecords()
        {
            string sql = @"SELECT Records.Id, Records.RecordNumber, Records.BookId, Records.RentStart, Records.RentEnd,
                                  Book.PricePerDay, Book.Title, Book.Author
                           FROM dbo.Records
                           INNER JOIN Book ON Records.BookId = Book.Id
                           WHERE Invoiced = 0
                           AND RentEnd IS NOT NULL;";
            return SqlDataAccess.LoadData<RecordsModel>(sql);
        }

        public static int InvoiceRecord(int id)
        {
            RecordsModel data = new RecordsModel
            {
                Id = id,
                InvoicedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            string sql = @"UPDATE dbo.Records
                           SET Invoiced = 1, InvoicedDate = @InvoicedDate
                           WHERE Id = @Id";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}

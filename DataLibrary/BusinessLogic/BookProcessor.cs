using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataLibrary.BusinessLogic
{
    public static class BookProcessor
    {
        public static int CreateBook(string title, string author, int pricePerDay)
        {
            BookModel data = new BookModel
            {
                BookId = new Random().Next(100000, 999999),
                Title = title,
                Author = author,
                PricePerDay = pricePerDay,                
            };

            string sql = @"INSERT INTO dbo.Book (BookId, Title, Author, PricePerDay)
                           VALUES (@BookId, @Title, @Author, @PricePerDay);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<BookModel> LoadBooks()
        {
            string sql = @"SELECT Id, BookId, Title, Author, PricePerDay, Availability
                           FROM dbo.Book;";
            return SqlDataAccess.LoadData<BookModel>(sql);
        }

        public static List<BookModel> LoadRentedBooks()
        {
            string sql = @"SELECT Id, BookId, Title, Author, PricePerDay, Availability
                           FROM dbo.Book
                           WHERE Availability IN (0,2,3);";
            return SqlDataAccess.LoadData<BookModel>(sql);
        }

        public static int SetAvailability(int bookId, int availability)
        {
            BookModel data = new BookModel
            {
                BookId = bookId,
                Availability = availability
            };

            string sql = @"UPDATE dbo.book
                           SET Availability = @Availability
                           WHERE Id = @BookId";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}

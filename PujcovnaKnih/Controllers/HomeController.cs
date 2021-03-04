using PujcovnaKnih.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static DataLibrary.BusinessLogic.BookProcessor;
using PagedList.Mvc;
using PagedList;

namespace PujcovnaKnih.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ViewBooks(int? i, string search)
        {
            ViewBag.Message = "Seznam knih";

            var data = LoadBooks();
            List<BookModel> books = new List<BookModel>();

            foreach (var row in data)
            {
                books.Add(new BookModel
                {
                    Id = row.Id,
                    BookId = row.BookId,
                    Title = row.Title,
                    Author = row.Author,
                    PricePerDay = row.PricePerDay,
                    Availability = row.Availability
                }); ;
            }

            return View(books.Where(x => x.Title.Contains(search ?? "") || search == null).ToPagedList(i ?? 1, 10));
        }

        public ActionResult AddBook()
        {
            ViewBag.Message = "Přidání knihy";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateBook(                    
                    model.Title,
                    model.Author,
                    model.PricePerDay);

                return RedirectToAction("ViewBooks");
            }

            return View();
        }

        public ActionResult ViewRentedBooks(int? i, string search)
        {
            ViewBag.Message = "Seznam půjčených knih";

            var data = LoadRentedBooks();
            List<BookModel> books = new List<BookModel>();

            foreach (var row in data)
            {
                books.Add(new BookModel
                {
                    Id = row.Id,
                    BookId = row.BookId,
                    Title = row.Title,
                    Author = row.Author,
                    PricePerDay = row.PricePerDay,
                    Availability = row.Availability
                });
            }

            return View(books.Where(x => x.Title.Contains(search ?? "") || search == null).ToPagedList(i ?? 1, 10));
        }

        public ActionResult Rent(int id)
        {
            int recordsUpdated = SetAvailability(id,2);

            return RedirectToAction("ViewBooks");

        }

        public ActionResult Return(int id)
        {
            /* Nastavení stavu knihy na žádost o vrácení */
            int recordsUpdated = SetAvailability(id,3);

            return RedirectToAction("ViewRentedBooks");

        }
    }
}
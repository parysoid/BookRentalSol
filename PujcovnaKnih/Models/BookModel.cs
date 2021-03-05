using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PujcovnaKnih.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Display(Name = "Kniha ID")]
        public int BookId { get; set; }

        [Display(Name = "Název knihy")]
        [Required(ErrorMessage = "Musíte vyplnit název knihy.")]
        public string Title { get; set; }

        [Display(Name = "Autor knihy")]
        [Required(ErrorMessage = "Musíte vyplnit autora knihy.")]
        public string Author { get; set; }

        [Display(Name = "Cena za půjčení / den")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Musíte vyplnit cenu půjčení.")]
        public int PricePerDay { get; set; }

        [Display(Name = "Stav knihy")]
        [Required(ErrorMessage = "Musíte zaškrtnout dostupnost knihy.")]
        public int Availability { get; set; }
    }
}
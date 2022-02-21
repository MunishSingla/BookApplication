using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public List<String> Author { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }
    }
}
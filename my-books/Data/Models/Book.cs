﻿using System;
using System.Collections.Generic;

namespace my_books.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        //Navigation properties
        public int PublisherId { get; set; } //define the FK. Knows it is the primary key for Publisher
        public Publisher Publisher { get; set; }
        public List<Book_Author> Book_Authors { get; set; }



    }
}

﻿using System.Collections.Generic;

namespace my_books.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }

    }
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> bookAuthors { get; set; }
    }

    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}

﻿using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Linq;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM book)
        {
            var _author = new Author()
            {
                FullName = book.FullName,
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                Fullname = n.FullName,
                BookTitles = n.Book_Authors.Select(b => b.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
    }
}

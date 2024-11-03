using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using Newtonsoft.Json.Bson;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using my_books.Data.Services;
using System.Linq;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName : "BookDbTest")
            .Options;

        AppDbContext context;
        PublishersService publishersService;

        [OneTimeSetUp] //set up for ex the db only one time and use it in as many methods, then dispose
        public void Setup()
        {//Here we write the code before running any unit test method
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();

            //set up service: it accepts the context as parameter
            publishersService = new PublishersService(context);

        }

        [Test, Order(1)] //decorator for test methods
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("","",null);

            Assert.That(result.Count, Is.EqualTo(5)); // we have 5 publishers, page size
            Assert.AreEqual(result.Count, 5);
        }

        [Test, Order(2)] 
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("", "", 2);

            Assert.That(result.Count, Is.EqualTo(1)); 
        }
        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("", "3", null);

            Assert.That(result.Count, Is.EqualTo(1)); 
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3")); 

        }

        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publishersService.GetAllPublishers("name_desc", "", null);

            Assert.That(result.Count, Is.EqualTo(5)); 
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));

        }

        [Test, Order(5)]
        public void GetPublisherById_WithResponse_Test()
        {
            var result = publishersService.GetPublisherById(1);
            Assert.That(result.Id, Is.EqualTo(1)); 
            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
        }
        [Test, Order(6)]
        public void GetPublisherById_WithoutResponse_Test()
        {
            var result = publishersService.GetPublisherById(9);
            Assert.That(result, Is.Null); 
        }

        [Test, Order(7)]
        public void AddPublihser_WithException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "123 With Exception"
            };
            Assert.That(() => publishersService.AddPublisher(newPublisher), Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name starts with number"));
        }
        [Test, Order(8)]
        public void AddPublihser_WithoutException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "Without Exception"
            };
            var result = publishersService.AddPublisher(newPublisher);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Does.StartWith("Without"));
            Assert.That(result?.Id, Is.Not.Null);
        }

        [Test, Order(9)]
        public void GetPublisherData_Test()
        {
            var result = publishersService.GetPublisherData(1);
            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
            Assert.That(result.bookAuthors, Is.Not.Null);
            Assert.That(result.bookAuthors.Count, Is.GreaterThan(0));

            var firstBookName = result.bookAuthors.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            Assert.That(firstBookName, Is.EqualTo("Book 1 Title"));

        }

        [OneTimeTearDown]
        public void CleanUp()
        { 
            context.Database.EnsureDeleted();
        }   
        
        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);


            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            context.Book_Authors.AddRange(books_authors);


            context.SaveChanges();
        }
    }
}
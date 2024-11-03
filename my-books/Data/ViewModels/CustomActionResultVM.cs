using my_books.Data.Models;
using System;

namespace my_books.Data.ViewModels
{
    public class CustomActionResultVM
    {
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; } //if the same for all, just 'object'
    }
}

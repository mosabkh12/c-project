using Library.Models;
using System.Collections.Generic;

namespace Library.ViewModels
{
    public class BooksAndPaymentsViewModel
            public IEnumerable<Book> Books { get; set; }
        public IEnumerable<payment2> Payments { get; set; }

        public Book purchasedProduct { get; set; }
        public List<Book> purchased { get; set; }
    }
}
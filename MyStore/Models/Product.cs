using System;
using System.Collections.Generic;

namespace MyStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime? Date { get; set; }
        public decimal Price { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
        public int State { get; set; }
        public ICollection<RegisterViewModel> User { get; set; }
    }
}
using MyStore.Models;
using System.Collections.Generic;

namespace MyStore.ViewModels
{
    public class UserProductsViewModel
    {
        public List<Product> products { get; set; }
        public int TotalPrice { get; set; }
    }
}
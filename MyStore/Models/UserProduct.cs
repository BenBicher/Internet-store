using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Models
{
    public class UserProduct
    {
        public int Id { get; set; }
        public int ProductRefId { get; set; }
        [ForeignKey("ProductRefId")]
        public Product Product { get; set; }
        public string UserRefId { get; set; }
        [ForeignKey("UserRefId")]
        public RegisterViewModel User { get; set; }
    }
}
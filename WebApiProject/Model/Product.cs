using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiProject.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string Img { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }

    }
}

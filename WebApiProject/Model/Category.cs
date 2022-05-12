using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApiProject.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<Product> product { set; get; }
    }
}

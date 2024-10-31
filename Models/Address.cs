using System;

namespace FluentAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Student> Students { get; set; } // One-to-many relationship
        
    }
}

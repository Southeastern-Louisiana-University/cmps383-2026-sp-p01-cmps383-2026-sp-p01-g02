namespace Selu383.SP26.Api.Models
{
    public class Location
    {
        public int Id { get; set; }
        
        public required string Name { get; set; } 
        
        public required string Address { get; set; } 
        
        public int TableCount { get; set; }
    }
}
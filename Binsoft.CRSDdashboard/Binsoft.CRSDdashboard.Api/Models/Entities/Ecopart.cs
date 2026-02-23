using System.ComponentModel.DataAnnotations;

namespace Binsoft.CRSDdashboard.Api.Models.Entities
{
    public class Ecopart
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public required string Material { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

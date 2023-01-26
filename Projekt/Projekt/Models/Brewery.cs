using System.ComponentModel.DataAnnotations;

namespace Projekt.Models;

public class Brewery{
    public int Id{ get; set; }
    
    [Required (ErrorMessage = "A good brewery needs a name!")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string Name{ get; set; }
    
    [Required (ErrorMessage = "Country of origin is required")]
    [Letters("Country", true)]
    [StringLength(20, ErrorMessage = "Country name is too long. Use a shortcut")]
    public string Country{ get; set; }
    
    [Letters("City", true)]
    [StringLength(30, ErrorMessage = "City name is too long")]
    public string? City{ get; set; }

    public ICollection<Beer> Beers{ get; set; }
    
    public Brewery(){
        Beers = new List<Beer>();
    }
    
    public override string ToString(){
        return $"{Name}, {City} ({Country})";
    }
    
    
}
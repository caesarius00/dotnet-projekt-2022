using System.ComponentModel.DataAnnotations;

namespace Projekt.Models;

public class BeerType{
    public int Id { get; set; }
    
    [Required]
    [Letters("Name")]
    public string Name { get; set; }
    public string? Description { get; set; }
    
    [Required]
    [AlcoholRange]
    public int MinAlcohol { get; set; }
    [Required]
    [AlcoholRange]
    public int MaxAlcohol { get; set; }
    
    public virtual ICollection<Beer> Beers { get; set; }
    
    public BeerType()
    {
        Beers = new List<Beer>();
    }
    
    //tostring with description if not null and alcohol range
    public override string ToString()
    {
        return $"{Name} ({MinAlcohol}-{MaxAlcohol}%)" + (Description != null ? $": {Description}" : "");
    }
}
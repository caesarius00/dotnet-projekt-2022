using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Projekt.Models;

public class Beer{
    public int Id{ get; set; }

    [Required(ErrorMessage = "Every beer needs a name!")]
    public string Name{ get; set; }

    public string? Description{ get; set; }
    
    [AlcoholRange]
    [Remote(action: "VerifyAlcohol", controller: "Beer", AdditionalFields="BeerTypeId")]
    public int? Alcohol{ get; set; }
    
    [Remote(action: "VerifyAlcohol", controller: "Beer", AdditionalFields="Alcohol")]
    public int? BeerTypeId{ get; set; }
    
    //reviews
    public ICollection<BeerReview>? BeerReviews{ get; set; }


    public BeerType? BeerType{ get; set; }
    
    [Required(ErrorMessage = "Where was it brewed?")]
    public int BreweryId{ get; set; } //foreign key

    public Brewery? Brewery{ get; set; }

    //tostring if brewery is null
    public override string ToString(){
        if(Brewery == null){
            return $"{Name} ({Alcohol}%)";
        }
        return $"{Name} ({Alcohol}%) from {Brewery}";
    }

}
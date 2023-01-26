using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Projekt.Models;

public class BeerUser : IdentityUser{
    
    
    public string? NickName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [PasswordPropertyText]
    public string? Password { get; set; }
    
    [Required]
    public string BeerRoleId { get; set; }
    public BeerRole? BeerRole { get; set; }

    //beer reviews
    public ICollection<BeerReview>? BeerReviews { get; set; }
    

    public override string ToString()
    {
        if (BeerRole == null){
            return NickName;
        }
        return $"{NickName} + ({BeerRole})";
    }
    
}
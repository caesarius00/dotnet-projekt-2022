using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Projekt.Models;

public class BeerRole : IdentityRole {
    
    [MaxLength (50)]
    public string? Description { get; set; }
    
    public ICollection<BeerUser>? BeerUsers { get; set; }
    
    public BeerRole() : base() { }
    public BeerRole(string roleName) : base(roleName) { }
    
    public override string ToString()
    {
        return $"{Name}";
    }
}
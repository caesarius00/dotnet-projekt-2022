using System.ComponentModel.DataAnnotations;

namespace Projekt.Models;

public class BeerReview{
    public int Id{ get; set; }
    
    [Required]
    public int BeerId{ get; set; }
    public Beer? Beer{ get; set; }
    public string? BeerUserId{ get; set; }
    public BeerUser? BeerUser{ get; set; }
    [Required]
    [Range(1, 10)]
    public int Rating{ get; set; }
    [MaxLength(500)]
    public string? Review{ get; set; }
    public DateTime Date{ get; set; }
    
    public override string ToString()
    {
        if (Review == null)
        {
            return $"{BeerUser?.NickName} ({Rating}) Date: {Date}";
        }
        return $"{BeerUser?.NickName} ({Rating}): {Review}" + $" Date: {Date}";
    }
    
}
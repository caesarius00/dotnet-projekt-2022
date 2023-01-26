using System.ComponentModel.DataAnnotations;

namespace Projekt.Models;

public class AlcoholRangeAttribute : ValidationAttribute{

    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

        if (Convert.ToInt32(value) < 0){
            return new ValidationResult("Oh. So you want to drink it to get sober?");
        }
        if (Convert.ToInt32(value) > 20){
            return new ValidationResult("Have you seen a beer with so much alcohol?");
        }

        return ValidationResult.Success;
    }

}
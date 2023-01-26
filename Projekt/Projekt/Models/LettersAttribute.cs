using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Projekt.Models;

public class LettersAttribute : ValidationAttribute{

    String _varName;
    bool firstLetterCapital;
    
    public LettersAttribute(string varName, bool fistLetterCapital = false)
    {
        _varName = varName;
        firstLetterCapital = fistLetterCapital;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

        //check if the value has only letters, spaces and - or '
        if (value != null && !Regex.IsMatch(value.ToString(), @"^[a-zA-Z\s\-\'\s]+$")) {
            return new ValidationResult("Only letters are allowed in " + _varName);
        }
        if ((value != null && !Regex.IsMatch(value.ToString(), @"^[A-Z]"))&&firstLetterCapital) {
            return new ValidationResult("The "+_varName +"'s first letter must be capital");
        }
        return ValidationResult.Success;
    }
    
}
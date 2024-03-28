using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaVillage.Customs.Attributes;

public class MatchPasswordAttribute : ValidationAttribute
{
    private readonly string _otherProperty;

    public MatchPasswordAttribute(string otherProperty)
    {
        _otherProperty = otherProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_otherProperty);

        if (property == null)
        {
            throw new ArgumentException("Property with this name not found");
        }

        var otherValue = property.GetValue(validationContext.ObjectInstance, null);

        if (value == null || otherValue == null || value.Equals(otherValue))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(ErrorMessage ?? "Passwords do not match.");
        }
    }
}

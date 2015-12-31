using FluentValidation;

namespace nancybook.Models
{
  public class FluentValidationExample
  {
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string PasswordRepeat { get; set; }
    public string PinCode { get; set; }

  }

  public class FluentValidationExampleValidator : AbstractValidator<FluentValidationExample>
  {
    public FluentValidationExampleValidator()
    {
      RuleFor(x => x.UserName).NotNull().NotEmpty();
      RuleFor(x => x.EmailAddress).NotNull().NotEmpty().EmailAddress().WithMessage("Email address was not supplied or is not a validly formed email address.");
      RuleFor(x => x.Password).NotNull().NotEmpty().Equal(y => y.PasswordRepeat).Equal("letmein").WithMessage("Invalid password specified.");
      RuleFor(x => x.PinCode).NotNull().NotEmpty().Equal("1234").WithMessage("Invalid pin code specified.");
    }
  }

}
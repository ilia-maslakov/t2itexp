using FluentValidation;
using t2itexp.Data.EF;

namespace t2itexp.Validators
{
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            var errmsg = "Incorrect data in [{PropertyName}]: value [{PropertyValue}]";
            RuleFor(x => x.Value).NotNull().NotEmpty().WithMessage(errmsg);
        }
    }
}

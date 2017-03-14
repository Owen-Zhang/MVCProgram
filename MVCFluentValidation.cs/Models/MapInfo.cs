using System;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;

namespace MVCFluentValidation.cs.Models
{
    [Validator(typeof(MapInfoValidator))]
    public class MapInfo
    {
        public string XPoint { get; set; }
        public string YPoing { get; set; }

        public string AddressInfo { get; set; }
    }

    public class ValidationBase<T> : AbstractValidator<T>
    {
        public ValidationBase()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;
        }

        public override ValidationResult Validate(T instance)
        {
            var result = base.Validate(instance);
            ThrowException(result);
            return result;
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var result = base.Validate(context);
            ThrowException(result);
            return result;
        }

        private void ThrowException(ValidationResult result)
        {
            /*
            if (!result.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, result.Errors));
             */
        }
    }

    public class MapInfoValidator : ValidationBase<MapInfo>
    {
        public MapInfoValidator() {
            
            this.RuleFor(item => item.XPoint)
                .NotNull()
                .NotEmpty();

            this.RuleFor(item => item.YPoing)
                .NotNull()
                .NotEmpty();

            this.RuleFor(item => item.AddressInfo)
                .NotNull()
                .NotEmpty();
        }
    }
}
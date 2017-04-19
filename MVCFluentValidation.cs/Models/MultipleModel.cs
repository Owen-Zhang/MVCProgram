using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace MVCFluentValidation.cs.Models
{
    [Validator(typeof(MultipleModelValidator))]
    public class MultipleModel
    {
        public string Name { get; set; }

        public string Tel { get; set; }

        public List<Address> AddressInfoList { get; set; }
    }

    public class Address {
        public string Name {get;set;}

        public string ZipCode {get;set;}

        public PointType? PointType { get; set; }
    }

    public enum PointType
    {
        XYPoint,
        XYZPoint
    }

    public class MultipleModelValidator : ValidationBase<MultipleModel>
    {
        public MultipleModelValidator()
        {
            this.RuleFor(item => item.Name)
                 .NotEmpty()
                 .WithMessage("Name is not null or empty");

            this.RuleFor(item => item.Tel)
                 .NotEmpty()
                 .WithMessage("Tel is not null or empty");

            this.RuleFor(item => item.AddressInfoList)
                .SetCollectionValidator(new AddressValidator())
                .When(item => item.AddressInfoList != null);
        }
    }

    public class AddressValidator : ValidationBase<Address>
    {
        public AddressValidator()
        { 
            this.RuleFor(item => item.Name)
                .NotEmpty()
                .WithMessage("Multiple Name is not null or empty");

            this.RuleFor(item => item.ZipCode)
                .NotEmpty()
                .WithMessage("ZipCode is not null or empty");
        }
    }
}
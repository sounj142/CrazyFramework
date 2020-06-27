using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CrazyFramework.Client.Models.Products
{
	public class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Name).Length(3, 200).WithMessage("Name must have between 3 and 200 characters");
			RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
			RuleFor(x => x.Price).LessThanOrEqualTo(10000).WithMessage("Price must be less than or equal to 10000");
		}
	}
}
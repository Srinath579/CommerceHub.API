using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required.");
        }
    }
}

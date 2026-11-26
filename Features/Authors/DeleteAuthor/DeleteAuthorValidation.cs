using FluentValidation;

namespace library_v3.Features.Authors.DeleteAuthor
{
    public class DeleteAuthorValidation : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id connot be empty")
                .GreaterThan(0).WithMessage("Id must be greater thn 0");
        }
    }

}

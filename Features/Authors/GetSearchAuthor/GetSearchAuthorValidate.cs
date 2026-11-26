using FluentValidation;

namespace library_v3.Features.Authors.GetSearchAuthor
{
    public class GetSearchAuthorValidate : AbstractValidator<GetSearchAuthorQuery>
    {
        public GetSearchAuthorValidate()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("cannot be empty")
                .MinimumLength(3).WithMessage("Search word must be minimum 3 chars")
                .MaximumLength(100).WithMessage("search word cannot exceed 100 chars");
        }
    }
}

using FluentValidation;
using Tweetbook3.Contracts.Requests;

namespace Tweetbook3.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9]*$");
                
        }
    }
}

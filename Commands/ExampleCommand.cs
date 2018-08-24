using System.Threading;
using System.Threading.Tasks;
using CodeStream.MediatR;
using FluentValidation;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Commands
{
    public class ExampleCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }
    }

    public class ExampleCommandValidator : AbstractValidator<ExampleCommand>
    {
        public ExampleCommandValidator()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithMessage("Name must have a value.");
        }
    }

    public class ExampleCommandHandler : IRequestHandler<ExampleCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ExampleCommand cmd, CancellationToken cancellationToken)
        {
            return new SuccessResult<string>($"ExampleCommand with name: {cmd.Name} was handled.");
        }
    }
}
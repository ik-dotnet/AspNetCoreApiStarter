using System;
using System.Threading;
using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.ViewModels
{
    public class TestExceptionQuery : AppMessage, IRequest<string>
    {
        public string Msg { get; set; }
    }

    public class TestExceptionQueryHandler : IRequestHandler<TestExceptionQuery, string>
    {
        public Task<string> Handle(TestExceptionQuery qry, CancellationToken cancellationToken)
        {
            throw new Exception(qry.Msg);
        }
    }
}
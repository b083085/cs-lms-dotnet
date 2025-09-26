using Capstone.LMS.Application.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Capstone.LMS.Application.Behaviors
{
    public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (IsNotCommand())
            {
                return await next(cancellationToken);
            }

            using var transactionScope = new TransactionScope();

            var response = await next(cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transactionScope.Complete();

            return response;
        }

        private static bool IsNotCommand()
        {
            return !typeof(TRequest).Name.EndsWith("Command");
        }
    }
}

using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    //We have to register this behavior in asp.net with Dependency injection
    public class ValidationBehavior<TRequest, TResponse> 
        (IEnumerable<IValidator<TRequest>> validators): IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        //next here is the next method 
        /// <summary>
        /// This Handle iterates over all possible validations in the incoming request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context,cancellationToken)));
            var failures = validationResults.Where(r=>r.Errors.Any()).SelectMany(r=>r.Errors).ToList();
            if (failures.Any())
                throw new ValidationException(failures);

            return await next();//This takes us to the next method in the pipeline
        }
    }
}

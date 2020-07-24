using MediatR;
using PracticalEventSourcing.Core.ReadModels;
using PracticalEventSourcing.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Queries
{
    public class GetAllProducts : IRequest<IEnumerable<ProductRM>>
    {
    }

    internal class GetAllProductsHandler : IRequestHandler<GetAllProducts, IEnumerable<ProductRM>>
    {
        IQueryRepository<ProductRM> queryRepository;
        public GetAllProductsHandler(IQueryRepository<ProductRM> repo)
        {
            queryRepository = repo;
        }

        public async Task<IEnumerable<ProductRM>> Handle(GetAllProducts request, CancellationToken cancellationToken)
        {
            return await queryRepository.GetAllAsync();
        }
    }
}

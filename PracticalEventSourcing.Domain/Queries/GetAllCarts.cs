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
    public class GetAllCarts: IRequest<IEnumerable<CartRM>>
    {
    }


    internal class GetAllHandler : IRequestHandler<GetAllCarts, IEnumerable<CartRM>>
    {
        IQueryRepository<CartRM> queryRepository;
        public GetAllHandler(IQueryRepository<CartRM> repo)
        {
            queryRepository = repo;
        }

        public async Task<IEnumerable<CartRM>> Handle(GetAllCarts request, CancellationToken cancellationToken)
        {
            return await queryRepository.GetAllAsync();
        }
    }
}

using MediatR;
using PracticalEventSourcing.Core.Dto;
using PracticalEventSourcing.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Domain.Queries
{
    public class GetCartInvoice : IRequest<InvoiceDto>
    {
        public Guid CartId { get; set; }

        public GetCartInvoice(Guid cartId)
        {
            CartId = cartId;
        }
    }


    internal class GetCartInvoiceHandler : IRequestHandler<GetCartInvoice, InvoiceDto>
    {
        IProductRepository _repository;
        public GetCartInvoiceHandler(IProductRepository repo)
        {
            _repository = repo;
        }


        public Task<InvoiceDto> Handle(GetCartInvoice query, CancellationToken cancellationToken)
        {
            var invoice = _repository.GetInvoice(query.CartId);
            return Task.FromResult(invoice);
        }
    }
}

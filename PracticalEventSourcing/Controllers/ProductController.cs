using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticalEventSourcing.Core.Dto;
using PracticalEventSourcing.Domain.Commands;
using PracticalEventSourcing.Domain.Queries;

namespace PracticalEventSourcing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var getAllProducts = new GetAllProducts();
                var products = await _mediator.Send(getAllProducts);
                return Ok(new { products });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewProductDto product)
        {
            try
            {
                var createProduct = new CreateProduct(Guid.NewGuid(), product.ProductName, product.Quantity);
                await _mediator.Send(createProduct);
                return Ok(new { message = "Product created" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut]
        [Route("changequantity")]
        public async Task<IActionResult> Put([FromBody]ProductQuantityChangeDto product)
        {
            try
            {
                var changeQuantity = new ChangeProductQuantity(product.ProductId.Value, product.Quantity);
                await _mediator.Send(changeQuantity);
                return Ok(new { message = "Product quantity changed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
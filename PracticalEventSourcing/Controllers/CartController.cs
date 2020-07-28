using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticalEventSourcing.Core.Dto;
using PracticalEventSourcing.Domain.Commands;

namespace PracticalEventSourcing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                var createCart = new CreateCart(Guid.NewGuid(), DateTime.Now);
                await _mediator.Send(createCart);
                return Ok(new { message = "New cart created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPatch]
        [Route("addProduct")]
        public async Task<IActionResult> AddProductToCart([FromBody]ModifyCartProductDto dto)
        {
            try
            {
                var addProduct = new AddProductToCart(dto.CartId, dto.ProductId, 1);
                await _mediator.Send(addProduct);
                return Ok(new { message = "Product added to cart" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPatch]
        [Route("removeProduct")]
        public async Task<IActionResult> RemoveProductToCart([FromBody]ModifyCartProductDto dto)
        {
            try
            {
                var addProduct = new RemoveProductFromCart(dto.CartId, dto.ProductId, 1);
                await _mediator.Send(addProduct);
                return Ok(new { message = "Product removed from cart" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

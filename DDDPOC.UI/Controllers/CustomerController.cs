using DDDPOC.Application;
using DDDPOC.Application.getCustomerCommand;
using DDDPOC.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDPOC.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<bool> Add(AddCustomerCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpGet("GetAll")]
        public async Task<List<CustomerDto>> Get()
        
        {
            return await _mediator.Send(new GetCustomerCommand());
        }
        [HttpGet("GetById")]
        public async Task<CustomerDto> Get(GetCustomerByIdCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

using DDDPOC.Application;
using DDDPOC.Application.getCustomerCommand;
using DDDPOC.Infrastructure.Repositories;
using Elastic.Apm;
using Elastic.Apm.Api;
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
        //private readonly ITracer _tracer;
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
            //var span = _tracer.CurrentTransaction?.StartSpan("MySampleSpan", "Sample");
            try
            {
                return await _mediator.Send(new GetCustomerCommand());

            }
            catch (Exception e)
            {

                //span?.CaptureException(e);
                throw;
            }
            finally
            {
                //span?.End();
            }
        }
        [HttpGet("GetById")]
        public async Task<CustomerDto> Get(GetCustomerByIdCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

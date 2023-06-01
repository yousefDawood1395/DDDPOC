using DDDPOC.Application.Commands.Customer;
using DDDPOC.Domain.Aggregates;
using DDDPOC.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDPOC.Application;

namespace DDDPOC.Application.getCustomerCommand
{
    public class GetCustomerCommand : IRequest<List<CustomerDto>>
    {
    }
    public class GetCustomerHandler : IRequestHandler<GetCustomerCommand,List<CustomerDto>>
    {
        private readonly IRepository<Customer, Guid> _customerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public GetCustomerHandler(IRepository<Customer, Guid> customerRepo,
                                  IUnitOfWork unitOfWork,
                                  ICacheService cacheService)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;

        }
        public Task<List<CustomerDto>> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<IEnumerable<Customer>>("customer");
            if (cacheData != null)
            {
                return Task.FromResult(CustomerDto.FromCustomers(cacheData));
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
             cacheData = _customerRepo.GetAll().ToList();
            _cacheService.SetData<IEnumerable<Customer>>("customer", cacheData, expirationTime);
            return Task.FromResult(CustomerDto.FromCustomers(cacheData));
        }
    }
}

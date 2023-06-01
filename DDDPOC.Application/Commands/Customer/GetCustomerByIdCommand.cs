using DDDPOC.Domain.Aggregates;
using DDDPOC.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application
{
    public class GetCustomerByIdCommand : IRequest<CustomerDto>
    {
        [Required]
        public Guid Id { get; set; }
    }
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdCommand, CustomerDto>
    {
        private readonly IRepository<Customer, Guid> _customerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public GetCustomerByIdHandler(IRepository<Customer, Guid> customerRepo,
                                  IUnitOfWork unitOfWork,
                                  ICacheService cacheService)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public Task<CustomerDto> Handle(GetCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<IEnumerable<Customer>>("customer");
            CustomerDto filteredData;
            if (cacheData != null)
            {
               var data = cacheData.Where(x => x.Id == request.Id).FirstOrDefault();
                filteredData = CustomerDto.FromCustomer(data);
                return Task.FromResult(filteredData);
            }
            filteredData =CustomerDto.FromCustomer(_customerRepo.GetAll(c => c.Id == request.Id).FirstOrDefault()!);

            return Task.FromResult(filteredData);
        }
    }
}

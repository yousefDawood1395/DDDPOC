using DDDPOC.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application
{
    public class CustomerDto
    {
        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public static CustomerDto FromCustomer(Customer input) =>
            new CustomerDto
            {
                Address = input.Address,
                Email = input.Email,
                CustomerName = input.CustomerName
            };
        public static List<CustomerDto> FromCustomers(IEnumerable<Customer> inputs)
        {

            List<CustomerDto> customers = new List<CustomerDto>();
            foreach (Customer customer in inputs)
            {
                customers.Add(FromCustomer(customer));
            }
            return customers;
        }
    }
}

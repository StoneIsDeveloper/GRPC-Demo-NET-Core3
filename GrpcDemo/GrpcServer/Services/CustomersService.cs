using Grpc.Core;
using GrpcServer.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane2";
                output.LastName = "Doe2";
            }
            else if (request.UserId == 3)
            {
                output.FirstName = "Jane3";
                output.LastName = "Doe3";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>() {
                new CustomerModel{  FirstName = "Jane1",LastName="Doe1"},
                new CustomerModel{  FirstName = "Jane2",LastName="Doe2"},
                new CustomerModel{  FirstName = "Jane3",LastName="Doe3"},
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }

        }


    }
}

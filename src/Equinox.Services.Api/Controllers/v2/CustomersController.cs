using Equinox.Application.DTOs;
using Equinox.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equinox.Services.Api.Controllers.v2
{
    [Authorize]
    [Route("api/[controller]")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomersController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<IEnumerable<CustomerListDto>> Get()
        {
            return Task.FromResult(Enumerable.Empty<CustomerListDto>());
        }
    }
}

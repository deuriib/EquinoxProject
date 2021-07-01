using Equinox.Application.DTOs;
using Equinox.Application.Interfaces;
using Equinox.Infra.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinox.Services.Api.Controllers.v1
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
        public async Task<IEnumerable<CustomerListDto>> Get()
        {
            return await _customerAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<CustomerDetailDto> Get(Guid id)
        {
            return await _customerAppService.GetById(id);
        }

        [ClaimsAuthorize("customers", "Write")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerDto request)
        {
            return !ModelState.IsValid ? Response(ModelState) : Response(await _customerAppService.Create(request));
        }

        [ClaimsAuthorize("customers", "Write")]
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateCustomerDto request)
        {
            return !ModelState.IsValid ? Response(ModelState) : Response(await _customerAppService.Update(id, request));
        }

        [ClaimsAuthorize("customers", "Delete")]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Response(await _customerAppService.Delete(id));
        }
    }
}

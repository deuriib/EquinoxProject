using Equinox.Application.DTOs;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinox.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task<IEnumerable<CustomerListDto>> GetAll();
        Task<CustomerDetailDto> GetById(Guid id);

        Task<ValidationResult> Create(CreateCustomerDto dto);
        Task<ValidationResult> Update(Guid id, UpdateCustomerDto dto);
        Task<ValidationResult> Delete(Guid id);

    }
}

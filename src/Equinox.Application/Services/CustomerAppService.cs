using AutoMapper;
using Equinox.Application.DTOs;
using Equinox.Application.Interfaces;
using Equinox.Domain.Commands;
using Equinox.Domain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equinox.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _mediator;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler mediator)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<IEnumerable<CustomerListDto>> GetAll()
            => _mapper.Map<IEnumerable<CustomerListDto>>(await _customerRepository.GetAll());

        public async Task<CustomerDetailDto> GetById(Guid id)
            => _mapper.Map<CustomerDetailDto>(await _customerRepository.GetById(id));

        public async Task<ValidationResult> Create(CreateCustomerDto dto)
            => await _mediator.SendCommand(new RegisterNewCustomerCommand(dto.Name, dto.Email, dto.BirthDate));

        public async Task<ValidationResult> Update(Guid id, UpdateCustomerDto dto)
            => await _mediator.SendCommand(new UpdateCustomerCommand(id, dto.Name, dto.Email, dto.BirthDate));

        public async Task<ValidationResult> Delete(Guid id)
            => await _mediator.SendCommand(new RemoveCustomerCommand(id));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

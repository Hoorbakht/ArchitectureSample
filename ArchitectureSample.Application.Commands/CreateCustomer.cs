﻿using ArchitectureSample.Application.Dtos;
using ArchitectureSample.Domain.Core.Cqrs;
using ArchitectureSample.Domain.Entities;
using ArchitectureSample.Domain.Repository;
using ArchitectureSample.Infrastructure.Core.Validators;
using FluentValidation;
using MediatR;

namespace ArchitectureSample.Application.Commands;

public class CreateCustomer
{
	public record Command : ICreateCommand<Command.CreateCustomerModel, CustomerDto>
	{
		public CreateCustomerModel Model { get; init; } = default!;

		public record CreateCustomerModel(string FirstName, string LastName, DateTime BirthOfDate, string PhoneNumber, string Email, string BankAccount);

		internal class Validator : AbstractValidator<Command>
		{
			public Validator()
			{
				RuleFor(v => v.Model.FirstName)
				    .NotEmpty().WithMessage("Name is required.")
				    .MinimumLength(5).WithMessage("Name must be longer than 5 characters.")
				    .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

				RuleFor(v => v.Model.LastName)
				    .NotEmpty().WithMessage("LastName is required.")
				    .MinimumLength(5).WithMessage("LastName must be longer than 5 characters.")
				    .MaximumLength(60).WithMessage("LastName must not exceed 60 characters.");

				RuleFor(x => x.Model.BirthOfDate)
				    .GreaterThanOrEqualTo(DateTime.Now.AddYears(-100)).WithMessage($"DateOfBirth should at least greater than or equal to {DateTime.Now.AddYears(-100).ToShortDateString()}.")
				    .LessThanOrEqualTo(DateTime.Now).WithMessage("DateOfBirth must not be greater than now");

				RuleFor(x => x.Model.Email)
					.NotEmpty().WithMessage("Email address is required.")
					.EmailValidator();

				RuleFor(x => x.Model.BankAccount)
					.NotEmpty().WithMessage("BankAccount is required.")
					.MinimumLength(10).WithMessage("BankAccount must be longer than 10 characters.")
					.MaximumLength(20).WithMessage("BankAccount must not exceed 20 characters.");

				RuleFor(x => x.Model.PhoneNumber)
					.NotEmpty().WithMessage("PhoneNumber is required.")
					.PhoneNumberValidator();
			}
		}

		internal class Handler(IRepository<Customer> repository) : IRequestHandler<Command, ResultModel<CustomerDto>>
		{
			public async Task<ResultModel<CustomerDto>> Handle(Command request, CancellationToken cancellationToken)
			{
				var created = await repository.AddAsync(
				    Customer.Create(
					request.Model.FirstName,
					request.Model.LastName,
					request.Model.BirthOfDate,
					request.Model.PhoneNumber,
					request.Model.Email,
					request.Model.BankAccount));

				return ResultModel<CustomerDto>.Create(new CustomerDto
				{
					Id = created.Id,
					FirstName = created.FirstName,
					LastName = created.LastName,
					DateOfBirth = created.DateOfBirth,
					PhoneNumber = created.PhoneNumber,
					Created = created.Created,
					Updated = created.Updated,
					Email = created.Email,
					BankAccount = created.BankAccount
				});
			}
		}
	}
}
using Finme.Application.WebApp.DTOs;
using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountAdditionalCommand : IRequest<UserResponseDto>
    {
        public int UserId { get; set; }
        public string? BusinessName { get; set; }
        public string? Cnpj { get; set; }
        public string? CurrentlyWorks { get; set; }
        public string? Occupation { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? MonthlyIncome { get; set; }
        public string? FinancialApplications { get; set; }
        public string? OtherIncomes { get; set; }
        public string? RealEstate { get; set; }
        public bool RelatedPerson { get; set; }
        public bool PoliticallyExposed { get; set; }
        public bool TaxResidence { get; set; }

        public List<UserAddressDto>? Address { get; set; }
    }
}

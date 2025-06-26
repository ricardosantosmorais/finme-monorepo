using Finme.Application.Admin.DTOs;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Commands
{
    public class ChangeUserCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string FullName { get; set; }
        public string? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Cpf { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Naturalness { get; set; } //Naturalidade
        public string? Nationality { get; set; } //Nacionalidade
        public string? Gender { get; set; } //Genero
        public string? MaritalStatus { get; set; } //Estado civil
        public string? CpfSpouse { get; set; } //CPF Conjuge
        public string? FullNameSpouse { get; set; } //Nome Conjuge
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
        public string Status { get; set; }
        public bool Active { get; set; }

        public List<UserBank>? Banks { get; set; }
        public List<UserAddress>? Address { get; set; }
        public List<UserDocument>? Documents { get; set; }
        public List<Bookmark>? Bookmarks { get; set; }

        public class UserBank
        {
            public int UserId { get; set; }
            public string Bank { get; set; }
            public string Agency { get; set; }
            public string CurrentAccount { get; set; }
            public bool JoinAccount { get; set; }
            public string? CpfSpouse { get; set; } //CPF Conjuge
            public string? FullNameSpouse { get; set; } //Nome Conjuge
        }

        public class UserAddress
        {
            public int UserId { get; set; }
            public string? Cep { get; set; }
            public string? Address { get; set; }
            public string? Number { get; set; }
            public string? Complement { get; set; }
            public string? Neighborhood { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
        }

        public class UserDocument
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string FileName { get; set; }
            public string DocumentType { get; set; }
            public string Type { get; set; }
            public bool Active { get; set; }
        }

        public class Bookmark
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int OperationId { get; set; }
            public OperationDto Operation { get; set; }
        }
    }
}

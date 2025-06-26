using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string FullName { get; set; }
        public string? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Token { get; set; }
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

        public UserBankDto[] Banks { get; set; }
        public UserAddressDto[] Address { get; set; }
        public UserDocumentDto[] Documents { get; set; }
        public BookmarkDto[] Bookmarks { get; set; }
    }
}

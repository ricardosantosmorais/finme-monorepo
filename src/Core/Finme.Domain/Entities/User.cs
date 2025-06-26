using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Entities
{
    public class User : Base
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public string? Token { get; private set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
        public bool VerifyPhone { get; set; }
        public bool VerifyEmail { get; set; }
        public List<UserBank>? UserBanks { get; set; } = [];
        public List<UserAddress>? UserAddreses { get; set; } = [];
        public List<UserDocument>? UserDocuments { get; set; } = [];
        public List<OperationComment>? OperationComments { get; set; } = [];
        public List<Bookmark>? Bookmarks { get; set; } = [];

        public User() { } // Construtor padrão para AutoMapper

        public void SetToken(string token) => Token = token;
        public void UpdatePassword(string newPasswordHash) => Password = newPasswordHash;
        public void UpdateEmail(string newEmail) => Email= newEmail;
        public void UpdateTelephone(string newPhone) => Telephone = newPhone;
    }
}

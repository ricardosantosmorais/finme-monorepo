using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Auth.Commands;
using Finme.Application.WebApp.Features.Investments.Commands;
using Finme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de RegisterDto para User
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

            // Mapeamento de User para LoginResponseDto
            CreateMap<User, LoginResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));

            // Mapeamento de User para Forgot
            CreateMap<User, ForgotResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            // Mapeamento de User para UserResponseDto
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Naturalness, opt => opt.MapFrom(src => src.Naturalness))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.CpfSpouse, opt => opt.MapFrom(src => src.CpfSpouse))
                .ForMember(dest => dest.FullNameSpouse, opt => opt.MapFrom(src => src.FullNameSpouse))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Banks, opt => opt.MapFrom(src => src.UserBanks))
                .ForMember(dest => dest.Bookmarks, opt => opt.MapFrom(src => src.Bookmarks))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.UserDocuments))
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.BusinessName))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj))
                .ForMember(dest => dest.CurrentlyWorks, opt => opt.MapFrom(src => src.CurrentlyWorks))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation))
                .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.FatherName))
                .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.MotherName))
                .ForMember(dest => dest.MonthlyIncome, opt => opt.MapFrom(src => src.MonthlyIncome))
                .ForMember(dest => dest.FinancialApplications, opt => opt.MapFrom(src => src.FinancialApplications))
                .ForMember(dest => dest.OtherIncomes, opt => opt.MapFrom(src => src.OtherIncomes))
                .ForMember(dest => dest.RealEstate, opt => opt.MapFrom(src => src.RealEstate))
                .ForMember(dest => dest.RelatedPerson, opt => opt.MapFrom(src => src.RelatedPerson))
                .ForMember(dest => dest.PoliticallyExposed, opt => opt.MapFrom(src => src.PoliticallyExposed))
                .ForMember(dest => dest.TaxResidence, opt => opt.MapFrom(src => src.TaxResidence))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserAddreses))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf));

            CreateMap<UserBank, UserBankDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Bank, opt => opt.MapFrom(src => src.Bank))
                .ForMember(dest => dest.CurrentAccount, opt => opt.MapFrom(src => src.CurrentAccount))
                .ForMember(dest => dest.CpfSpouse, opt => opt.MapFrom(src => src.CpfSpouse))
                .ForMember(dest => dest.FullNameSpouse, opt => opt.MapFrom(src => src.FullNameSpouse))
                .ForMember(dest => dest.JoinAccount, opt => opt.MapFrom(src => src.JoinAccount))
                .ForMember(dest => dest.Agency, opt => opt.MapFrom(src => src.Agency))
                .ReverseMap();

            CreateMap<UserAddress, UserAddressDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement))
                .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();

            CreateMap<UserDocument, UserDocumentDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ReverseMap();

            CreateMap<Operation, OperationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SocialName, opt => opt.MapFrom(src => src.SocialName))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background))
                .ForMember(dest => dest.TargetProfitability, opt => opt.MapFrom(src => src.TargetProfitability))
                .ForMember(dest => dest.MinimumInvestment, opt => opt.MapFrom(src => src.MinimumInvestment))
                .ForMember(dest => dest.MaximumInvestment, opt => opt.MapFrom(src => src.MaximumInvestment))
                .ForMember(dest => dest.InvestmentTarget, opt => opt.MapFrom(src => src.InvestmentTarget))
                .ForMember(dest => dest.FinalDate, opt => opt.MapFrom(src => src.FinalDate))
                .ForMember(dest => dest.InvestmentTerms, opt => opt.MapFrom(src => src.InvestmentTerms))
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment))
                .ForMember(dest => dest.QuoteValue, opt => opt.MapFrom(src => src.QuoteValue))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.ShareValue, opt => opt.MapFrom(src => src.ShareValue))
                .ForMember(dest => dest.Participation, opt => opt.MapFrom(src => src.Participation))
                .ForMember(dest => dest.Modality, opt => opt.MapFrom(src => src.Modality))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.AmountCollected, opt => opt.MapFrom(src => src.AmountCollected))
                .ForMember(dest => dest.ProjectedPayments, opt => opt.MapFrom(src => src.ProjectedPayments))
                .ForMember(dest => dest.Investors, opt => opt.MapFrom(src => src.Investors))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files));

            CreateMap<OperationComment, OperationCommentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            CreateMap<OperationFile, OperationFileDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OperationId, opt => opt.MapFrom(src => src.OperationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Bookmark, BookmarkDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.OperationId, opt => opt.MapFrom(src => src.OperationId))
                .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => src.Operation))
                .ReverseMap();

            CreateMap<CreateInvestmentCommand, Investment>();
        }
    }
}

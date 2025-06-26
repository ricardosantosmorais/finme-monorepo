using Amazon.Runtime.Internal;
using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Users.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Handlers
{
    public class ChangeUserHandler(
        IRepository<User> userRepository,
        IRepository<UserBank> userBankRepository,
        IRepository<UserAddress> userAddressRepository,
        IMapper mapper
        ) : IRequestHandler<ChangeUserCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserBank> _userBankRepository = userBankRepository;
        private readonly IRepository<UserAddress> _userAddressRepository = userAddressRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<UserDto> Handle(ChangeUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(x => x.Id == command.Id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado!");
            }

            // Atualiza as propriedades do usuário com base no comando
            user.FullName = command.FullName ?? user.FullName;
            user.Image = command.Image ?? user.Image;
            user.DateOfBirth = command.DateOfBirth ?? user.DateOfBirth;
            user.Telephone = command.Telephone ?? user.Telephone;
            user.Cpf = command.Cpf ?? user.Cpf;
            user.Email = command.Email ?? user.Email;
            user.DocumentNumber = command.DocumentNumber ?? user.DocumentNumber;
            user.Naturalness = command.Naturalness ?? user.Naturalness;
            user.Nationality = command.Nationality ?? user.Nationality;
            user.Gender = command.Gender ?? user.Gender;
            user.MaritalStatus = command.MaritalStatus ?? user.MaritalStatus;
            user.CpfSpouse = command.CpfSpouse ?? user.CpfSpouse;
            user.FullNameSpouse = command.FullNameSpouse ?? user.FullNameSpouse;
            user.BusinessName = command.BusinessName ?? user.BusinessName;
            user.Cnpj = command.Cnpj ?? user.Cnpj;
            user.CurrentlyWorks = command.CurrentlyWorks ?? user.CurrentlyWorks;
            user.Occupation = command.Occupation ?? user.Occupation;
            user.FatherName = command.FatherName ?? user.FatherName;
            user.MotherName = command.MotherName ?? user.MotherName;
            user.MonthlyIncome = command.MonthlyIncome ?? user.MonthlyIncome;
            user.FinancialApplications = command.FinancialApplications ?? user.FinancialApplications;
            user.OtherIncomes = command.OtherIncomes ?? user.OtherIncomes;
            user.RealEstate = command.RealEstate ?? user.RealEstate;
            user.RelatedPerson = command.RelatedPerson;
            user.PoliticallyExposed = command.PoliticallyExposed;
            user.TaxResidence = command.TaxResidence;
            user.Status = command.Status;
            user.Active = command.Active;
            user.UpdatedAt = DateTime.UtcNow; // Atualiza a data de modificação

            // Verifica se a lista UserBanks não é nula antes de acessar sua contagem
            if (user.UserBanks != null && user.UserBanks.Count != 0)
            {
                foreach (var item in user.UserBanks)
                {
                    _userBankRepository.Delete(item);
                }
            }

            // Verifica se a lista UserBanks não é nula antes de acessar sua contagem
            if (user.UserAddreses != null && user.UserAddreses.Count != 0)
            {
                foreach (var item in user.UserAddreses)
                {
                    _userAddressRepository.Delete(item);
                }
            }

            var bank = new UserBank
            {
                Agency = command.Banks?.FirstOrDefault()?.Agency ?? string.Empty, // Provide a default value to avoid null assignment
                Bank = command.Banks?.FirstOrDefault()?.Bank ?? string.Empty, // Provide a default value to avoid null assignment
                CpfSpouse = command.Banks?.FirstOrDefault()?.CpfSpouse ?? string.Empty, // Provide a default value to avoid null assignment
                CurrentAccount = command.Banks?.FirstOrDefault()?.CurrentAccount ?? string.Empty, // Provide a default value to avoid null assignment
                FullNameSpouse = command.Banks?.FirstOrDefault()?.FullNameSpouse ?? string.Empty, // Provide a default value to avoid null assignment
                JoinAccount = command.Banks?.FirstOrDefault()?.JoinAccount ?? false, // Provide a default value to avoid null assignment
                UserId = user.Id
            };

            await _userBankRepository.AddAsync(bank);

            var address = new UserAddress
            {
                UserId = user.Id,
                Cep = command.Address?.FirstOrDefault()?.Cep ?? string.Empty, // Provide a default value to avoid null assignment
                Address = command.Address?.FirstOrDefault()?.Address ?? string.Empty, // Provide a default value to avoid null assignment
                Number = command.Address?.FirstOrDefault()?.Number ?? string.Empty, // Provide a default value to avoid null assignment
                Complement = command.Address?.FirstOrDefault()?.Complement ?? string.Empty, // Provide a default value to avoid null assignment
                Neighborhood = command.Address?.FirstOrDefault()?.Neighborhood ?? string.Empty, // Provide a default value to avoid null assignment
                City = command.Address?.FirstOrDefault()?.City ?? string.Empty, // Provide a default value to avoid null assignment
                State = command.Address?.FirstOrDefault()?.State ?? string.Empty // Provide a default value to avoid null assignment
            };

            await _userAddressRepository.AddAsync(address);

            // Atualiza o usuário no repositório
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}

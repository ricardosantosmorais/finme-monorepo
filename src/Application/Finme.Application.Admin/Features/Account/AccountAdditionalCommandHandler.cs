using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountAdditionalCommandHandler(
        IRepository<User> userRepository,
        IRepository<UserAddress> userAddressRepository,
        IMapper mapper) : IRequestHandler<AccountAdditionalCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserAddress> _userAddressRepository = userAddressRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(AccountAdditionalCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            user.BusinessName = request.BusinessName;
            user.Cnpj = request.Cnpj;
            user.CurrentlyWorks = request.CurrentlyWorks;
            user.Occupation = request.Occupation;
            user.FatherName = request.FatherName;
            user.MotherName = request.MotherName;
            user.MonthlyIncome = request.MonthlyIncome;
            user.FinancialApplications = request.FinancialApplications;
            user.OtherIncomes = request.OtherIncomes;
            user.RealEstate = request.RealEstate;
            user.RelatedPerson = request.RelatedPerson;
            user.PoliticallyExposed = request.PoliticallyExposed;
            user.TaxResidence = request.TaxResidence;
            user.UpdatedAt = DateTime.UtcNow;

            // Persistir no banco
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            if (request.Address != null)
            {
                if (request.Address.Any())
                {
                    var address = await _userAddressRepository.FindAsync(x => x.UserId == request.UserId);
                    address ??= new UserAddress();
                    var addressRequest = request.Address.FirstOrDefault();

                    if (addressRequest != null)
                    {
                        address.UserId = request.UserId;
                        address.Cep = addressRequest.Cep;
                        address.Address = addressRequest.Address;
                        address.City = addressRequest.City;
                        address.Number = addressRequest.Number;
                        address.Complement = addressRequest.Complement;
                        address.Neighborhood = addressRequest.Neighborhood;
                        address.State = addressRequest.State;

                        if(address.Id == 0)
                        {
                            await _userAddressRepository.AddAsync(address);
                        }
                        else
                        {
                            _userAddressRepository.Update(address);
                        }
                        await _userAddressRepository.SaveChangesAsync();
                    }
                }
            }

            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<UserDto>(user);
        }
    }
}

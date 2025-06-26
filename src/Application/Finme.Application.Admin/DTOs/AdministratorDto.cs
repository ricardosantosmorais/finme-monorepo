
namespace Finme.Application.Admin.DTOs
{
    public class AdministratorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string? Telephone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string? Token { get; private set; }
        public string Status { get; set; }
        public bool Active { get; set; }
    }
}

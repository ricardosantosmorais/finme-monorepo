

namespace Finme.Application.Admin.DTOs.Request
{
    public class SaveAdministratorRequest
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string? Telephone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
    }
}

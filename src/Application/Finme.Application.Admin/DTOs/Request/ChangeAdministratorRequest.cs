

namespace Finme.Application.Admin.DTOs.Request
{
    public class ChangeAdministratorRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string? Telephone { get; set; }
        public string Cpf { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}

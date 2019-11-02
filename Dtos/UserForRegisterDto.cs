using System.ComponentModel.DataAnnotations;

namespace HelperDesk.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required (ErrorMessage = "Ingrese su nombre")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Ingrese su apellido")]
        public string LastName { get; set; }

        [EmailAddress (ErrorMessage = "Ingrese un correo válido")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Numero de telefono es requerido")]
        public string Phone { get; set; }

        public string Username { get; set; }

        [Required (ErrorMessage = "La contraseña es requerida")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Debe especificar una contraseña entre 4 y 8 caracteres")]
        public string Password { get; set; }

        public int GenderId { get; set; }

        public int RoleId { get; set; }

        public int CompanyId { get; set; }

        public int status { get; set; }
    }
}
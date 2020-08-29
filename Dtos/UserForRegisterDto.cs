using System.ComponentModel.DataAnnotations;

namespace HelperDesk.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required (ErrorMessage = "Ingrese su nombre")]
        public string Names { get; set; }

        [Required (ErrorMessage = "Ingrese su apellido")]
        public string LastName { get; set; }

        [EmailAddress (ErrorMessage = "Ingrese un correo válido")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Numero de telefono es requerido")]
        public string Phone { get; set; }

        public string Username { get; set; }

        [Required (ErrorMessage = "La contraseña es requerida")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Debe especificar una contraseña entre 8 y 15 caracteres")]
        public string Password { get; set; }

        // [Required (ErrorMessage = "Es neceario tener un codigo de verificación")]
        // public string Authy_id { get; set; }

        public int GenderId { get; set; }

        public int RoleId { get; set; }

        public int CompanyId { get; set; }

        public int status { get; set; }
    }
}
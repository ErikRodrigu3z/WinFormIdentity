using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WinFormIdentity.Models
{
    public class AppUsers : IdentityUser
    {
        [Required(ErrorMessage = "El Nombre es Requerido")]
        [RegularExpression(@"^[A-Za-z áÁéÉíÍóÓúÚ]+$", ErrorMessage = "Solo Letras")]
        [MaxLength(100), MinLength(4, ErrorMessage = "Minino 4 Caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es Requerido")]
        [RegularExpression(@"^[A-Za-z áÁéÉíÍóÓúÚ]+$", ErrorMessage = "Solo Letras")]
        [MaxLength(100), MinLength(4, ErrorMessage = "Minino 4 Caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Password es Requerido")]
        [StringLength(100, ErrorMessage = "Minimo 8 Catacteres, Minimo 1 Mayuscula y Minimo 1 Caracter Especial", MinimumLength = 8)]
        public string PasswordRecover { get; set; }

        [DefaultValue("True")]
        public bool Activo { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }

        public DateTime? FechaAlta { get; set; }

        public DateTime? FechaInactivacion { get; set; }

        [Required(ErrorMessage = "El Email es Requerido")]
        [EmailAddress(ErrorMessage = "Capture un Email Valido")]
        public override string Email { get => base.Email; set => base.Email = value; }

        [Required(ErrorMessage = "El Telefono es Requerido")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo Numeros")]
        [MaxLength(10, ErrorMessage = "Solo 10 Digitos")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        public string FirebaseId { get; set; }
        public string DeviceId { get; set; }
        public string Logout { get; set; }

        public string ImgPerfil { get; set; }


       
    }
}

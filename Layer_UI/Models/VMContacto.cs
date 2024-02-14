using System.ComponentModel.DataAnnotations;

namespace Layer_UI.Models
{
    public class VMContacto
    {
        public int IdContacto { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string? FechaNacimiento { get; set; }

    }
}

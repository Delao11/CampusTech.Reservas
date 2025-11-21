using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CampusTech.Reservas.Models
{
    public enum Laboratorio
    {
        [Display(Name = "Seleccione...")] None,
        [Display(Name = "Lab-01")] Lab01,
        [Display(Name = "Lab-02")] Lab02,
        [Display(Name = "Lab-03")] Lab03,
        [Display(Name = "Lab-Redes")] LabRedes,
        [Display(Name = "Lab-IA")] LabIA
    }

    public class Reserva
    {
        public int Id { get; set; } // Id interno para la lista en memoria

        [Required(ErrorMessage = "El nombre del profesor es obligatorio.")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        public string NombreProfesor { get; set; }

        [Required(ErrorMessage = "El correo institucional es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [RegularExpression(@".+@campus\.edu$", ErrorMessage = "El correo debe pertenecer al dominio @campus.edu.")]
        public string CorreoInstitucional { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un laboratorio.")]
        [EnumDataType(typeof(Laboratorio), ErrorMessage = "Laboratorio inválido.")]
        public Laboratorio Laboratorio { get; set; }

        [Required(ErrorMessage = "La fecha de la reserva es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaReserva { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [MinLength(5, ErrorMessage = "El motivo debe tener al menos 5 caracteres.")]
        [MaxLength(200, ErrorMessage = "El motivo no puede exceder 200 caracteres.")]
        public string Motivo { get; set; }

        [Required(ErrorMessage = "El código de reserva es obligatorio.")]
        [RegularExpression(@"^RES-\d{3}$", ErrorMessage = "El código debe tener el formato RES-### (ej. RES-001).")]
        public string CodigoReserva { get; set; }
    }
}

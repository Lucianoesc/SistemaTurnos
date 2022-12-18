using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaEntidad
{
    public class Medico
    {
        public int IdMedico { get; set; }

        public string DocumentoMedico { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NumeroMatricula { get; set; }
        public Especialidad oEspecialidad { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}

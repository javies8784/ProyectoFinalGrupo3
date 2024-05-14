using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoG3.Models
{
    public class RegistroMultas
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public int tipomulta { get; set; }
        public string numeroplaca { get; set; }
        public string coordenadas { get; set; }
        public string imagen_base64 { get; set; }
        public string hora { get; set; }


        public string fh = "Fecha";
        public string pc = "Placa";

        public string text => $"{pc}: {numeroplaca}";
        public string detalle => $"{fh}: {fecha:dd/MM/yyyy} {hora}";
    }
}

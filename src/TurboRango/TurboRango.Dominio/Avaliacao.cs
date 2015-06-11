using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Avaliacao : Entidade
    {
        public string Login { get; set; }
        public double Nota { get; set; }
        public DateTime Data { get; set; }
    }
}

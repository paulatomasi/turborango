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
        public int Nota { get; set; }
        public virtual Restaurante Restaurante { get; set; }
        public DateTime Data { get; set;  }
    }
}

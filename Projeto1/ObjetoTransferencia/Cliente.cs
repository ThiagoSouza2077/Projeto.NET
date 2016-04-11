using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Boolean Sexo { get; set; }
        public decimal LimiteCompra { get; set; }
    }
}

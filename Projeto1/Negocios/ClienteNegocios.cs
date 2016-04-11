using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcessoBancoDados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class ClienteNegocios
    {
        //Instanciar 
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimitecOMPRA", cliente.LimiteCompra);

                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();

                return idCliente;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string Alterar(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);

                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(
                    CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                return idCliente;


            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string Excluir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente",cliente.IdCliente);
                string idCliente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();
                return idCliente;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public ClienteColecao ConsultarPorNome(string nome)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome",nome);

                 DataTable dataTableCliente = acessoDadosSqlServer.ExecutarConsulta(
                     CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                foreach (DataRow linha in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento =Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Não foi possível consultar o cliente por nome. " + ex.Message);
            }
        }

        public ClienteColecao ConsultarPorId(int idCliente)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente",idCliente);

                DataTable dataTableCliente =  acessoDadosSqlServer.ExecutarConsulta(
                    CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow linha in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();

                    cliente.IdCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.Nome = Convert.ToString(linha["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Não foi possível consultar o cliente por código. Detalhes: " + ex.Message);
            }
        }



    }
}

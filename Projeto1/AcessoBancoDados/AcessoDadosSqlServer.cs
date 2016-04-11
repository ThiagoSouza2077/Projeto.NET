using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//classe para manipular os dados do banco de dados
using System.Data;
using System.Data.SqlClient;
using AcessoBancoDados.Properties;


namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //Aqui eu crio a conexao ao banco
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parâmetro que vão para o banco, declarar igual está no banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência -Inserir, Alterar, Excluir
        public object ExecutarManipulacao(CommandType commandType,string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //criei a conexao
                SqlConnection sqlConnection = CriarConexao();
                //abri a conexao
                sqlConnection.Open();
                //criei um comando q leva informação ao banco.
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //colocando as coisas dentro do comando
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7000; //em segundos

                //add os parâmetro no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                //Executar o comando,mandar o comando ir até o banco de dados.
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
           
        //Consultar registros do banco de dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //criei a conexao
                SqlConnection sqlConnection = CriarConexao();
                //abri a conexao
                sqlConnection.Open();
                //criei um comando q leva informação ao banco.
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //colocando as coisas dentro do comando
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7000; //em segundos

                //add os parâmetro no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
             
                //Criar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //DataTable = Tabela de Dados vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new DataTable();

                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher o datatable
                sqlDataAdapter.Fill(dataTable);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

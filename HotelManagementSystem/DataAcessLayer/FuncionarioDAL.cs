using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    public class FuncionarioDAL
    {
        public Response Insert(Funcionario funcionario)
        {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "INSERT INTO FUNCIONARIOS (NOME,CPF,RG,CEP,RUA,BAIRRO,CIDADE,ESTADO,NUMERO," +
                "COMPLEMENTO,TELEFONE,EMAIL,SENHA,ISADM,ATIVO) " +
                "VALUES (@NOME,@CPF,@RG,@CEP,@RUA,@BAIRRO,@CIDADE,@ESTADO,@NUMERO," +
                "@COMPLEMENTO,@TELEFONE,@EMAIL,@SENHA,@ISADM,@ATIVO)";
            command.Parameters.AddWithValue("@NOME", funcionario.Nome);
            command.Parameters.AddWithValue("@CPF", funcionario.CPF);
            command.Parameters.AddWithValue("@RG", funcionario.RG);
            command.Parameters.AddWithValue("@CEP", funcionario.Cep);
            command.Parameters.AddWithValue("@RUA", funcionario.Rua);
            command.Parameters.AddWithValue("@BAIRRO", funcionario.Bairro);
            command.Parameters.AddWithValue("@CIDADE", funcionario.Cidade);
            command.Parameters.AddWithValue("@ESTADO", funcionario.Estado);
            command.Parameters.AddWithValue("@NUMERO", funcionario.Numero);
            command.Parameters.AddWithValue("@COMPLEMENTO", funcionario.Complemento);
            command.Parameters.AddWithValue("@TELEFONE", funcionario.Telefone);
            command.Parameters.AddWithValue("@EMAIL", funcionario.Email);
            command.Parameters.AddWithValue("@SENHA", funcionario.Senha);
            command.Parameters.AddWithValue("@ISADM", funcionario.IsAdm);
            command.Parameters.AddWithValue("@ATIVO", true);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Cadastrado com sucesso.";
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }
        public Response Update(Funcionario funcionario)
        {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE FUNCIONARIOS SET NOME = @NOME, ATIVO = @ATIVO WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", funcionario.Nome);
            command.Parameters.AddWithValue("@ATIVO", funcionario.Ativo);
            command.Parameters.AddWithValue("@ID", funcionario.ID);


            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }
        public Response Delete(Funcionario funcionario)
        {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "DELETE FROM FUNCIONARIOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", funcionario.ID);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Excluído" +
                    " com sucesso.";
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }
        public QueryResponse<Funcionario> GetAll()
        {
            QueryResponse<Funcionario> response = new QueryResponse<Funcionario>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM FUNCIONARIOS WHERE ATIVO = 1";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Funcionario> funcionarios = new List<Funcionario>();

                while (reader.Read())
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.ID = (int)reader["ID"];
                    funcionario.Nome = (string)reader["NOME"];
                    funcionario.CPF = (string)reader["CPF"];
                    funcionario.Telefone = (string)reader["TELEFONE"];
                    funcionario.Email = (string)reader["EMAIL"];
                    funcionario.Ativo = (bool)reader["ATIVO"];
                    funcionarios.Add(funcionario);

                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = funcionarios;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }

        }
        public Response IsCpfUnique(string cpf)
        {

            QueryResponse<Funcionario> response = new QueryResponse<Funcionario>();

            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM FUNCIONARIOS WHERE CPF = @CPF";

            command.Parameters.AddWithValue("@CPF", cpf);

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                //Nem é um while ! É so um if, e se ele for true, é pq achou o cpf
                if (reader.Read())
                {
                    response.Success = false;
                    response.Message = "CPF já é cadastrado";
                }
                else
                {
                    response.Success = true;
                    response.Message = "CPF único.";
                }


                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no Banco de Dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;

            }
            finally
            {
                connection.Close();
            }



        }


    }
}

using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAcessLayer {
    public class FornecedorDAL
    {

        public Response Insert(Fornecedor fornecedor)
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
                "INSERT INTO FORNECEDORES (RAZAO,CNPJ,NOME,TELEFONE,EMAIL,ATIVO) " +
                "VALUES (@RAZAO,@CNPJ,@NOME,@TELEFONE,@EMAIL,@ATIVO)";
            command.Parameters.AddWithValue("@RAZAO", fornecedor.RazaoSocial);
            command.Parameters.AddWithValue("@CNPJ", fornecedor.CNPJ);
            command.Parameters.AddWithValue("@NOME", fornecedor.NomeContato);
            command.Parameters.AddWithValue("@TELEFONE", fornecedor.Telefone);
            command.Parameters.AddWithValue("@EMAIL", fornecedor.Email);
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
        public Response Update(Fornecedor fornecedor)
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
                "UPDATE FORNECEDORES SET RAZAO = @RAZAO, ATIVO = @ATIVO WHERE ID = @ID";
            command.Parameters.AddWithValue("@RAZAO", fornecedor.RazaoSocial);
            command.Parameters.AddWithValue("@ATIVO", fornecedor.Ativo);
            command.Parameters.AddWithValue("@ID", fornecedor.ID);

          
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
        public Response Delete(Fornecedor fornecedor)
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
                "DELETE FROM FORNECEDORES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", fornecedor.ID);

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
        public QueryResponse<Fornecedor> GetAll()
        {
            QueryResponse<Fornecedor> response = new QueryResponse<Fornecedor>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM FORNECEDORES WHERE ATIVO = 1";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Fornecedor> fornecedores = new List<Fornecedor>();

                while (reader.Read())
                {
                    Fornecedor fornecedor = new Fornecedor();
                    fornecedor.ID = (int)reader["ID"];
                    fornecedor.RazaoSocial = (string)reader["RAZAO_SOCIAL"];
                    fornecedor.CNPJ = (string)reader["CNPJ"];
                    fornecedor.NomeContato = (string)reader["NOME_CONTATO"];
                    fornecedor.Telefone = (string)reader["TELEFONE"];
                    fornecedor.Email = (string)reader["EMAIL"];
                    fornecedor.Ativo = (bool)reader["ATIVO"];
                    fornecedores.Add(fornecedor);

                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = fornecedores;
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
        public Response IsCNPJUnique(string cnpj)
        {
            QueryResponse<Fornecedor> response = new QueryResponse<Fornecedor>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT ID FROM FORNECEDORES WHERE CNPJ = @CNPJ";
            command.Parameters.AddWithValue("@CNPJ", cnpj);
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                //Nem é um while! É só um if, e, se ele for true, é pq achou o cpf!
                if (reader.Read())
                {
                    response.Success = false;
                    response.Message = "CPF já cadastrado.";
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
    }
}

using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAcessLayer {
    public class ProdutosDAL
    {
        public Response Insert(Produto produto)sasasa
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
                "INSERT INTO PRODUTOS (NOME,DESCRICAO,PRECO,QTD_ESTOQUE,ATIVO) " +
                "VALUES (@NOME,@DESCRICAO,@PRECO,@QTD_ESTOQUE,@ATIVO)";
            command.Parameters.AddWithValue("@NOME", produto.Nome);
            command.Parameters.AddWithValue("@DESCRICAO", produto.Descricao);
            command.Parameters.AddWithValue("@PRECO", produto.preco);
            command.Parameters.AddWithValue("@QTD_ESTOQUE", produto.quantidade);
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
        public Response Update(Produto produto)
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
                "UPDATE PRODUTOS SET NOME = @NOME, ATIVO = @ATIVO WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", produto.Nome);
            command.Parameters.AddWithValue("@ATIVO", produto.Ativo);
            command.Parameters.AddWithValue("@ID", produto.ID);


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
        public Response Delete(Produto produto)
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
                "DELETE FROM PRODUTOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", produto.ID);

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
        public QueryResponse<Produto> GetAll()
        {
            QueryResponse<Produto> response = new QueryResponse<Produto>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM PRODUTOS WHERE ATIVO = 1";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Produto> produtos = new List<Produto>();

                while (reader.Read())
                {
                    Produto produto = new Produto();
                    produto.ID = (int)reader["ID"];
                    produto.Nome = (string)reader["NOME"];
                    produto.Descricao= (string)reader["DESCRICAO"];
                    produto.preco= (double)reader["PRECO"];
                    produto.quantidade= (int)reader["QTD_ESTOQUE"];
                    produto.Ativo = (bool)reader["ATIVO"];
                    produtos.Add(produto);

                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = produtos;
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

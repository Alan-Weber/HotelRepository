using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAcessLayer {
    public class QuartosDAL
    {
        public Response Insert(Quarto quarto) {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "INSERT INTO QUARTOS (NUMERO, CAPACIDADE, ESTA_OCUPADO, PRECO, CLIENTE_ID)" +
                "VALUES (@NUMERO, @CAPACIDADE, @ESTA_OCUPADO, @PRECO, @CLIENTE_ID)";
            command.Parameters.AddWithValue("@NUMERO", quarto.Numero);
            command.Parameters.AddWithValue("@CAPACIDADE", quarto.Capacidade);
            command.Parameters.AddWithValue("@ESTA_OCUPADO", quarto.EstaOcupado);
            command.Parameters.AddWithValue("@PRECO", quarto.Preco);
            command.Parameters.AddWithValue("@CLIENTE_ID", quarto.ClienteId);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Cadastrado com sucesso.";
            } catch (Exception ex) {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            } finally {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }
        public Response Update(Quarto quarto) {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE QUARTOS SET NUMERO = @NUMERO, CAPACIDADE = @CAPACIDADE, ESTA_OCUPADO = @ESTA_OCUPADO, PRECO = @PRECO, CLIENTE_ID = @CLIENTE_ID WHERE ID = @ID";
            command.Parameters.AddWithValue("@NUMERO", quarto.Numero);
            command.Parameters.AddWithValue("@CAPACIDADE", quarto.Capacidade);
            command.Parameters.AddWithValue("@ESTA_OCUPADO", quarto.EstaOcupado);
            command.Parameters.AddWithValue("@PRECO", quarto.Preco);
            command.Parameters.AddWithValue("@CLIENTE_ID", quarto.ClienteId);
            command.Parameters.AddWithValue("@ID", quarto.ID);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1) {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Atualizado com sucesso.";
            } catch (Exception ex) {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            } finally {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }
        public Response Delete(Quarto quarto) {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "DELETE FROM QUARTOS WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", quarto.ID);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1) {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Excluído" +
                    " com sucesso.";
            } catch (Exception ex) {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            } finally {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }
        public QueryResponse<Quarto> GetAll() /* VERIFICAR */ { 
            QueryResponse<Quarto> response = new QueryResponse<Quarto>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM QUARTOS WHERE ATIVO = 1";

            command.Connection = connection;

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Quarto> quartos = new List<Quarto>();

                while (reader.Read()) {
                    Quarto quarto = new Quarto();
                    quarto.ID = (int)reader["ID"];
                    quarto.Numero = (string)reader["NUMERO"];
                    quarto.Capacidade = (int)reader["CAPACIDADE"];
                    quarto.EstaOcupado = (bool)reader["ESTA_OCUPADO"];
                    quarto.Preco = (int)reader["EMAIL"];
                   //Verifícar com o Marcelo se não tem que referenciar o Cliente_ID também
                    quartos.Add(quarto);
                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = quartos;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no banco de dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            } finally {
                connection.Close();
            }

        }
        public Response IsQuartoUnique(int quartoId, int clienteId) /* VERIFICAR */ {

            QueryResponse<Quarto> response = new QueryResponse<Quarto>();

            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM QUARTOS WHERE ID = @ID, CLIENTE_ID = @CLIENTE_ID";

            command.Parameters.AddWithValue("@ID", quartoId);
            command.Parameters.AddWithValue("@CLIENTE_ID", clienteId);

            command.Connection = connection;

            try {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                //Nem é um while ! É so um if, e se ele for true, é pq achou o cpf
                if (reader.Read()) {
                    response.Success = false;
                    response.Message = "Quarto ocupado"; //Verificar a regra se está correta
                }
                else {
                    response.Success = true;
                    response.Message = "Quarto disponível."; //Verificar a regra se está correta
                }

                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Erro no Banco de Dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;

            } finally {
                connection.Close();
            }
        }
    }
}

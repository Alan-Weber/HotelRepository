using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAcessLayer {
    public class ClienteDAL
    {
        
            public Response Insert(Cliente cliente)
            {
                Response response = new Response();

                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = ConnectionHelper.GetConnectionString();

                SqlCommand command = new SqlCommand();

                command.CommandText = "INSERT INTO CLIENTES (NOME, CPF, RG, TELEFONE,CELULAR,EMAIL) " +
                "VALUES (@NOME, @CPF, @RG, @TELEFONE,@CELULAR,@EMAIL)";

                command.Parameters.AddWithValue("@NOME", cliente.Nome);
                command.Parameters.AddWithValue("@CPF", cliente.CPF);
                command.Parameters.AddWithValue("@RG", cliente.RG);
                command.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
                command.Parameters.AddWithValue("@CELULAR", cliente.Celular);
                command.Parameters.AddWithValue("@EMAIL", cliente.Email);

                command.Connection = connection;

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    response.Success = true;
                    response.Message = "Cadastrado com sucesso";

                }
                catch (Exception ex)
                {

                    response.Success = false;
                    response.Message = "Erro no Banco de Dados, contate o administrador.";


                    //Controle de LOG
                    response.StackTrace = ex.StackTrace;
                    response.ExceptionError = ex.Message;
                }
                finally
                {
                    connection.Close();
                }

                return response;

            }

            public Response Update(Cliente cliente)
            {
                Response response = new Response();

                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = ConnectionHelper.GetConnectionString();

                SqlCommand command = new SqlCommand();

                command.CommandText = "UPDATE CLIENTES SET NOME = @NOME, CPF = @CPF, TELEFONE = @TELEFONE, CELULAR = @CELULAR, EMAIL = @EMAIL WHERE ID = @ID";

                command.Parameters.AddWithValue("@NOME", cliente.Nome);
                command.Parameters.AddWithValue("@CPF", cliente.CPF);
                command.Parameters.AddWithValue("@RG", cliente.RG);
                command.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
                command.Parameters.AddWithValue("@CELULAR", cliente.Celular);
                command.Parameters.AddWithValue("@EMAIL", cliente.Email);
                command.Parameters.AddWithValue("@ID", cliente.ID);


                command.Connection = connection;

                try
                {
                    connection.Open();

                    int nLinhasAfetadas = command.ExecuteNonQuery();


                    if (nLinhasAfetadas != 1)
                    {
                        response.Success = false;
                        response.Message = "Registro não encontrado";
                        return response;
                    }
                    response.Success = true;
                    response.Message = "Atualizado com Sucesso";

                }
                catch (Exception ex)
                {

                    response.Success = false;
                    response.Message = "Erro no Banco de Dados, contate o administrador.";


                    //Controle de LOG
                    response.StackTrace = ex.StackTrace;
                    response.ExceptionError = ex.Message;
                }
                finally
                {
                    connection.Close();
                }

                return response;

            }

            public Response Delete(Cliente cliente)
            {
                Response response = new Response();

                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = ConnectionHelper.GetConnectionString();

                SqlCommand command = new SqlCommand();

                command.CommandText = "DELETE FROM CLIENTES WHERE ID = @ID";


                command.Parameters.AddWithValue("@ID", cliente.ID);


                command.Connection = connection;

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    response.Success = true;
                    response.Message = "Cadastrado com sucesso";

                }
                catch (Exception ex)
                {

                    response.Success = false;
                    response.Message = "Erro no Banco de Dados, contate o administrador.";


                    //Controle de LOG
                    response.StackTrace = ex.StackTrace;
                    response.ExceptionError = ex.Message;
                }
                finally
                {
                    connection.Close();
                }

                return response;

            }

            public QueryResponse<Cliente> GetAll()
            {
                QueryResponse<Cliente> response = new QueryResponse<Cliente>();


                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = ConnectionHelper.GetConnectionString();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM CLIENTES"; //WHERE CPF = @CPF";

                //    command.Parameters.AddWithValue("@CPF", cliente)

                command.Connection = connection;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    List<Cliente> clientes = new List<Cliente>();

                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.ID = (int)reader["ID"]; // (int) -> cast = é tipo um convert so que mais otimizado
                        cliente.Nome = (string)reader["NOME"];
                        cliente.CPF = (string)reader["CPF"];
                        cliente.RG = (string)reader["RG"];
                        cliente.Telefone = (string)reader["TELEFONE"];
                        cliente.Celular = (string)reader["CELULAR"];
                        cliente.Email = (string)reader["EMAIL"];
                        cliente.DataCriacao = (DateTime)reader["DATACRIACAO"];


                        clientes.Add(cliente);
                    }
                    response.Success = true;
                    response.Message = "Dados selecionado com sucesso. ";
                    response.Data = clientes;
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

            public Response IsCpfUnique(string cpf)
            {

                QueryResponse<Cliente> response = new QueryResponse<Cliente>();

                SqlConnection connection = new SqlConnection();

                connection.ConnectionString = ConnectionHelper.GetConnectionString();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM CLIENTES WHERE CPF = @CPF";

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

        public SingleResponse<Cliente> GetById(int id)
        {
            SingleResponse<Cliente> response = new SingleResponse<Cliente>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConnectionHelper.GetConnectionString();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", id);
            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.ID = (int)reader["ID"];
                    cliente.Nome = (string)reader["NOME"];
                    cliente.CPF = (string)reader["CPF"];
                    cliente.RG = (string)reader["RG"];
                    cliente.Telefone = (string)reader["TELEFONE"];
                    cliente.Celular = (string)reader["CELULAR"];
                    cliente.Email = (string)reader["EMAIL"];
                    cliente.DataCriacao = (DateTime)reader["DATACRIACAO"];
                    response.Message = "Dados selecionados com sucesso.";
                    response.Success = true;
                    response.Data = cliente;
                    return response;
                }
                response.Message = "Cliente não encontrado.";
                response.Success = false;
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


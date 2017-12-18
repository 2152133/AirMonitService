using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using System.Data;

namespace AirMonit_SERVICE.Controllers
{
    public class UserReadsController : ApiController
    {
        SqlConnection conn = new SqlConnection("Server=8ed5b81c-f02d-4b07-8a1b-a82d01211ad0.sqlserver.sequelizer.com;Database=db8ed5b81cf02d4b078a1ba82d01211ad0;User ID=ymapojbsswihswrv;Password=eeXwreGgDWzeEK2hVnytXksn7gTeN5x3rirFr7i252gT58mh8nDDCwb2TKyjtCzG;");

        // GET: api/UserReads
        public IEnumerable<UserParameter> GetAllSensors()
        {
            List<UserParameter> lista = new List<UserParameter>();
            UserParameter value = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM UsersParameters;";
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    value = new UserParameter
                    (
                        (int)reader["Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"]
                    );
                    lista.Add(value);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                throw ex;
            }
            return lista;
        }

        // GET: api/UserReads/5
        public IHttpActionResult GetSensorById(int id)
        {
            UserParameter value = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM UsersParameters WHERE Id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    value = new UserParameter
                    (
                        (int)reader["Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"]
                    );
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                throw ex;
            }

            if (value == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(value);
            }
        }

        // POST: api/UserReads --> faz INSERT na database
        public IHttpActionResult Post([FromBody]UserParameter value)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO UsersParameters (Parameter, Read_Value, Location, DateTime) VALUES (@parametro, @valor_leitura, @localizacao, @data_hora);";
                cmd.Parameters.AddWithValue("@parametro", value.Parameter);
                cmd.Parameters.AddWithValue("@valor_leitura", value.ReadValue);
                cmd.Parameters.AddWithValue("@localizacao", value.Location);
                cmd.Parameters.AddWithValue("@data_hora", value.MomentOfRead);
                cmd.Connection = conn;

                conn.Open();

                int nRows = cmd.ExecuteNonQuery();

                conn.Close();
                if (nRows >= 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                throw ex;
            }

        }

        // DELETE: api/UserReads/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE UsersParameters WHERE Id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;

                int nRows = cmd.ExecuteNonQuery();

                conn.Close();
                if (nRows >= 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                throw ex;
            }
        }

    }

}

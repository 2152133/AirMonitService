using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace AirMonit_SERVICE.Controllers
{
    public class EventReadsController : ApiController
    {

        SqlConnection conn = new SqlConnection("Server=8ed5b81c-f02d-4b07-8a1b-a82d01211ad0.sqlserver.sequelizer.com;Database=db8ed5b81cf02d4b078a1ba82d01211ad0;User ID=ymapojbsswihswrv;Password=eeXwreGgDWzeEK2hVnytXksn7gTeN5x3rirFr7i252gT58mh8nDDCwb2TKyjtCzG;");

        // GET: api/SensorReads
        public IEnumerable<Event> GetAllSensors()
        {
            List<Event> lista = new List<Event>();
            Event natureEvent = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Events;";
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    natureEvent = new Event
                    (
                        (int)reader["Id"],
                        (int)reader["User_Id"],
                        (string)reader["Uncommon_event_description"],
                        (string)reader["City_name"],
                        (DateTime)reader["Date_Time"]
                    );
                    lista.Add(natureEvent);
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

        // GET: api/SensorReads/5
        public IHttpActionResult GetSensorById(int id)
        {
            Event natureEvent = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Events WHERE Id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    natureEvent = new Event
                    (
                        (int)reader["Id"],
                        (int)reader["User_Id"],
                        (string)reader["Uncommon_event_description"],
                        (string)reader["City_name"],
                        (DateTime)reader["Date_Time"]
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

            if (natureEvent == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(natureEvent);
            }
        }

        // POST: api/sensors --> faz INSERT na database
        public IHttpActionResult Post([FromBody]Event value)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Events (User_Id, Uncommon_event_description, City_name, Date_Time) VALUES (@user_id, @Uncommon_event_description, @city, @data_hora);";
                cmd.Parameters.AddWithValue("@user_id", value.UserId);
                cmd.Parameters.AddWithValue("@Uncommon_event_description", value.UncommonEventDescription);
                cmd.Parameters.AddWithValue("@city", value.CityName);
                cmd.Parameters.AddWithValue("@data_hora", value.MomentOfEvent);
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

        // PUT: api/sensors/5
        public IHttpActionResult Put(int id, [FromBody]Event value)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Events SET User_Id = @user_id, Uncommon_event_description = @Uncommon_event_description, City_name = @city, Date_Time = @data_hora WHERE Id=@id;";
                cmd.Parameters.AddWithValue("@user_id", value.UserId);
                cmd.Parameters.AddWithValue("@Uncommon_event_description", value.UncommonEventDescription);
                cmd.Parameters.AddWithValue("@city", value.CityName);
                cmd.Parameters.AddWithValue("@data_hora", value.MomentOfEvent);
                cmd.Parameters.AddWithValue("@id", id);
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

        // DELETE: api/sensors/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE Events WHERE Id=@id;";
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

        // GET: api/EventReads/city
        [Route("api/EventReads/city/{city}")]
        public IEnumerable<Event> GetEventsByCity(string city)
        {
            List<Event> lista = new List<Event>();
            Event natureEvent = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Events WHERE City_name = @city;";
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    natureEvent = new Event
                    (
                        (int)reader["Id"],
                        (int)reader["User_Id"],
                        (string)reader["Uncommon_event_description"],
                        (string)reader["City_name"],
                        (DateTime)reader["Date_Time"]
                    );
                    lista.Add(natureEvent);
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
    }
}

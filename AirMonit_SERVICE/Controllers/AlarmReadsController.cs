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
    public class AlarmReadsController : ApiController
    {
        //Connection to DB using the connection string
        SqlConnection conn = new SqlConnection("Server=8ed5b81c-f02d-4b07-8a1b-a82d01211ad0.sqlserver.sequelizer.com;Database=db8ed5b81cf02d4b078a1ba82d01211ad0;User ID=ymapojbsswihswrv;Password=eeXwreGgDWzeEK2hVnytXksn7gTeN5x3rirFr7i252gT58mh8nDDCwb2TKyjtCzG;");

         
        /// <summary>
        /// GET: api/AlarmReads
        /// Accesses the database through Rest Api 
        /// </summary>
        /// <returns>IEnumerable with all the alarms</returns>
        public IEnumerable<AlarmComplete> GetAllAlarms()
        {
            List<AlarmComplete> alarms = new List<AlarmComplete>();
            AlarmComplete alarm = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Alarms a join SensorsParameters sp on a.Sensor_Parameter_Id = sp.Read_Id;";
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    alarm = new AlarmComplete
                    (
                        (int)reader["Id"],
                        (int)reader["Sensor_Parameter_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"],
                        (string)reader["Alarm_Message"]
                    );
                    alarms.Add(alarm);
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
            return alarms;
        }


        /// <summary>
        /// GET: api/AlarmReads/5
        /// Accesses the database through Rest Api 
        /// </summary>
        /// <returns>IHttpActionResult 'OK' with the alarm with the given ID if it exists or IHttpActionResult 'Not Found' if it does not exists</returns>
        public IHttpActionResult GetAlarmById(int id)
        {
            AlarmComplete alarm = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Alarms a join SensorsParameters sp on a.Sensor_Parameter_Id = sp.Read_Id WHERE Id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    alarm = new AlarmComplete
                    (
                        (int)reader["Id"],
                        (int)reader["Sensor_Parameter_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"],
                        (string)reader["Alarm_Message"]
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

            if (alarm == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(alarm);
            }
        }


        /// <summary>
        /// PUT: api/AlarmReads/5
        /// Updates the database through Rest Api 
        /// </summary>
        /// <returns>IHttpActionResult 'OK' if the update was successful and IHttpActionResult 'Not Found' if the update failed</returns>
        public IHttpActionResult Put(int id, [FromBody]Alarm value)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Alarms SET Sensor_Parameter_Id = @sensor_parametro_id, Alarm_Message = @message WHERE Id=@id;";
                cmd.Parameters.AddWithValue("@sensor_parametro_id", value.SensorParameterId);
                cmd.Parameters.AddWithValue("@message", value.AlarmMessage);
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


        /// <summary>
        /// DELETE: api/AlarmReads/5
        /// Deletes a line from the database through Rest Api 
        /// </summary>
        /// <returns>IHttpActionResult 'OK' if the update was successful and IHttpActionResult 'Not Found' if the update failed</returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE Alarms WHERE Id=@id;";
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
        // GET: api/AlarmReads/date/20171130/city/leiria/
        [Route("api/AlarmReads/date/{date}/city/{city}")]
        public IEnumerable<AlarmComplete> GetDailyAlarmsByCity(string date, string city)
        {
            List<AlarmComplete> lista = new List<AlarmComplete>();
            AlarmComplete alarm = null;

            try
            {
                conn.Open();
                //11-30-2017
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "SELECT * FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = '20171130' AND Location = @city";
                cmd.CommandText = "SELECT * FROM SensorsParameters s JOIN Alarms a ON s.Read_Id=a.Sensor_Parameter_Id WHERE CONVERT(varchar(15), s.DateTime, 112) = @date AND s.Location = @city";
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    alarm = new AlarmComplete
                    (
                        (int)reader["Id"],
                        (int)reader["Sensor_Parameter_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"],
                        (string)reader["Alarm_Message"]
                    );
                    lista.Add(alarm);
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

        // GET: api/AlarmReads/city/leiria/
        [Route("api/AlarmReads/city/{city}")]
        public IEnumerable<AlarmComplete> GetAlarmsByCity(string city)
        {
            List<AlarmComplete> lista = new List<AlarmComplete>();
            AlarmComplete alarm = null;

            try
            {
                conn.Open();
                //11-30-2017
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "SELECT * FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = '20171130' AND Location = @city";
                cmd.CommandText = "SELECT * FROM SensorsParameters s JOIN Alarms a ON s.Read_Id=a.Sensor_Parameter_Id WHERE s.Location = @city";
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    alarm = new AlarmComplete
                    (
                        (int)reader["Id"],
                        (int)reader["Sensor_Parameter_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"],
                        (string)reader["Alarm_Message"]
                    );
                    lista.Add(alarm);
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

        // GET: api/AlarmReads/From/20171130/To/20191130
        [Route("api/AlarmReads/From/{date1}/To/{date2}")]
        public IEnumerable<AlarmComplete> GetAlarmsBetweenTwoDates(string date1, string date2)
        {
            List<AlarmComplete> lista = new List<AlarmComplete>();
            AlarmComplete alarm = null;

            try
            {
                conn.Open();
                //11-30-2017
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "SELECT * FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = '20171130' AND Location = @city";
                cmd.CommandText = "SELECT * FROM SensorsParameters s JOIN Alarms a ON s.Read_Id = a.Sensor_Parameter_Id WHERE CONVERT(varchar(15), s.DateTime, 112) BETWEEN @date1 AND @date2;";
                cmd.Parameters.AddWithValue("@date1", date1);
                cmd.Parameters.AddWithValue("@date2", date2);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    alarm = new AlarmComplete
                    (
                        (int)reader["Id"],
                        (int)reader["Sensor_Parameter_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"],
                        (string)reader["Alarm_Message"]
                    );
                    lista.Add(alarm);
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

        // GET: api/AlarmReads/From/20171130/To/20191130/city/Leiria
        [Route("api/AlarmReads/From/{date1}/To/{date2}/city/{city}")]
        public IEnumerable<AlarmComplete> GetAlarmsBetweenTwoDatesOnCity(string date1, string date2, string city)
        {
            List<AlarmComplete> lista = new List<AlarmComplete>();
            AlarmComplete alarm = null;

            try
            {
                conn.Open();
                //11-30-2017
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "SELECT * FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = '20171130' AND Location = @city";
                cmd.CommandText = "SELECT * FROM SensorsParameters s JOIN Alarms a ON s.Read_Id = a.Sensor_Parameter_Id WHERE CONVERT(varchar(15), s.DateTime, 112) BETWEEN @date1 AND @date2 AND s.Location = @city;";
                cmd.Parameters.AddWithValue("@date1", date1);
                cmd.Parameters.AddWithValue("@date2", date2);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    alarm = new AlarmComplete
                    (
                        (int)reader["Id"],
                        (int)reader["Sensor_Parameter_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"],
                        (string)reader["Alarm_Message"]
                    );
                    lista.Add(alarm);
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

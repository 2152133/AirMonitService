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
    public class SensorReadsController : ApiController
    {
        SqlConnection conn = new SqlConnection("Server=8ed5b81c-f02d-4b07-8a1b-a82d01211ad0.sqlserver.sequelizer.com;Database=db8ed5b81cf02d4b078a1ba82d01211ad0;User ID=ymapojbsswihswrv;Password=eeXwreGgDWzeEK2hVnytXksn7gTeN5x3rirFr7i252gT58mh8nDDCwb2TKyjtCzG;");

        // GET: api/SensorReads
        public IEnumerable<SensorParameter> GetAllSensors()
        {
            List<SensorParameter> lista = new List<SensorParameter>();
            SensorParameter sensor = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM SensorsParameters;";
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    sensor = new SensorParameter
                    (
                        (int)reader["Read_Id"],
                        (int)reader["Sensor_Id"],
                        (string)reader["Parameter"],
                        (int)reader["Read_Value"],
                        (string)reader["Location"],
                        (DateTime)reader["DateTime"]
                    );
                    lista.Add(sensor);
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

        // GET: api/SensorReads/5 (read_id)
        public IHttpActionResult GetReadById(int id)
        {
            SensorParameter sensor = null;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM SensorsParameters WHERE Read_Id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sensor = new SensorParameter
                    (
                        (int)reader["Read_Id"],
                        (int)reader["Sensor_Id"],
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

            if (sensor == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sensor);
            }
        }
        
        // DELETE: api/sensors/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE SensorsParameters WHERE Read_Id=@id;";
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

using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirMonit_SERVICE.Controllers
{
    public class StatisticsController : ApiController
    {
        SqlConnection conn = new SqlConnection("Server=8ed5b81c-f02d-4b07-8a1b-a82d01211ad0.sqlserver.sequelizer.com;Database=db8ed5b81cf02d4b078a1ba82d01211ad0;User ID=ymapojbsswihswrv;Password=eeXwreGgDWzeEK2hVnytXksn7gTeN5x3rirFr7i252gT58mh8nDDCwb2TKyjtCzG;");



        // GET: api/Statistics/date/20171130
        [Route("api/Statistics/date/{date}")]
        public IEnumerable<StatisticHour> GetStatisticsHourlyMaxMinAvgOnDate(string date)
        {
            List<StatisticHour> lista = new List<StatisticHour>();
            StatisticHour statistic = null;

            try
            {
                conn.Open();
                //20171130
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT DATEPART(HOUR, DateTime) AS HOUR, Parameter, MIN(Read_Value) AS MIN, MAX(Read_Value) AS MAX, AVG(Read_Value) AS AVG FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = @date GROUP BY DATEPART(HOUR, DateTime), Parameter;";
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statistic = new StatisticHour(
                        (int)reader["MIN"],
                        (int)reader["MAX"],
                        (int)reader["AVG"],
                        null,
                        (string)reader["Parameter"],
                        (int)reader["HOUR"]
                    );
                    lista.Add(statistic);
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

        // GET: api/Statistics/date/20171130
        [Route("api/Statistics/parameter/{parameter}/date/{date}")]
        public IEnumerable<StatisticHour> GetStatisticsHourlyMaxMinAvgOnParameterOnDate(string parameter, string date)
        {
            List<StatisticHour> lista = new List<StatisticHour>();
            StatisticHour statistic = null;

            try
            {
                conn.Open();
                //20171130
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT DATEPART(HOUR, DateTime) AS HOUR, Parameter, Location, MIN(Read_Value) AS MIN, MAX(Read_Value) AS MAX, AVG(Read_Value) AS AVG FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = @date AND Parameter = @parameter GROUP BY DATEPART(HOUR, DateTime), Parameter, Location;";
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@parameter", parameter);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statistic = new StatisticHour(
                        (int)reader["MIN"],
                        (int)reader["MAX"],
                        (int)reader["AVG"],
                        (string)reader["Location"],
                        (string)reader["Parameter"],
                        (int)reader["HOUR"]
                    );
                    lista.Add(statistic);
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

        // GET: api/Statistics/date/20171130/city/Leiria
        [Route("api/Statistics/date/{date}/city/{city}")]
        public IEnumerable<StatisticHour> GetStatisticsHourlyMaxMinAvgOnDateOnCity(string date, string city)
        {
            List<StatisticHour> lista = new List<StatisticHour>();
            StatisticHour statistic = null;

            try
            {
                conn.Open();
                //20171130
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT DATEPART(HOUR, DateTime) AS HOUR, Parameter, Location, MIN(Read_Value) AS MIN, MAX(Read_Value) AS MAX, AVG(Read_Value) AS AVG FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = @date AND Location = @city GROUP BY DATEPART(HOUR, DateTime), Parameter, Location;";
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statistic = new StatisticHour(
                        (int)reader["MIN"],
                        (int)reader["MAX"],
                        (int)reader["AVG"],
                        (string)reader["Location"],
                        (string)reader["Parameter"],
                        (int)reader["HOUR"]
                    );
                    lista.Add(statistic);
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
        
        // GET: api/Statistics/parameter/o3/date/20171130/city/Leiria
        [Route("api/Statistics/parameter/{parameter}/date/{date}/city/{city}")]
        public IEnumerable<StatisticHour> GetDailySensorMaxMinAvgOnParameterOnDateIOnCity(string parameter, string date, string city)
        {
            List<StatisticHour> lista = new List<StatisticHour>();
            StatisticHour statistic = null;
            
            try
            {
                conn.Open();
                //20171130
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT DATEPART(HOUR, DateTime) AS HOUR, Parameter, Location, MIN(Read_Value) AS MIN, MAX(Read_Value) AS MAX, AVG(Read_Value) AS AVG FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) = @date AND Location = @city AND Parameter = @parameter GROUP BY DATEPART(HOUR, DateTime), Parameter, Location;";
                cmd.Parameters.AddWithValue("@parameter", parameter);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statistic = new StatisticHour(
                        (int)reader["MIN"],
                        (int)reader["MAX"],
                        (int)reader["AVG"],
                        (string)reader["Location"],
                        (string)reader["Parameter"],
                        (int)reader["HOUR"]
                    );
                    lista.Add(statistic);
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

        // GET: api/Statistics/parameter/o3/From/20171130/To/20191130/city/Leiria
        [Route("api/Statistics/parameter/{parameter}/From/{date1}/To/{date2}/city/{city}")]
        public IEnumerable<StatisticDay> GetDailySensorMaxMinAvgOnParameterBetweenDatesOnCity(string parameter, string date1, string date2, string city)
        {
            List<StatisticDay> lista = new List<StatisticDay>();
            StatisticDay statistic = null;

            try
            {
                conn.Open();
                //20171130
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT DATEPART(DAY, DateTime) AS DAY, Parameter, Location, MIN(Read_Value) AS MIN, MAX(Read_Value) AS MAX, AVG(Read_Value) AS AVG FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) BETWEEN @date1 AND @date2 AND Location = @city AND Parameter = @parameter GROUP BY DATEPART(DAY, DateTime), Parameter, Location;";
                cmd.Parameters.AddWithValue("@parameter", parameter);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@date1", date1);
                cmd.Parameters.AddWithValue("@date2", date2);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statistic = new StatisticDay(
                        (int)reader["MIN"],
                        (int)reader["MAX"],
                        (int)reader["AVG"],
                        (string)reader["Location"],
                        (string)reader["Parameter"],
                        (int)reader["DAY"]
                    );
                    lista.Add(statistic);
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

        // GET: api/Statistics/parameter/o3/From/20171130/To/20191130
        [Route("api/Statistics/parameter/{parameter}/From/{date1}/To/{date2}")]
        public IEnumerable<StatisticDay> GetDailySensorMaxMinAvgOnParameterBetweenDates(string parameter, string date1, string date2)
        {
            List<StatisticDay> lista = new List<StatisticDay>();
            StatisticDay statistic = null;

            try
            {
                conn.Open();
                //20171130
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT DATEPART(DAY, DateTime) AS DAY, Parameter, Location, MIN(Read_Value) AS MIN, MAX(Read_Value) AS MAX, AVG(Read_Value) AS AVG FROM SensorsParameters WHERE CONVERT(varchar(15), DateTime, 112) BETWEEN @date1 AND @date2 AND Parameter = @parameter GROUP BY DATEPART(DAY, DateTime), Parameter, Location;";
                cmd.Parameters.AddWithValue("@parameter", parameter);
                cmd.Parameters.AddWithValue("@date1", date1);
                cmd.Parameters.AddWithValue("@date2", date2);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    statistic = new StatisticDay(
                        (int)reader["MIN"],
                        (int)reader["MAX"],
                        (int)reader["AVG"],
                        (string)reader["Location"],
                        (string)reader["Parameter"],
                        (int)reader["DAY"]
                    );
                    lista.Add(statistic);
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

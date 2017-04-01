using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ShallvaMailService.Models;

namespace ShallvaMailService
{
    public class GetEmailsByCriteriaAction
    {
        public List<EmailActivityModel> Run(EmailActiviyCriteriaModel criteria)
        {
            List<EmailActivityModel> results = new List<EmailActivityModel>();

            using (SqlConnection connection = new SqlConnection(Consts.CONNECTION_STRING))
            {
                // Create the command and set its properties.
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "GetEmailActivitiesByCriteria";
                command.CommandType = CommandType.StoredProcedure;

                #region Input parameters
                SqlParameter status = new SqlParameter();
                status.ParameterName = "@Status";
                status.SqlDbType = SqlDbType.TinyInt;
                status.Direction = ParameterDirection.Input;
                status.Value = (byte?)criteria.Status;
                status.IsNullable = true;

                SqlParameter lostEmails = new SqlParameter();
                lostEmails.ParameterName = "@LostEmails";
                lostEmails.SqlDbType = SqlDbType.Bit;
                lostEmails.Direction = ParameterDirection.Input;
                lostEmails.Value = criteria.LostEmails;
                lostEmails.IsNullable = true;

                SqlParameter fromDate = new SqlParameter();
                fromDate.ParameterName = "@FromDate";
                fromDate.SqlDbType = SqlDbType.DateTime;
                fromDate.Direction = ParameterDirection.Input;
                fromDate.Value = criteria.FromDate;
                fromDate.IsNullable = true;

                SqlParameter toDate = new SqlParameter();
                toDate.ParameterName = "@ToDate";
                toDate.SqlDbType = SqlDbType.DateTime;
                toDate.Direction = ParameterDirection.Input;
                toDate.Value = criteria.ToDate;
                toDate.IsNullable = true;

                #endregion

                // Add to the Parameters collection. 
                command.Parameters.Add(status);
                command.Parameters.Add(lostEmails);
                command.Parameters.Add(fromDate);
                command.Parameters.Add(toDate);

                // Open the connection and execute the reader.
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            Guid _id;
                            Guid.TryParse(reader[0].ToString(), out _id);
                            Guid _createdBy;
                            Guid.TryParse(reader[7].ToString(), out _createdBy);
                            DateTime _createdOn;
                            DateTime.TryParse(reader[6].ToString(), out _createdOn);
                            DateTime _sentOn;
                            DateTime.TryParse(reader[8].ToString(), out _sentOn);

                            results.Add(new EmailActivityModel
                            {
                                Id = _id,
                                Subject = !reader.IsDBNull(1) ? (string)reader[1] : string.Empty,
                                Body = !reader.IsDBNull(2) ? (string)reader[2] : string.Empty,
                                To = !reader.IsDBNull(3) ? (string)reader[3] : string.Empty,
                                CC = !reader.IsDBNull(4) ? (string)reader[4] : string.Empty,
                                BCC = !reader.IsDBNull(5) ? (string)reader[5] : string.Empty,
                                CreatedOn = _createdOn,
                                CreatedBy = _createdBy,
                                SentOn = _sentOn,
                                Status = !reader.IsDBNull(9) ? (byte)reader[9] : default(byte),
                                TryNumber = !reader.IsDBNull(10) ? (int)reader[10] : default(int),
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No rows found for {command.CommandType.ToString()}: {command.CommandText}.");
                    }
                }
            }

            int count = criteria.Size.HasValue ? criteria.Size.Value : Consts.MAX_SIZE;
            return results.Take(count).ToList();
        }

    }
}

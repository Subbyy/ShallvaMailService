using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ShallvaMailService.Models;

namespace ShallvaMailService
{
    public class UpdateEmailActivityAction
    {
        public void Run(string connectionString, EmailActivityModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the command and set its properties.
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "UpdateEmailActivity";
                command.CommandType = CommandType.StoredProcedure;

                #region Input parameters
                SqlParameter id = new SqlParameter();
                id.ParameterName = "@Id";
                id.SqlDbType = SqlDbType.UniqueIdentifier;
                id.Direction = ParameterDirection.Input;
                id.Value = model.Id;
                id.IsNullable = false;

                SqlParameter sentOn = new SqlParameter();
                sentOn.ParameterName = "@SentOn";
                sentOn.SqlDbType = SqlDbType.DateTime;
                sentOn.Direction = ParameterDirection.Input;
                sentOn.Value = model.SentOn;
                sentOn.IsNullable = true;

                SqlParameter status = new SqlParameter();
                status.ParameterName = "@Status";
                status.SqlDbType = SqlDbType.TinyInt;
                status.Direction = ParameterDirection.Input;
                status.Value = (byte)model.Status;
                status.IsNullable = false;

                SqlParameter tryNumber = new SqlParameter();
                tryNumber.ParameterName = "@TryNumber";
                tryNumber.SqlDbType = SqlDbType.Int;
                tryNumber.Direction = ParameterDirection.Input;
                tryNumber.Value = model.TryNumber;
                tryNumber.IsNullable = true;

                #endregion

                // Add to the Parameters collection. 
                command.Parameters.Add(id);
                command.Parameters.Add(sentOn);
                command.Parameters.Add(status);
                command.Parameters.Add(tryNumber);

                // Open the connection and execute the reader.
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine($" - Update action for email: {model.Id} complete successfuly");
                }
            }

        }

    }
}

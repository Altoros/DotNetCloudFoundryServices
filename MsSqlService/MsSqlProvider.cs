using CloudFoundryServiceBroker.Interfaces;
using CloudFoundryServiceBroker.Interfaces.Entities;
using MsSqlService.Properties;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MsSqlService
{
    public class MsSqlProvider
    {
        private readonly string _connectionString;
        private static readonly Random _rnd = new Random((int)DateTime.Now.Ticks);

        public MsSqlProvider()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[Constants.AppKeys.CloudFoundryConnectionString].ConnectionString;
        }

        public void CreateDatabase(string databaseName)
        {
            CheckInputParams(databaseName);

            var databaseNameBase64 = ConvertToBase64(databaseName);
            var query = string.Format(Constants.SqlTemplates.CreateDB, databaseNameBase64);

            using (var sqlConn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open();
                    var codeResult = (int)cmd.ExecuteScalar();

                    if (codeResult == -1)
                    {
                        throw new ConflictException(string.Format(Constants.ErrorMessageTemplates.DatabaseAlreadyExists, databaseNameBase64));
                    }
                }
            }
        }

        public Credentials CreateUserForDatabase(string databaseName, string userName)
        {
            CheckInputParams(databaseName, userName);

            var databaseNameBase64 = ConvertToBase64(databaseName);
            var userNameBase64 = ConvertToBase64(userName);
            var userPassword = GetRandomString(16);

            using (var sqlConn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(Constants.SqlStroredProcedures.CreateUserForDatabase, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(Constants.SqlParameters.UserLogin, SqlDbType.NVarChar).Value = userNameBase64;
                    cmd.Parameters.Add(Constants.SqlParameters.UserPassword, SqlDbType.NVarChar).Value = userPassword;
                    cmd.Parameters.Add(Constants.SqlParameters.DbName, SqlDbType.NVarChar).Value = databaseNameBase64;
                    cmd.Parameters.Add(Constants.SqlParameters.Result, SqlDbType.Int).Direction = ParameterDirection.Output;

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();

                    var result = (int)cmd.Parameters[Constants.SqlParameters.Result].Value;
                    if (result == -1)
                    {
                        throw new ConflictException(string.Format(Constants.ErrorMessageTemplates.UserAlreadyExists, userNameBase64));
                    }
                }
            }

            var dataSource = Settings.Default.SqlServerUrl;
            var port = Settings.Default.SqlServerPort;

            return new Credentials(userNameBase64, userPassword, dataSource, port, databaseNameBase64);
        }

        public void RemoveUserFromDatabase(string databaseName, string userName)
        {
            CheckInputParams(databaseName, userName);

            var databaseNameBase64 = ConvertToBase64(databaseName);
            var userNameBase64 = ConvertToBase64(userName);

            using (var sqlConn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(Constants.SqlStroredProcedures.DropUserFromDatabase, sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(Constants.SqlParameters.UserLogin, SqlDbType.NVarChar).Value = userNameBase64;
                    cmd.Parameters.Add(Constants.SqlParameters.DbName, SqlDbType.NVarChar).Value = databaseNameBase64;
                    cmd.Parameters.Add(Constants.SqlParameters.Result, SqlDbType.Int).Direction = ParameterDirection.Output;

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();

                    var codeResult = (int)cmd.Parameters[Constants.SqlParameters.Result].Value;
                    if (codeResult == -1)
                    {
                        throw new NotFoundException(string.Format(Constants.ErrorMessageTemplates.UserNotFound, userNameBase64));
                    }
                }
            }
        }

        public void DropDatabase(string databaseName)
        {
            CheckInputParams(databaseName);

            var databaseNameBase64 = ConvertToBase64(databaseName);

            var query = string.Format(Constants.SqlTemplates.DropDB, databaseNameBase64);

            using (var sqlConn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open();
                    var codeResult = (int)cmd.ExecuteScalar();

                    if (codeResult == -1)
                    {
                        throw new NotFoundException(string.Format(Constants.ErrorMessageTemplates.DatabaseNotFound, databaseNameBase64));
                    }
                }
            }
        }

        #region Private methods

        private void CheckInputParams(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(databaseName, Constants.ErrorMessages.DatabaseNameCantBeNull);
            }
        }

        private void CheckInputParams(string databaseName, string userName)
        {
            CheckInputParams(databaseName);

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(userName, Constants.ErrorMessages.UserNameCantBeNull);
            }
        }

        private string ConvertToBase64(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        private string GetRandomString(int size)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";

            var buffer = new char[size];

            buffer[0] = chars[_rnd.Next(chars.Length)];
            var symbols = string.Concat(chars, digits);

            for (int i = 1; i < size; i++)
            {
                buffer[i] = symbols[_rnd.Next(symbols.Length)];
            }

            return new string(buffer);
        }

        #endregion
    }
}

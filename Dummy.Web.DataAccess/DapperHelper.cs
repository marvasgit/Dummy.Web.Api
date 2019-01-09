namespace Dummy.Web.DataAccess
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using Dapper;
    using Dummy.Web.DataAccess.Common;
    using static Dapper.SqlMapper;

    public class DapperHelper : IDapperHelper
    {
        private readonly string _connectionString = string.Empty;

        public DapperHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public T ExecuteStoredProcedure<T>(CommandType commandType, string commandText, DynamicParameters commandParameters)
        {
            var triesToConnectCount = 0;
            SetUpCustomPropertyMapping<T>();

            while (true)
            {
                try
                {
                    using (var connection = OpenConnection())
                    {
                        return connection.ExecuteScalar<T>(commandText,
                                                                commandParameters,
                                                                commandType: commandType,
                                                                commandTimeout: SqlHelperConstants.TimeOutInSeconds);

                    }
                }
                catch (SqlException sqlException)
                {

                    if (sqlException.Number == SqlExceptionsConstants.CanNotObtainLockExceptionCode
                        || sqlException.Number == SqlExceptionsConstants.DeadlockExceptionCode)
                    {
                        triesToConnectCount++;

                        if (triesToConnectCount <= SqlHelperConstants.MaxTriesToConnect)
                        {
                            Thread.Sleep(SqlHelperConstants.MillisecondsToWait);
                        }
                        else
                        {
                            throw sqlException;
                        }
                    }
                    else
                    {
                        throw sqlException;
                    }
                }
            }
        }

        public IList<T> ExecuteDataset<T>(CommandType commandType, string storedProcedureName)
        {
            var triesToConnectCount = 0;
            SetUpCustomPropertyMapping<T>();

            while (true)
            {
                try
                {
                    using (var connection = OpenConnection())
                    {
                        return connection.Query<T>(storedProcedureName,
                                                              commandType: commandType,
                                                              commandTimeout: SqlHelperConstants.TimeOutInSeconds)
                                                              .ToList();

                    }
                }
                catch (SqlException sqlException)
                {

                    if (sqlException.Number == SqlExceptionsConstants.CanNotObtainLockExceptionCode
                        || sqlException.Number == SqlExceptionsConstants.DeadlockExceptionCode)
                    {
                        triesToConnectCount++;

                        if (triesToConnectCount <= SqlHelperConstants.MaxTriesToConnect)
                        {
                            Thread.Sleep(SqlHelperConstants.MillisecondsToWait);
                        }
                        else
                        {
                            throw sqlException;
                        }
                    }
                    else
                    {
                        throw sqlException;
                    }
                }
            }
        }

        private void SetUpCustomPropertyMapping<T>()
        {
            SetTypeMap(
                typeof(T),
                new CustomPropertyTypeMap(
                    typeof(T),
                    (type, columnName) =>
                        type.GetProperties().FirstOrDefault(prop =>
                            prop.GetCustomAttributes(true)
                                .OfType<ColumnAttribute>()
                                .Any(attr => attr.Name == columnName))));
        }

        private SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}

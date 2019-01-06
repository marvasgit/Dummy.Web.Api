using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Dummy.Web.DataAccess
{
    public interface IDapperHelper
    {
        T ExecuteStoredProcedure<T>(CommandType storedProcedure, string storedProcedureName, DynamicParameters commandParameters);
        IList<T> ExecuteDataset<T>(CommandType storedProcedure, string storedProcedureName);
    }
}
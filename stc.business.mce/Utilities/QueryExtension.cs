using Core.DataAccess.Extentions;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace stc.business.mce
{
    public static class QueryExtension
    {
        public static Dictionary<string, string> ToKeyPairs(this object obj)
        {
            var dictionary = new Dictionary<string, string>();
            var properties = obj.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(obj)?.ToString());
            }

            return dictionary;
        }

        //public static async Task<int> ExecuteStoredProcPgSql1(this IDbConnection connection, string procName, DynamicParameters parameters, string resultParam, IDbTransaction tran = null)
        //{
        //    connection.Reconnect();
        //    IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
        //    try
        //    {
        //        parameters.Add(resultParam, 0);
        //        string query = procName.ToPostgresStoredStatement(parameters, null);
        //        CommandType? commandType = CommandType.Text;
        //        IEnumerable<int> result = await (await connection.QueryMultipleAsync(query, parameters, transaction, null, commandType)).ReadAsync<int>();
        //        if (tran == null)
        //        {
        //            transaction.Commit();
        //        }

        //        return result.First();
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        if (ex.Message.Contains("terminating connection due to administrator command"))
        //        {
        //            return await connection.ExecuteStoredProcPgSql(procName, parameters, resultParam, tran);
        //        }

        //        transaction?.Rollback();
        //        throw ex;
        //    }
        //    catch
        //    {
        //        transaction?.Rollback();
        //        throw;
        //    }
        //}

        //public static async Task<SqlMapper.GridReader> QueryMultiStoredProcPgSql1(this IDbConnection connection, string procName, DynamicParameters parameters, params string[] resultParams)
        //{
        //    connection.Reconnect();
        //    try
        //    {
        //        string query = procName.ToPostgresStoredStatement(parameters, resultParams);
        //        CommandType? commandType = CommandType.Text;
        //        SqlMapper.GridReader result = await connection.QueryMultipleAsync(query, parameters, null, null, commandType);
        //        await result.ReadAsync<object>();
        //        return result;
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        if (ex.Message.Contains("terminating connection due to administrator command"))
        //        {
        //            return await connection.QueryMultiStoredProcPgSql(procName, parameters, resultParams);
        //        }

        //        throw ex;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProductionDatabase
{
    public static class Utility
    {
        public static bool Company_Update_Share_3M(string symbol, string connection_string)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("symbol", symbol);

                return Library.Database.ExecuteProcedure.Post("[fsn].[Company_Update_Share_3M]", connection_string, param);
            } catch (Exception ex)
            {
                /* staticmethodfullname */
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;
                Library.Logger.Log_Error(fullMethodName, "Error", ex.Message);

                return false;
            }
        }
    }
}

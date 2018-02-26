﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public static class DatabaseUpdateHelper
    {
        

        // here
        public static int Share_Insert_Update(string _ticker, int _alpha)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ticker", _ticker);
                param.Add("alpha", _alpha);

                string result = Library.Database.ExecuteProcedure_Get(
                    "[dbo].[Company_IEXTrading_Insert_Update]"
                    , param
                    , ConnectionString.Get());
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
                //this.Robinhood = !this.Robinhood;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("DatabaseUpdateHelper.Update_Company_IEXTrading(" + _ticker + ") failed. Message: " + ex.Message);
            }

            return 1;
        }

        public static int Update_Company_IEXTrading(string _ticker, int _alpha)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ticker", _ticker);
                param.Add("alpha", _alpha);

                string result = Library.Database.ExecuteProcedure_Get(
                    "[dbo].[Company_IEXTrading_Insert_Update]"
                    , param
                    , ConnectionString.Get());
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
                //this.Robinhood = !this.Robinhood;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("DatabaseUpdateHelper.Update_Company_IEXTrading(" + _ticker + ") failed. Message: " + ex.Message);
            }

            return 1;
        }

        public static int Update_WebApi_AlphaVantage(string _ticker, int _alpha)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ticker", _ticker);
                param.Add("alpha", _alpha);

                string result = Library.Database.ExecuteProcedure_Get(
                    "[dbo].[Company_WebApi_AlphaVantage_Insert_Update]"
                    , param
                    , ConnectionString.Get());
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
                //this.Robinhood = !this.Robinhood;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("DatabaseUpdateHelper.Update_WebApi_AlphaVantage(" + _ticker + ") failed. Message: " + ex.Message);
            }

            return 1;
        }

            public static int Update_Robinhood(string _ticker)
        {

            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Ticker", _ticker);
                string result = Library.Database.ExecuteProcedure_Get(
                    "[dbo].[Company_Update_Robinhood]"
                    , param
                    , ConnectionString.Get());
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
                //this.Robinhood = !this.Robinhood;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("DatabaseUpdateHelper.Update_Robinhood(" + _ticker + ") failed. Message: " + ex.Message);
            }

            return 1;

        }
    }
}

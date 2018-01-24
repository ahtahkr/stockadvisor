using System;

namespace AustinsFirstProject.CoreLibrary.Database
{
    public class Share
    {
        private const string DB_STORED_PROCEDURE_GET = "[dbo].[Share_Insert_Update]";

        public int ID { get; set; }
        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public int volume { get; set; }
        public string Ticker { get; set; }
    }
}

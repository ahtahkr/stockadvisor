﻿using System.ServiceProcess;

namespace Stock_Advisor_Windows_Service
{
    class Program
    {
        static void Main()
        {
            /*
            #if (!DEBUG)
                       ServiceBase[] ServicesToRun;
                       ServicesToRun = new ServiceBase[] 
                   { 
                        new Service1()
                   };
                       ServiceBase.Run(ServicesToRun);
            #else
                        Service1 myServ = new Service1();
             //myServ.IEXTrading_Get_Previous();
             //myServ.Process_File();
             //myServ.IEXTrading_Get_Symbol_ChartRange();
             //myServ.Process_File();
             myServ.Send_Email();
            // here Process is my Service function
            // that will run when my service onstart is call
            // you need to call your own method or function name here instead of Process();
#endif
            */

            
             ServiceBase[] ServicesToRun;
             ServicesToRun = new ServiceBase[]
             {
                new Service1()
             };
             ServiceBase.Run(ServicesToRun);
             
        }
    }
}

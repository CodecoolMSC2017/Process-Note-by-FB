using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Process_Note
{
    class MyProcess
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string CPU_usage { get; set; }
        public string Memory_usage { get; set; }
        public string Running_time {get;set;}
        public string Start_time { get; set; }
        public int Number_of_threads { get; set; }
        public string Comment { get; set; }

        public MyProcess( Process process)
        {
            this.Name = process.ProcessName;
            this.Id = process.Id.ToString();
            this.CPU_usage = new PerformanceCounter("Process", "% Processor Time", process.ProcessName).NextValue() + " %"; ;
            this.Memory_usage = (process.PrivateMemorySize64 / 1024.0 / 1024.0).ToString("#,##0") + " MB";
            try
            {
                this.Running_time = (DateTime.Now - process.StartTime).ToString();
            } catch(Exception ex)
            {
                this.Running_time = "Not available!";
            }
            try
            {
                this.Start_time = process.StartTime.ToString();
            }
            catch (Exception e)
            {
                this.Start_time = "Not available!";
            }
            this.Number_of_threads = process.Threads.Count;
            
        }
    }



}

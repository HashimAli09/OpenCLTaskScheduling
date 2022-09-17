using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerConsole
{
    public enum ProcessorType
    {
        CPU,
        GPU
    }
    public class MockProcessor
    {
        public static MockProcessor Create(float baseClockSpeed, int noOfCores, ProcessorType type)
        {
            //List<Core> cores = new List<Core>();
            //for (int i = 0; i < noOfCores; i++)
            //{
            //    cores.Add(new Core(i, baseClockSpeed));
            //}
            return new MockProcessor
            {
                Type = type,
                ClockFrequency = baseClockSpeed,
                TotalCores = noOfCores
            };
        }

        /// <summary>
        /// Processing speed of the processor in Intrusction processed per second
        /// </summary>
        public float SpeedIPS => ClockFrequency * TotalCores;

        public float ClockFrequency { get; set; }

        //public List<Core> Cores { get; set; }
        public int TotalCores { get; set; }

        public ProcessorType Type { get; set; }

        public float ExecuteAsync(List<Job> jobPool)
        {
            float totalExeTime = 0;
            int i = 0;
            try
            {
                while (i < jobPool.Count)
                {
                    var jobsBatch = jobPool.GetRange(i, Math.Min(TotalCores, jobPool.Count - i));
                    totalExeTime += jobsBatch[0].NumberOfInstructions / ClockFrequency;
                    i += jobsBatch.Count;
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                Debugger.Break();
            }
            

            return totalExeTime;
        }
    }

    //public class Core
    //{
    //    public float ClockSpeed { get; set; }

    //    public int Id { get; private set; }

    //    public int TasksInQueue { get; private set; }

    //    public bool IsBusy { get; private set; }

    //    public Core(int id, float clockSpeed)
    //    {
    //        ClockSpeed = clockSpeed;
    //        Id = id;
    //        TasksInQueue = 0;
    //    }

    //    public async Task<float> ExecuteAsync(Job job)
    //    {
    //        float busyWaitDealy = 0;
    //        if (IsBusy) 
    //        {
    //            TasksInQueue++;
    //            busyWaitDealy = await WaitAsync();
    //            TasksInQueue--;
    //        }

    //        IsBusy = true;

    //        float exeTime = job.NumberOfInstructions / ClockSpeed;
    //        await Task.Delay(TimeSpan.FromMilliseconds(exeTime));

    //        IsBusy = false;
    //        return exeTime + busyWaitDealy;
    //    }

    //    public async Task<float> WaitAsync() 
    //    {
    //        float start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    //        while (IsBusy) 
    //        {
    //            await Task.Delay(1);
    //        }
    //        return DateTimeOffset.Now.ToUnixTimeMilliseconds() - start;
    //    }
    //}

    public class Job 
    {
        public int Id { get; set; }

        public long NumberOfInstructions { get; set; }
    }
}

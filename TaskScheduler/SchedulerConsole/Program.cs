using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerConsole
{
    public class Program
    {
        static MockProcessor cpu = MockProcessor.Create(3 * 1000, 4, ProcessorType.CPU);
        static MockProcessor gpu = MockProcessor.Create(900, 1000, ProcessorType.GPU);

        static async Task Main(string[] args)
        {

            Console.WriteLine("*****Fixed Instructions Size****");
            await SimulateJobPoolExecutionAsync(10, 1000000000);
            await SimulateJobPoolExecutionAsync(50, 1000000000);
            await SimulateJobPoolExecutionAsync(100, 1000000000);
            await SimulateJobPoolExecutionAsync(250, 1000000000);
            await SimulateJobPoolExecutionAsync(500, 1000000000);
            await SimulateJobPoolExecutionAsync(1000, 1000000000);
            await SimulateJobPoolExecutionAsync(1200, 1000000000);
            await SimulateJobPoolExecutionAsync(1500, 1000000000);
            await SimulateJobPoolExecutionAsync(2000, 1000000000);

            Console.WriteLine("\n\n*****Fixed Job Size****");
            await SimulateJobPoolExecutionAsync(100, 1000);
            await SimulateJobPoolExecutionAsync(100, 10000);
            await SimulateJobPoolExecutionAsync(100, 100000);
            await SimulateJobPoolExecutionAsync(100, 1000000);
            await SimulateJobPoolExecutionAsync(100, 10000000);
            await SimulateJobPoolExecutionAsync(100, 100000000);
            await SimulateJobPoolExecutionAsync(100, 1000000000);

            Console.ReadLine();
        }

        public static async Task SimulateJobPoolExecutionAsync(int jobSize, long insSize)
        {
            var jobPool = CreateJobPool(jobSize, insSize);

            Console.WriteLine($"\nSimulating JobPool=[TotalJobs={jobPool.Count}, NoOfInstructions={insSize}]");
            //Console.Write("\tExecuting All on CPU....");

            float cpuElapsed = await ExecuteAllOnAsync(cpu, jobPool);
            //Console.WriteLine($" took={elapsed} ms");

            //Console.Write("\tExecuting All on GPU....");
            float gpuElapsed = await ExecuteAllOnAsync(gpu, jobPool);
            //Console.WriteLine($" took={elapsed} ms");

            //Console.Write("\tWork item Guided....");
            //await ExecuteWorkItemGuidedAsync(jobPool);

            Console.WriteLine($"AllOnCPU={cpuElapsed / 1000} s, AllOnGPU={gpuElapsed / 1000} s");
        }

        //public static List<Job> CreateJobPool(int size, long minInsSize, long maxInsSize)
        public static List<Job> CreateJobPool(int size, long insSize)
        {
            List<Job> jobs = new List<Job>();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                jobs.Add(new Job
                {
                    Id = i,
                    NumberOfInstructions = insSize,
                });
            }
            return jobs;
        }

        public static async Task<float> ExecuteAllOnAsync(MockProcessor processor, List<Job> jobPool)
        {
            await Task.Delay(10);
            return processor.ExecuteAsync(jobPool);

            //List<Task> tasks = new List<Task>();
            //float total = 0;
            //foreach (var job in jobPool)
            //{
            //    tasks.Add(processor.ExecuteAsync(job));
            //    //tasks.Add(Task.Run(async () => 
            //    //{
            //    //    float exeTime = await processor.ExecuteAsync(job);
            //    //    total += exeTime;
            //    //}));
            //}
            //total = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //await Task.WhenAll(tasks);
            //return DateTimeOffset.Now.ToUnixTimeMilliseconds() - total;
        }

        //static int i, j;
        //public static async Task ExecuteWorkItemGuidedAsync(List<Job> jobPool)
        //{
        //    List<Job> sortedJobs = jobPool.OrderByDescending(x => x.NumberOfInstructions).ToList();
        //    i = 0;
        //    j = sortedJobs.Count - 1;

        //    List<Task> tasks = new List<Task>
        //    {
        //        Task.Run(async () =>
        //        {
        //            while (i < j)
        //            {
        //                var jobsToExecute = sortedJobs.GetRange(i, cpu.Cores.Count);
        //                await ExecuteAllOnAsync(cpu, jobsToExecute);
        //                i += cpu.Cores.Count;
        //            }
        //        }),
        //        Task.Run(async () =>
        //        {
        //            while (j > i)
        //            {
        //                var jobsToExecute = sortedJobs.GetRange(j - gpu.Cores.Count, gpu.Cores.Count);
        //                await ExecuteAllOnAsync(gpu, jobsToExecute);
        //                j -= gpu.Cores.Count;
        //            }

        //        })
        //    };
        //    await Task.WhenAll(tasks);
        //}

    }
}

using LiveCharts.Wpf;
using SchedulerConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MockProcessor cpu = MockProcessor.Create(3 * 1000, 4, ProcessorType.CPU);
        MockProcessor gpu = MockProcessor.Create(500, 1000, ProcessorType.GPU);

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 100; i++) 
            {
                RootPanel.Children.Add(new CartesianChart());
            }

            Task.Run(async () => 
            {
                List<int> jobs;
                //if (args.Length == 0)
                //{
                //    jobs = CreateJobPool(10000, 10000, 20000);
                //    CommitJobPoolToFile(jobs);
                //}
                //else
                //{
                //    jobs = ReadJobPool();
                //}

                Console.WriteLine("*****Fixed Instructions Size****");
                //    await SimulateJobPoolExecutionAsync(10, 1000000000);
                //    await SimulateJobPoolExecutionAsync(50, 1000000000);
                //    await SimulateJobPoolExecutionAsync(100, 1000000000);
                //    await SimulateJobPoolExecutionAsync(250, 1000000000);
                //    await SimulateJobPoolExecutionAsync(500, 1000000000);
                //    await SimulateJobPoolExecutionAsync(1000, 1000000000);
                //    await SimulateJobPoolExecutionAsync(1200, 1000000000);
                //    await SimulateJobPoolExecutionAsync(1500, 1000000000);
                //    await SimulateJobPoolExecutionAsync(2000, 1000000000);

                Console.WriteLine("\n\n*****Fixed Job Size****");
                //    await SimulateJobPoolExecutionAsync(100, 1000);
                //    await SimulateJobPoolExecutionAsync(100, 10000);
                //    await SimulateJobPoolExecutionAsync(100, 100000);
                //    await SimulateJobPoolExecutionAsync(100, 1000000);
                //    await SimulateJobPoolExecutionAsync(100, 10000000);
                //    await SimulateJobPoolExecutionAsync(100, 100000000);
                //    await SimulateJobPoolExecutionAsync(100, 1000000000);

            });

        }

        void CommitJobPoolToFile(List<int> jobPool)
        {
            string path = string.Format($"{Environment.CurrentDirectory}/res/jobpool.txt");
            var file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
                file.Create();
            }
            File.WriteAllLines(path, jobPool.Select(x => x.ToString()));
        }

        List<int> ReadJobPool()
        {
            string path = string.Format($"{Environment.CurrentDirectory}/res/jobpool.txt");
            var file = new FileInfo(path);

            if (!file.Exists)
            {
                return CreateJobPool(10000, 10000, 20000);
            }
            List<int> jobs = new List<int>();
            foreach (var line in File.ReadAllLines(path))
            {
                jobs.Add(int.Parse(line));
            }
            return jobs;
        }

        public async Task SimulateJobPoolExecutionAsync(int jobSize, long insSize)
        {
            var jobPool = CreateJobPool(jobSize, insSize);

            Console.WriteLine($"\nSimulating JobPool=[TotalJobs={jobPool.Count}, NoOfInstructions={insSize}]");
            //Console.Write("\tExecuting All on CPU....");

            //float cpuElapsed = await ExecuteAllOnAsync(cpu, jobPool);
            //Console.WriteLine($" took={elapsed} ms");

            //Console.Write("\tExecuting All on GPU....");
            //float gpuElapsed = await ExecuteAllOnAsync(gpu, jobPool);
            //Console.WriteLine($" took={elapsed} ms");

            //Console.Write("\tWork item Guided....");
            //await ExecuteWorkItemGuidedAsync(jobPool);

            //Console.WriteLine($"AllOnCPU={cpuElapsed / 1000} s, AllOnGPU={gpuElapsed / 1000} s");
        }

        public List<int> CreateJobPool(int size, int minInsSize, int maxInsSize)
        {
            List<int> jobs = new List<int>();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                jobs.Add(random.Next(minInsSize, maxInsSize));
            }
            return jobs;
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

        //public static async Task<float> ExecuteAllOnAsync(MockProcessor processor, List<int> jobPool)
        //{
        //    await Task.Delay(10);
        //    return processor.ExecuteAsync(jobPool);

        //    //List<Task> tasks = new List<Task>();
        //    //float total = 0;
        //    //foreach (var job in jobPool)
        //    //{
        //    //    tasks.Add(processor.ExecuteAsync(job));
        //    //    //tasks.Add(Task.Run(async () => 
        //    //    //{
        //    //    //    float exeTime = await processor.ExecuteAsync(job);
        //    //    //    total += exeTime;
        //    //    //}));
        //    //}
        //    //total = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        //    //await Task.WhenAll(tasks);
        //    //return DateTimeOffset.Now.ToUnixTimeMilliseconds() - total;
        //}
    }
}

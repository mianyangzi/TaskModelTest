using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskModelTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var tasks = new Task[10];
            for (int i = 0; i< 10; i++)
            {
                tasks[i] = Task.Factory.StartNew((m) =>
                {
                    Run(source.Token);
                }, i);
            }
            Task.WhenAll(tasks).ContinueWith((t) =>
            { 
                Console.WriteLine("准备退出了。。。");
                Console.Read();
                Environment.Exit(0);
            });
            string input = Console.ReadLine();
            while ("Y".Equals(input, StringComparison.OrdinalIgnoreCase))
            {
                source.Cancel();
            }
            Console.Read();
        }
        static void Run(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) break;
                Thread.Sleep(1000);

                Console.WriteLine("我是线程:{0},正在执行业务逻辑", Thread.CurrentThread.ManagedThreadId);

            }
        }
    }
}
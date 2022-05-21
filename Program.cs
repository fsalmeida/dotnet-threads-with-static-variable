using System;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_threads_with_static_variable
{
    class Program
    {
        static double IDADE = 0;

        static async Task Main(string[] args)
        {
            IDADE = 30;
            var taskGetText1 = Task.Run(() => PrintAge("Minha idade é"));

            Thread.Sleep(100);
            IDADE = 90;
            var taskGetText2 =
                Task.Run(() => PrintAge("A idade da minha vó é"));

            await Task.WhenAll(taskGetText1, taskGetText2);
        }

        static async Task PrintAge(string prefix)
        {
            System.Threading.Thread.Sleep(300);
            Console.WriteLine($"{prefix}: {IDADE}");
        }
    }
}

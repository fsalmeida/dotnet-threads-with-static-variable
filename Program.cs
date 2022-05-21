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
            var taskImprimeMinhaIdade = Task.Run(() => ImprimeIdade("Minha idade é"));

            Thread.Sleep(100);
            IDADE = 90;
            var taskImprimeIdadeAvo = Task.Run(() => ImprimeIdade("A idade da minha vó é"));

            await Task.WhenAll(taskImprimeMinhaIdade, taskImprimeIdadeAvo);
        }

        static async Task ImprimeIdade(string prefixo)
        {
            System.Threading.Thread.Sleep(300);
            Console.WriteLine($"{prefixo}: {IDADE} anos.");
        }
    }
}

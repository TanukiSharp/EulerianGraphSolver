using System;

namespace EulerianGraphConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            var graph = new EulerianGraph<string>();

            // replace the following with you own graph
            graph
                .Connect("A", "B", "C")
                .Connect("B", "A", "C")
                .Connect("C", "A", "B", "D", "E", "F", "G", "J", "H")
                .Connect("D", "C", "E")
                .Connect("E", "C", "D")
                .Connect("F", "C", "I", "H")
                .Connect("G", "C", "I", "J")
                .Connect("H", "C", "F", "I", "N")
                .Connect("I", "F", "G", "H", "J", "L", "M")
                .Connect("J", "C", "G", "I", "K")
                .Connect("K", "J", "L")
                .Connect("L", "I", "K")
                .Connect("M", "I", "N")
                .Connect("N", "H", "M")
                .Done();

            var trail = graph.Solve();

            foreach (var v in trail)
                Console.WriteLine(v);

            Console.WriteLine();
            Console.WriteLine("Done!");
        }
    }
}


namespace Kruskal_Algo
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            int V = 4;
            int E = 5;

            Graph graph = new Graph(V, E);

            graph.ReadGraphEdges();

            graph.KruskalMST();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kruskal_Algo
{
    public class Graph
    {
        private int verticesNum, edgesNum;

        Edge[] edges;
        public Graph(int v, int e)
        {
            verticesNum = v;
            edgesNum = e;

            edges = new Edge[edgesNum];
            for (int i = 0; i < edgesNum; i++)
                edges[i] = new Edge();
        }

        class Edge : IComparable<Edge>
        {
            private int src, dst, weight;

            public int Source 
            {
                get => src;
                set => src = value;
            }
            public int Destination 
            {
                get => dst;
                set => dst = value;
            }
            public int Weight 
            { 
                get => weight;
                set => weight = value;
            }

            public int CompareTo(Edge obj)
            {
                return this.weight - obj.Weight;
            }
        }

        class Subset
        {
            private int parent, rank;

            public int Parent 
            { 
                get => parent;
                set => parent = value;
            }
            public int Rank 
            { 
                get => rank;
                set => rank = value; 
            }
        }
        
        int _Find(Subset[] subsets, int index)
        {
            if (subsets[index].Parent != index)
                subsets[index].Parent = _Find(subsets, subsets[index].Parent);

            return subsets[index].Parent;
        }
        void _Union(Subset[] subsets, int x, int y)
        {
            int xRoot = _Find(subsets, x);
            int yRoot = _Find(subsets, y);

            if (subsets[xRoot].Rank < subsets[yRoot].Rank)
                subsets[xRoot].Parent = yRoot;
            else if (subsets[xRoot].Rank > subsets[yRoot].Rank)
                subsets[yRoot].Parent = xRoot;
            else
            {
                subsets[yRoot].Parent = xRoot;
                subsets[xRoot].Rank++;
            }

        }
        public void KruskalMST()
        {
            Edge[] result = new Edge[verticesNum];

            int e = 0;

            int i;
            for (i = 0; i < verticesNum; i++)
                result[i] = new Edge();

            Array.Sort(edges);

            Subset[] subsets = new Subset[verticesNum];
            for (i = 0;i < verticesNum; i++)
                subsets[i] = new Subset();

            for (i = 0;i < verticesNum; i++)
            {
                subsets[i].Parent = i;
                subsets[i].Rank = 0;
            }

            i = 0;
            while (e < verticesNum - 1)
            {
                var NextEdge = new Edge();
                NextEdge = edges[i++];

                int x = _Find(subsets, NextEdge.Source);
                int y = _Find(subsets, NextEdge.Destination);

                if (x != y)
                {
                    result[e++] = NextEdge;
                    _Union(subsets, x, y);
                }            
            }

            Console.WriteLine("Following are the edges in the constructed MST\n");

            int minimumCost = 0;
            for (i = 0; i < e; i++)
            {
                Console.WriteLine($"{result[i].Source} -- {result[i].Destination} == {result[i].Weight}");

                minimumCost += result[i].Weight;
            }

            Console.WriteLine($"\nMinimum Cost Spanning Tree: {minimumCost}");

        }
        public void ReadGraphEdges()
        {
            for (int i = 0; i < edgesNum; i++)
            {
                edges[i] = new Edge();
                Console.WriteLine($"\nEdge {i+1}\n_____________________________________");
                Console.Write("Source of Edge Number : ");
                edges[i].Source = int.Parse(Console.ReadLine());
                Console.Write("Destination of Edge Number : ");
                edges[i].Destination = int.Parse(Console.ReadLine());
                Console.Write("Weight of Edge Number : ");
                edges[i].Weight = int.Parse(Console.ReadLine());
                Console.WriteLine("_____________________________\n");
            }
        }
    }
}

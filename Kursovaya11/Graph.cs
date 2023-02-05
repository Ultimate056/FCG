using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace Kursovaya11
{
    class Graph
    {
        List<Vertex> X = new List<Vertex>();
        public List<Edge> E = new List<Edge>();

        public int VertexCount => X.Count;
        public int EdgeCount => E.Count;
        public void AddVertex(Vertex vertex)
        {
            X.Add(vertex);
        }
        public void AddEdge(Vertex from, Vertex to)
        {
            var edge = new Edge(from, to);
            E.Add(edge);
        }
  
        public List<Edge> GetKNF()
        {
            List<Edge> T = new List<Edge>();
            for (int i = 0; i < EdgeCount; i++)
            {
                if(E[i].Weight == 1)
                {
                    T.Add(E[i]);
                }
            }
            return T;
        }
        public int[,] GetMatrix() // Матрица смежности
        {
            var matrix = new int[X.Count, X.Count];

            foreach(var edge in E)
            {
                var row = edge.From.Name-1;
                var col = edge.To.Name-1;
                matrix[row, col] = edge.Weight;
            }
            return matrix;
        }
    
        public List<string> GetInternalSets(string[] bo)
        {
            List<string> vnutr = new List<string>();
            foreach (var zh in bo)
            {
                if (!zh.Contains('1') && !zh.Contains('2') && !zh.Contains('3') && zh.Contains('4'))
                {
                    vnutr.Add("(X1^X2^X3)");
                    continue;
                }
                if (!zh.Contains('1') && !zh.Contains('2') && !zh.Contains('4') && zh.Contains('3'))
                {
                    vnutr.Add("(X1^X2^X4)");
                    continue;
                }
                if (!zh.Contains('1') && !zh.Contains('3') && !zh.Contains('4') && zh.Contains('2'))
                {
                    vnutr.Add("(X1^X3^X4)");
                    continue;
                }
                if (!zh.Contains('2') && !zh.Contains('3') && !zh.Contains('4') && zh.Contains('1'))
                {
                    vnutr.Add("(X2^X3^X4)");
                    continue;
                }
                if (!zh.Contains('1') && !zh.Contains('2') && zh.Contains('3') && zh.Contains('4'))
                {
                    vnutr.Add("(X1^X2)");
                    continue;
                }
                if (!zh.Contains('1') && !zh.Contains('3') && zh.Contains('2') && zh.Contains('4'))
                {
                    vnutr.Add("(X1^X3)");
                    continue;
                }
                if (!zh.Contains('1') && !zh.Contains('4') && zh.Contains('2') && zh.Contains('3'))
                {
                    vnutr.Add("(X1^X4)");
                    continue;
                }
                if (!zh.Contains('2') && !zh.Contains('3') && zh.Contains('1') && zh.Contains('4'))
                {
                    vnutr.Add("(X2^X3)");
                    continue;
                }
                if (!zh.Contains('2') && !zh.Contains('4') && zh.Contains('1') && zh.Contains('3'))
                {
                    vnutr.Add("(X2^X4)");
                    continue;
                }
                if (!zh.Contains('3') && !zh.Contains('4') && zh.Contains('1') && zh.Contains('2'))
                {
                    vnutr.Add("(X3^X4)");
                    continue;
                }
                if (!zh.Contains('1') && zh.Contains('2') && zh.Contains('3') && zh.Contains('4'))
                {
                    vnutr.Add("X1");
                    continue;
                }
                if (!zh.Contains('2') && zh.Contains('1') && zh.Contains('3') && zh.Contains('4'))
                {
                    vnutr.Add("X2");
                    continue;
                }
                if (!zh.Contains('3') && zh.Contains('1') && zh.Contains('2') && zh.Contains('4'))
                {
                    vnutr.Add("X3");
                    continue;
                }
                if (!zh.Contains('4') && zh.Contains('1') && zh.Contains('2') && zh.Contains('3'))
                {
                    vnutr.Add("X4");
                    continue;
                }
            }
            return vnutr;
        }
        public string GetExternalDisjuncts(int[,] matrix2)
        {
            string Ddd = "";
            for (int i = 0; i < matrix2.GetLength(0); i++)
            {
                Ddd += "(";
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    if (matrix2[i, j] == 1)
                    {
                        if (j == 0)
                        {
                            Ddd += X[0].GetNameVertex();
                            Ddd += "V";
                        }
                        if (j == 1)
                        {
                            Ddd += X[1].GetNameVertex();
                            Ddd += "V";
                        }
                        if (j == 2)
                        {
                            Ddd += X[2].GetNameVertex();
                            Ddd += "V";
                        }
                        if (j == 3)
                        {
                            Ddd += X[3].GetNameVertex();
                            Ddd += "V";
                        }
                    }
                }
                Ddd = Ddd.Substring(0, Ddd.Length - 1);
                Ddd += ")";
            }
            return Ddd;
        }
    }
}

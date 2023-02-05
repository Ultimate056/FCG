using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Kursovaya11
{
    class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public int Weight { get; set; }

        public Edge(Vertex from, Vertex to, int weight = 1)
        {
            Weight = weight;
            From = from;
            To = to;
        }
        public override string ToString()
        {
            return $"(X{From} V X{To})";
        }
        public string ToStringCon()
        {
            return $"(X{From} Y X{To})";
        }
        public static List<Edge>RemoveIdential(List<Edge> T)
        {
            for (int i = 0; i < T.Count; i++)
            {
                for (int j = i + 1; j < T.Count; j++)
                {
                    if ((T[i].From == T[j].To && T[i].To == T[j].From) || (T[i].From == T[j].From && T[i].To == T[j].To))
                    {
                        T.RemoveAt(j);
                        j--;
                    }
                }
            }
            return T;
        }
    }
}

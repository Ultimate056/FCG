using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace Kursovaya11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Курсовая программа");
            Console.WriteLine("Задача программы: найти ядро невзвешенного графа");
            bool workProgram = true; 
            List<Vertex> ListVertex = new List<Vertex>();
            var graph = new Graph();     
            while(workProgram)
            {
                Console.WriteLine("\tМеню программы:");
                Console.WriteLine("\t_______________________");
                Console.WriteLine("\t1. Добавить вершину");
                Console.WriteLine("\t2. Добавить ребро");
                Console.WriteLine("\t3. Найти ядро графа");
                Console.WriteLine("\t4. Повторить программу");
                Console.WriteLine("\t5. Завершить программу");
                uint total_choise = uint.Parse(Console.ReadLine());
                switch(total_choise)
                {
                    case 1:
                        Console.WriteLine("Введите номер вершины: ");
                        uint number_vertex = uint.Parse(Console.ReadLine());
                        if (number_vertex < 0 || number_vertex > 8)
                            throw new OverflowException("Номер вершины не может быть отрицательным или больше 8");
                        bool ThereIsVertex = false;
                        if(ListVertex != null)
                        {
                            foreach (var item in ListVertex)
                            {
                                if (item.Name == number_vertex)
                                {
                                    ThereIsVertex = true;
                                    Console.WriteLine("Такая вершина уже есть! Введите другую вершину");
                                    Console.ReadKey();
                                }
                            }
                        }
                        if(!ThereIsVertex)
                            ListVertex.Add(new Vertex(number_vertex));
                        break;
                    case 2:
                        bool ThereIsVertexFrom = false;
                        bool ThereIsVertexTo = false;
                        bool EdgeOriented = true; // По умолчанию ребро ориентированное
                        var VertexFrom = new Vertex();
                        var VertexTo = new Vertex();
                        Console.WriteLine("Ребро будет ориентированное или неориентированное? (1 - ориентированное, 0 - неориентированное): ");
                        int input = int.Parse(Console.ReadLine());
                        if (input == 0)
                            EdgeOriented = false;
                        Console.WriteLine("Введите номер соединяющей вершины(откуда): ");
                        uint number_vertex_from = uint.Parse(Console.ReadLine());
                        Console.WriteLine("Введите номер соединяемой вершины(куда): ");
                        uint number_vertex_to = uint.Parse(Console.ReadLine());
                        foreach (var item in ListVertex)
                        {
                            if (item.Name == number_vertex_from)
                                ThereIsVertexFrom = true;
                            if (item.Name == number_vertex_from)
                                ThereIsVertexTo = true;
                            if (ThereIsVertexFrom && ThereIsVertexTo)
                                break;
                        }
                        if(ThereIsVertexFrom && ThereIsVertexTo)
                        {
                            VertexFrom = new Vertex(number_vertex_from);
                            VertexTo = new Vertex(number_vertex_to);
                            graph.AddEdge(VertexFrom, VertexTo);
                            if (!EdgeOriented)
                            {
                                graph.AddEdge(VertexTo, VertexFrom);
                            }
                        }
                        break;
                    case 3:
                        foreach(var item in ListVertex)
                        {
                            graph.AddVertex(item);
                        }
                        Console.WriteLine("Часть 1:");
                        Console.WriteLine("Определяем множества внутренней устойчивости...");
                        Console.WriteLine("Матрица смежности графа: ");
                        var matrix = graph.GetMatrix();
                        for (int i = 0; i < graph.VertexCount; i++) 
                        {
                            for (int j = 0; j < graph.VertexCount; j++)
                            {
                                Console.Write(matrix[i, j] + " ");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("Составляем парные дизъюнкты:");
                        List<Edge> bul_func = graph.GetKNF(); // список ребер
                        Edge.RemoveIdential(bul_func); // метод по удалению одинаковых булевых выражений
                        Console.Write("     ");
                        foreach (var zh in bul_func)
                        {
                            Console.Write(" " + zh);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Преобразуйте данное выражение КНФ в ДНФ ,а затем МДНФ и введите результат. Пример: (X1^X2^X3)V(X4^X1)V(X1^X3)");
                        Console.Write(": ");
                        string DNF = Console.ReadLine();
                        string[] DNFInternals = DNF.Split(new char[] { 'V' });
                        List<string> InternalSets = graph.GetInternalSets(DNFInternals);
                        Console.WriteLine("Множества внутренней устойчивости: ");
                        foreach (var zm in InternalSets)
                        {
                            Console.Write(zm + " ");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Часть 2:");
                        Console.WriteLine("Определяем множества внешней устойчивости... ");
                        Console.WriteLine("Матрица смежности c дополненными единицами: ");
                        int[,] matrix2 = matrix;
                        for (int i = 0; i < graph.VertexCount; i++) // Матрица дополняется единицами по главной диагонали
                        {
                            for (int j = 0; j < graph.VertexCount; j++)
                            {
                                if (i == j)
                                {
                                    matrix2[i, j] = 1;
                                    foreach (var item in ListVertex)
                                    {
                                        if (j == item.Name)
                                        {
                                            graph.AddEdge(item, item);
                                            break;
                                        }
                                    }
                                }
                                Console.Write(matrix2[i, j] + " ");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("Все дизъюнкты построчно: ");
                        string Ddd = graph.GetExternalDisjuncts(matrix2); // Выписываются дизъюкнты построчно
                        Console.WriteLine(Ddd);
                        Console.WriteLine();
                        Console.WriteLine("Преобразуйте выражение выше в ДНФ. Пример: (X1^X2^X3)V(X4^X1)V(X1^X3)");
                        Console.Write(": ");
                        string dnff = Console.ReadLine();
                        string[] boo = dnff.Split(new char[] { 'V' });
                        Console.WriteLine("Множества внешней устойчивости: ");
                        foreach (var zz in boo)
                        {
                            Console.Write(zz + " ");
                        }
                        List<string> Cores = new List<string>();
                        foreach (var vn in InternalSets)
                        {
                            for (int i = 0; i < boo.Length; i++)
                            {
                                if (vn == boo[i])
                                {
                                    Cores.Add(vn);
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("!!!!!-----------!!!!!!");
                        Console.Write("Ядро(а) графа: ");
                        foreach (var core in Cores)
                        {
                            Console.Write(core + " ");
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        ListVertex = new List<Vertex>();
                        graph = new Graph();
                        Console.Clear();
                        break;
                    case 5:
                        workProgram = false;
                        break;
                    default:
                        Console.WriteLine("Вы ввели неправильный номер. Повторите попытку или завершите программу");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();

            }
        
            Console.ReadKey();
        }

    }
}

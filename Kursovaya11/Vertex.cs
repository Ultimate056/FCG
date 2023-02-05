using System;

namespace Kursovaya11
{
    class Vertex // Вершина
    {
        public Vertex(uint name)
        {
            Name = name;
        }
        public Vertex() { }
        public uint Name { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
        public string GetNameVertex()
        {
            return $"X{Name}";
        }
    }
}

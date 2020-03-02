//cs
namespace Cs.Base
{
    using System.Collections.Generic;

    public class csBase
    {
        public string getName = "something";

        private double _age = 20.0;
        public double getAge
        {
            get { return _age; }
            set { _age = value; }
        } 
        public List<string> getInterests = new List<string>{ "F#", "C#", "Vb", "Java", "Scala", "Python", "Javascript" };
        public int Add(int x, int y) 
        {
            return x + y;
        }
    }
}
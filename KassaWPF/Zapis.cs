using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaWPF
{
    internal class Zapis
    {
        public string Name { get; set; }
        public string TypeOf { get; set; }
        private int sum;
        public int Sum
        {
            get
            {
                return sum;
            }
            set
            {
                if(value < 0)
                {
                    sum = value * -1;
                    isPOs = false;
                }
                else
                {
                    sum = value;
                    isPOs = true;
                }
            }
        }
        public bool isPOs { get; set; }
        public DateTime data;
        public Zapis(string name, string typeOf, int sum, DateTime data)
        {
            Name = name;
            TypeOf = typeOf;
            Sum = sum;
            this.data = data;
        }
    }
}

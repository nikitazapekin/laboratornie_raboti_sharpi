 

 using System;
using System.Collections.Generic;
 
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab9Itog.Classes
{



  public  class Element

    {
        private string name;
        private int inputCount;
        private int outputCount;

        public Element() { }

        public Element(string name, int inputCount = 1, int outputCount = 1)
        {
            this.name = name;
            this.inputCount = inputCount;
            this.outputCount = outputCount;
        }
 



        public string Name => name; 
        public int InputCount
        {
            get => inputCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество входов не может быть отрицательным.");
                inputCount = value;
            }
        }
        public int OutputCount
        {
            get => outputCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество выходов не может быть отрицательным.");
                outputCount = value;
            }
        }


        public virtual void SetInputs(int[] inputs)
        {
            throw new NotImplementedException("This element type does not support setting inputs.");
        }
        //  public   int ComputeOutput();

        public int ComputeOutput()
        {
            // Здесь может быть логика для вычисления выхода. Например:
            return 0;  // Это заглушка, замените на свою логику
        }


        public override bool Equals(object obj)
    {
      
        if (ReferenceEquals(this, obj))
            return true;

        
        if (obj == null || GetType() != obj.GetType())
            return false;

        
        Element other = (Element)obj;
        return name == other.name &&
               inputCount == other.inputCount &&
               outputCount == other.outputCount;
    }
        public virtual void Invert()
        {
            throw new NotImplementedException("Invert not implemented for this element.");
        }



       

    }



}

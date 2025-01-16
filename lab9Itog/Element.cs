

using Lab9Itog.Interfaces;
using System;


 
    public abstract class Element : IInvertible

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
 
        public virtual void Invert()
        {
            throw new NotImplementedException("Invert not implemented for this element.");
        }

        public abstract int  ComputeOutput();


    public virtual void RandomSetInputs()
    {
        throw new NotImplementedException("This element type does not support setting inputs.");
    }
 
    public virtual string ToBinaryString()
    {
        throw new NotImplementedException();
    }
    public virtual void FromBinaryString(string dataString)
    {
        throw new NotImplementedException();
    }



}

 
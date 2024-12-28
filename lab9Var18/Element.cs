using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;

public interface IInvertible
{
    void Invert();


}

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
                throw new ArgumentException("Error");
            inputCount = value;
        }
    }
    public int OutputCount
    {
        get => outputCount;
        set
        {
            if (value < 0)
                throw new ArgumentException("Error");
            outputCount = value;
        }
    }



    public virtual void SetInput(int  inputs)
    {
        throw new ArgumentException("Error setInput");
    }

    public virtual void Invert()
    {
       

         throw new ArgumentException("Error");
    }

    public abstract int ComputeOutput();





    public virtual string ToBinaryString()
    {
        throw new NotImplementedException();
    }
    public virtual void FromBinaryString(string dataString)
    {
        throw new NotImplementedException();
    }



}


/*
 * 
namespace lab9Var18
{
    class Element
    {
    }
}
*/
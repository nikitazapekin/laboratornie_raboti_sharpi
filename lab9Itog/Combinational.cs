using System;
using System.IO;

public class Combinational : Element, IComparable<Combinational>
{
   
   public int[] inputs;


    public int[] Inputs
    {
        get { return inputs; }
        set { inputs = value; }
    }

    public Combinational() : base("Combinational", 2, 1)
    {
        inputs = new int[InputCount]; 
    }

    public Combinational(int inputCount) : base("Combinational", inputCount, 1)
    {
        inputs = new int[inputCount];
    }
 

    public void SetInputs(int[] inputValues)
    {
        if (inputValues.Length != InputCount)
        {
            throw new ArgumentException("Количество значений входов не совпадает с количеством входов элемента.");
        }
        inputs = inputValues;
    }
 
    public int[] GetInputs()
    {
        return inputs;
    }
 
    public override void Invert()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
           
            inputs[i] = (inputs[i] == 0) ? 1 : 0;
        }
    }

   
    public int GetInputState(int index)
    {
        if (index < 0 || index >= InputCount)
        {
            throw new ArgumentOutOfRangeException("Неверный индекс входа.");
        }
        return inputs[index];
    }

   
    public override int ComputeOutput()
    {
        int result = inputs[0];  
        for (int i = 1; i < inputs.Length; i++)
        {
        
            result ^= inputs[i];
        }
        return result;
    }










 
    public override string ToBinaryString()
    {
        using (var ms = new MemoryStream())
        using (var writer = new BinaryWriter(ms))
        {
            writer.Write(inputs.Length);
            foreach (var value in inputs)
            {
                writer.Write(value);
            }

            return Convert.ToBase64String(ms.ToArray());
        }
    }


    public override void FromBinaryString(string dataString)
    {
        var data = Convert.FromBase64String(dataString);
        using (var ms = new MemoryStream(data))
        using (var reader = new BinaryReader(ms))
        {


            int inputValuesLength = reader.ReadInt32();
            for (int i = 0; i < inputValuesLength; i++)
            {
                inputs[i] = reader.ReadInt32();
            }
        }
    }
 
   

   



    public override void RandomSetInputs()
    {
        Random random = new Random();

        
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = random.Next(0, 2);  
        }
    }







    public int CountOnesOnOutputs()
    {
        int count = 0;
        foreach (var input in inputs)
        {
            if (input == 1)
            {
                count++;
            }
        }
        return count;
    }



    public int CompareTo(Combinational other)
    {
        if (other == null) return 1;

       
        return this.CountOnesOnOutputs().CompareTo(other.CountOnesOnOutputs());
    }

    public static bool operator <(Combinational a, Combinational b)
    {
        if (a == null || b == null)
            throw new ArgumentNullException("Нельзя сравнивать с null.");
        
        return a.ComputeOutput() < b.ComputeOutput();
    }

    public static bool operator >(Combinational a, Combinational b)
    {
        if (a == null || b == null)
            throw new ArgumentNullException("Нельзя сравнивать с null.");
     
        return a.ComputeOutput() > b.ComputeOutput();
    }
    public static bool operator ==(Combinational a, Combinational b)
    {
        if (ReferenceEquals(a, b)) return true;
        if ((object)a == null || (object)b == null) return false;
        return a.ComputeOutput() == b.ComputeOutput();
    }

    public static bool operator !=(Combinational a, Combinational b)
    {
        return !(a == b);
    }

 
    public override bool Equals(object obj)
    {
        if (obj is Combinational other)
        {
            return this.ComputeOutput() == other.ComputeOutput();
        }
        return false;
    }


}

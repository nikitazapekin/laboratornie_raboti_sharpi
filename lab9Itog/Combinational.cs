using System;
using System.IO;

public class Combinational : Element
{
   
    private int[] inputs;

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

 

    public void SaveToFile(string fileName)
    {
        try
        {
            var binaryData = ToBinaryData();  
            File.WriteAllBytes(fileName, binaryData); 
        }
        catch (Exception ex)
        {
            throw new IOException($"Ошибка при сохранении файла: {ex.Message}", ex);
        }
    }


    public override string ToString()
    {
        string result = "[";
        for (int i = 0; i < inputs.Length; i++)
        {
            result += inputs[i];
            if (i < inputs.Length - 1)
            {
                result += ", ";
            }
        }
        result += "]";
        return result;
    }











    public byte[] ToBinaryData()
    {
        using (var ms = new MemoryStream())
        using (var writer = new BinaryWriter(ms))
        {
            writer.Write(inputs.Length);
            for (int i = 0; i < inputs.Length; i++)
            {
                writer.Write(inputs[i]);
            }

            writer.Flush();
            return ms.ToArray();
        }
    }

    public static Combinational FromBinaryData(byte[] data)
    {
        using (var ms = new MemoryStream(data))
        using (var reader = new BinaryReader(ms))
        {
            int inputValuesLength = reader.ReadInt32();
            var combinational = new Combinational(inputValuesLength);
            for (int i = 0; i < inputValuesLength; i++)
            {
                combinational.inputs[i] = reader.ReadInt32();
            }

            return combinational;
        }
    }

    public override string ToBinaryString()
    {
        return Convert.ToBase64String(ToBinaryData());
    }

    public override void FromBinaryString(string dataString)
    {
        var data = Convert.FromBase64String(dataString);
        using (var ms = new MemoryStream(data))
        using (var reader = new BinaryReader(ms))
        {
            int inputValuesLength = reader.ReadInt32();
            inputs = new int[inputValuesLength];
            for (int i = 0; i < inputValuesLength; i++)
            {
                inputs[i] = reader.ReadInt32();
            }
        }
    }
    

}

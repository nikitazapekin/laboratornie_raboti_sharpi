﻿using System;
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
 
   

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Combinational)obj;

     
        if (Name != other.Name || InputCount != other.InputCount)
            return false;

       
        return true;
    }



}

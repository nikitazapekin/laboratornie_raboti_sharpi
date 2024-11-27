﻿using System;
using System.IO;

public class Memory : Element
{ 
    private int[] inputValues;

 
    private int directOutput;  
    private int invertedOutput; 

 
    private int setInput;
    private int resetInput;
 
    public Memory(int inputCount = 1)
        : base("Memory", inputCount + 1, 1)
    {
        inputValues = new int[inputCount + 1];
        directOutput = 0;
        invertedOutput = 1;
        setInput = 0;
        resetInput = 0;
    }

    public int getState()
    {
        return inputValues[1];
    }

   
    public Memory(Memory other) : base(other.Name, other.InputCount, other.OutputCount)
    {
        inputValues = (int[])other.inputValues.Clone();
        directOutput = other.directOutput;
        invertedOutput = other.invertedOutput;
        setInput = other.setInput;
        resetInput = other.resetInput;
    }
  
   
    public void SetInputs(int[] inputs)
    {
    

        if (inputs.Length != InputCount)
            throw new ArgumentException($"Expected {InputCount} inputs.");
        if (inputs[1] == 0)
        {
            inputValues[1] = 0;
        }
        else
        {
            inputValues = inputs;
        }
    }
    

 

    public int GetInputState(int index)
    {
        if (index >= 0 && index < inputValues.Length)
        {
            return inputValues[index];
        }
        else
        {
            throw new ArgumentOutOfRangeException("Неверный индекс входа");
        }
    }

 
    public void ComputeState()
    {
        
        if (setInput == 1)
        {
            directOutput = 1;
            invertedOutput = 0;
        }
    
        else if (resetInput == 1)
        {
            directOutput = 0;
            invertedOutput = 1;
        }
        else
        {
         
            directOutput = inputValues[0];
            invertedOutput = 1 - directOutput;
        }
    }


    public override bool Equals(object obj)
    {
        if (obj is Memory other)
        {
            if (this.directOutput != other.directOutput) return false;
      

            for (int i = 0; i < inputValues.Length; i++)
            {
                if (this.inputValues[i] != other.inputValues[i]) return false;
            }

            return true;
        }
        return false;
    }

    public int[] GetAllInputs()
    {
        return inputValues;
    }

    public override int ComputeOutput()
    {
        return directOutput;
    }


    public int DirectOutput => directOutput;
    public int InvertedOutput => invertedOutput;

    public int SetInput
    {
        get => setInput;
        set => setInput = value;
    }

    public int ResetInput
    {
        get => resetInput;
        set => resetInput = value;
    }

  
    public override void Invert()
    {
        if (inputValues[1] == 1)
        {
            inputValues[0] = inputValues[0] == 0 ? 1 : 0;

            directOutput = directOutput == 0 ? 1 : 0;

            invertedOutput = invertedOutput == 0 ? 1 : 0;
        }
    }



    /*
    public void SaveToFile(string fileName)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
         
            writer.Write(InputCount);

            
            for (int i = 0; i < inputValues.Length; i++)
            {
                writer.Write(inputValues[i]);
            }

          
            writer.Write(directOutput);
            writer.Write(invertedOutput);

          
            writer.Write(setInput);
            writer.Write(resetInput);
        }
    }
    */




    public override string ToBinaryString()
    {
        using (var ms = new MemoryStream())
        using (var writer = new BinaryWriter(ms))
        {
            writer.Write(inputValues.Length);
            foreach (var value in inputValues)
            {
                writer.Write(value);
            }

            writer.Write(directOutput);
            writer.Write(invertedOutput);

            return Convert.ToBase64String(ms.ToArray());
        }
    }




}
 
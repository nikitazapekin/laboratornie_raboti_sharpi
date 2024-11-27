using lab9Itog.Interfaces;
using System;
using System.IO;
using System.Windows;

public class Register : Element, IShiftable
{
    private static int ResetState = 0; 
    private static int SetState = 0;  
    private Memory[] memories;       
    private int[][] inputs;           

    public Register(int size) : base("Register", size, size)
    {
        memories = new Memory[size];
        inputs = new int[size][];

        for (int i = 0; i < size; i++)
        {
            memories[i] = new Memory(); 
            inputs[i] = new int[2];   
        }
    }

 

    public void SetInputs(int[][] inputValues)
    {
        if (inputValues.Length != memories.Length)
            throw new ArgumentException($"Expected {memories.Length} sets of inputs.");

        for (int i = 0; i < memories.Length; i++)
        {
            if (inputValues[i].Length != 2)
                throw new ArgumentException($"Each memory requires exactly 2 inputs (data and clock).");

            inputs[i] = inputValues[i];
        }
    }


    public int[][] GetInputs()
    {
        return inputs;
    }


    public int GetInputState(int memoryIndex, int inputIndex)
    {
        if (memoryIndex < 0 || memoryIndex >= memories.Length)
            throw new ArgumentOutOfRangeException(nameof(memoryIndex), "Invalid memory index.");
        if (inputIndex < 0 || inputIndex >= 2)
            throw new ArgumentOutOfRangeException(nameof(inputIndex), "Invalid input index.");

        return inputs[memoryIndex][inputIndex];
    }

    public void ComputeState()
    {
        for (int i = 0; i < memories.Length; i++)
        {
            if (SetState == 1)
            {
                memories[i].SetInputs(new int[] { 1, inputs[i][1] });
            }
            else if (ResetState == 1)
            {
                memories[i].SetInputs(new int[] { 0, inputs[i][1] });
            }
            else
            {
                memories[i].SetInputs(inputs[i]);
            }

            memories[i].ComputeState();
        }
    }

    public override int ComputeOutput()
    {
     
        int output = 0;
        for (int i = 0; i < memories.Length; i++)
        {
            output |= memories[i].DirectOutput << i;
        }
        return output;
    }

    public override void Invert()
    {
        foreach (var memory in memories)
        {
            memory.Invert();
        }
    }

    public int[] GetRegisterState()
    {
        int[] states = new int[memories.Length];
        for (int i = 0; i < memories.Length; i++)
        {
            states[i] = memories[i].DirectOutput;
        }
        return states;
    }

    public static void SetGlobalState(int set, int reset)
    {
        SetState = set;
        ResetState = reset;
    }


 
    public void Shift(int positions)
    {
        if (positions <= 0)
            throw new ArgumentException("Количество позиций для сдвига должно быть положительным числом.");

        
        positions %= inputs[0].Length;
        if (positions == 0) return;

       
        int[][] shiftedInputs = new int[inputs.Length][];
        for (int i = 0; i < inputs.Length; i++)
        {
            shiftedInputs[i] = new int[inputs[i].Length];  
            for (int j = 0; j < inputs[i].Length; j++)
            {
              
                int newIndex = (j + positions) % inputs[i].Length;
                shiftedInputs[i][newIndex] = inputs[i][j];
            }
        }
        inputs = shiftedInputs;

       
    }



    public override string ToBinaryString()
    {
        using (var ms = new MemoryStream())
        using (var writer = new BinaryWriter(ms))
        {
            writer.Write(inputs.Length);
            foreach (var value in inputs)
            {
                writer.Write(value.ToString());
            }

           

            return Convert.ToBase64String(ms.ToArray());
        }
    }




}

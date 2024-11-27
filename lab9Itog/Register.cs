using System;

public class Register : Element
{
    private static int ResetState = 0; // Состояние сброса
    private static int SetState = 0;   // Состояние установки
    private Memory[] memories;        // Массив памяти
    private int[][] inputs;           // Массивы входов для каждой ячейки памяти

    public Register(int size) : base("Register", size, size)
    {
        memories = new Memory[size];
        inputs = new int[size][];

        for (int i = 0; i < size; i++)
        {
            memories[i] = new Memory(); // Инициализируем каждую ячейку памяти
            inputs[i] = new int[2];     // У каждой ячейки два входа: data и clock
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
        // Пример: возвращаем "сумму" выходов всех триггеров
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
}


/*
 * using System;
using System.Linq;
using System.Windows;

public class Register : Element
{
 
    private int resetInput;
    private int setInput;
 
    private Memory[] memoryArray; 

 
    private int[][] memoryInputs;
    private int inputsNumber;

  
    public Register(int memorySize, int inputCount)
    {
        resetInput = 0;
        setInput = 0;
        memoryArray = new Memory[memorySize];
        memoryInputs = new int[memorySize][];

        for (int i = 0; i < memorySize; i++)
        {
            memoryArray[i] = new Memory(inputCount);
            memoryInputs[i] = new int[inputCount];
        }
    }

  


    public int getCurrentState()
    {
        return memoryArray[0].getState();
    }
 


    public int GetInputState(int index, int inputIndex)
    {
        if (index >= 0 && index < memoryArray.Length && inputIndex >= 0 && inputIndex < memoryArray[index].InputCount)
        {
            return memoryArray[index].GetInputState(inputIndex);
        }
        else
        {
            throw new ArgumentOutOfRangeException("Неверный индекс входа или элемента");
        }
    }
 
    public void ComputeState()
    {
        for (int i = 0; i < memoryArray.Length; i++)
        {
            memoryArray[i].SetInput = setInput;
            memoryArray[i].ResetInput = resetInput;
            memoryArray[i].ComputeState();
        }
    }
 
 


    public int[] GetInvertedOutputs()
    {
        int[] outputs = new int[memoryArray.Length];

        for (int i = 0; i < memoryArray.Length; i++)
        {
            outputs[i] = memoryArray[i].InvertedOutput;
        }

        return outputs;
    }

 
    //===================================
    public Register(int v)
    {
        inputsNumber = v;
        memoryArray = new Memory[v];
        memoryInputs = new int[v][];

        for (int i = 0; i < v; i++)
        {
            memoryArray[i] = new Memory(2); // Например, память с двумя входами
            memoryInputs[i] = new int[2];   // Инициализация массива входов
        }
    }

 
    public override void SetInputs(int[] inputs)
    {
        if (inputs.Length != InputCount)
            throw new ArgumentException($"Expected {InputCount} inputs.");

        for (int i = 0; i < memoryArray.Length; i++)
        {
            memoryArray[i].SetInputs(new int[] { inputs[i], 1 });
        }
    }


    public int[] GetDirectOutputs()
    {
        return memoryArray.Select(mem => mem.DirectOutput).ToArray();
    }

    public override int ComputeOutput()
    {
        return memoryArray.Aggregate(0, (acc, mem) => (acc << 1) | mem.DirectOutput);
    }




}

*/
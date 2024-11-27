 using System;

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

 

    public Register(int v)
    {
        this.inputsNumber = v;
        memoryArray = new Memory[v];  
        for (int i = 0; i < v; i++)
        {
            memoryArray[i] = new Memory(1);  
        }
    }

    public int getCurrentState()
    {
        return memoryArray[0].getState();
    }
 
    public void SetInputs(int reset, int set, int[][] inputs)
    {
        this.resetInput = reset;
        this.setInput = set;

        for (int i = 0; i < memoryArray.Length; i++)
        {
            memoryInputs[i] = inputs[i];
            memoryArray[i].SetInputs(memoryInputs[i]);
        }
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
 

    public int[] GetDirectOutputs()
    {
        if (memoryArray == null)
        {
            throw new InvalidOperationException("Memory array is not initialized.");
        }

        int[] outputs = new int[memoryArray.Length];
        for (int i = 0; i < memoryArray.Length; i++)
        {
            if (memoryArray[i] != null)
            {
                outputs[i] = memoryArray[i].DirectOutput;
            }
        }

        return outputs;
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

 
    public override int ComputeOutput()
    {
        int result = 0;

        foreach (var mem in memoryArray)
        {
            result |= mem.DirectOutput;
        }

        return result;
    }






}

 
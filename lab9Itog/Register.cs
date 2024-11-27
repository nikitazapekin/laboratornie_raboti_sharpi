using lab9Itog.Interfaces;
using System;
using System.Windows;

public class Register : Element, IShiftable
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


    /*
        public void Shift(int bits)
        {
            if (bits < 0)
            {
                throw new ArgumentException("Bit shift count cannot be negative.");
            }

            for (int i = 0; i < bits; i++)
            {
                var buff = memories[memories.Length - 1].getInputValues();
                for (int j = triggers.Length - 1; j > 0; j--)
                {
                    triggers[j].SetInputsNoState(triggers[j - 1].getInputValues());
                }
                triggers[0].SetInputsNoState(buff);
            }
            setState = triggers[0].getState();
        }
    */

    public void Shift(int bits)
    {
        if (bits < 0)
        {
            throw new ArgumentException("Bit shift count cannot be negative.");
        }

        for (int i = 0; i < bits; i++)
        {
           
            var buff = memories[memories.Length - 1].GetAllInputs();

          
            for (int j = memories.Length - 1; j > 0; j--)
            {
                memories[j].SetInputs(memories[j - 1].GetAllInputs());
            }

           
            memories[0].SetInputs(buff);
        }
    }




}

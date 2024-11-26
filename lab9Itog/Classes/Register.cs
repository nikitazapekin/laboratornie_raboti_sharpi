using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9Itog.Classes
{
    class Register
    {
        // Входы "Сброс" и "Установка"
        private int resetInput;
        private int setInput;

        // Массив типа Память
        private Memory[] memoryArray; // triggers

        // Массив входных значений для элементов типа Память
        private int[][] memoryInputs;

        // Конструктор, создающий Register с заданным количеством элементов типа Memory
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
        // Метод для задания значений на входах экземпляра класса
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

        // Метод для опроса состояния отдельного входа экземпляра класса
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

        // Метод для вычисления состояния экземпляра класса
        public void ComputeState()
        {
           

            foreach (var mem in memoryArray)
            {
                mem.SetInput = setInput;  
                mem.ResetInput = resetInput; 
                mem.ComputeState();
            }
        }

        // Метод для получения состояния прямого выхода
        public int[] GetDirectOutputs()
        {
            return memoryArray.Select(mem => mem.DirectOutput).ToArray();
        }

        // Метод для получения состояния инверсного выхода
        public int[] GetInvertedOutputs()
        {
            return memoryArray.Select(mem => mem.InvertedOutput).ToArray();
        }
    }
     
}

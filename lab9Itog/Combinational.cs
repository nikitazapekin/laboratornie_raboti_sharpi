using System;

public class Combinational : Element
{
    // Поле для хранения значений входов
    private int[] inputs;

    public Combinational() : base("Combinational", 2, 1)
    {
        inputs = new int[InputCount]; // по умолчанию 2 входа
    }

    public Combinational(int inputCount) : base("Combinational", inputCount, 1)
    {
        inputs = new int[inputCount];
    }

    // Метод, задающий значение на входах экземпляра класса
    public void SetInputs(int[] inputValues)
    {
        if (inputValues.Length != InputCount)
        {
            throw new ArgumentException("Количество значений входов не совпадает с количеством входов элемента.");
        }
        inputs = inputValues;
    }

    // Метод, возвращающий текущие входные значения
    public int[] GetInputs()
    {
        return inputs;
    }

    // Метод инвертирования входных значений
    public override void Invert()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            // Инвертируем каждое значение
            inputs[i] = (inputs[i] == 0) ? 1 : 0;
        }
    }

    // Метод, позволяющий опрашивать состояние отдельного входа экземпляра класса
    public int GetInputState(int index)
    {
        if (index < 0 || index >= InputCount)
        {
            throw new ArgumentOutOfRangeException("Неверный индекс входа.");
        }
        return inputs[index];
    }

    // Метод, вычисляющий значение выхода (по логике элемента)
    // ОПЕРАЦИЯ МОД2 ДЛЯ ДВОИЧНЫХ ЭЛЕМЕНТОВ
    public override int ComputeOutput()
    {
        int result = inputs[0]; // Начинаем с первого значения входа
        for (int i = 1; i < inputs.Length; i++)
        {
            // Применяем XOR к следующему элементу
            result ^= inputs[i];
        }
        return result;
    }
}


/*
 * 
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IO;

 
    
   public  class Combinational : Element
    {
        // Поле для хранения значений входов
        private int[] inputs;

        
        public Combinational() : base("Combinational", 2, 1)
        {
            inputs = new int[InputCount]; // по умолчанию 2 входа
        }
 
        public Combinational(int inputCount) : base("Combinational", inputCount, 1)
        {
            inputs = new int[inputCount];
        }

        // Метод, задающий значение на входах экземпляра класса
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
       inputs = inputs.Select(value => value == 0 ? 1 : 0).ToArray();
    }



    // Метод, позволяющий опрашивать состояние отдельного входа экземпляра класса
    public int GetInputState(int index)
        {
            if (index < 0 || index >= InputCount)
            {
                throw new ArgumentOutOfRangeException("Неверный индекс входа.");
            }
            return inputs[index];
        }

        // Метод, вычисляющий значение выхода (по логике элемента)
         
        //ОПЕРАЦИЯ МОД2 ДЛЯ ДВОИЧНЫХ ЭЛЕМЕНТОВ
        public override  int ComputeOutput()
        {
            return inputs.Aggregate((a, b) => a ^ b);
        }

    }


*/






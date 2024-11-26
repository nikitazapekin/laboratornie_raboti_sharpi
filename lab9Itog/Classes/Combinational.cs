using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IO;

namespace lab9Itog.Classes
{
    
    class Combinational : Element
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








     


}

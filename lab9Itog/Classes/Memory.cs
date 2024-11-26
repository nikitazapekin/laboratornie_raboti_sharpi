using System;
using System.Linq;

namespace lab9Itog.Classes
{
   public class Memory : Element
    {
        // Массив входных значений
        private int[] inputValues;

        // Состояния на выходах
        private int directOutput;  // Прямой выход
        private int invertedOutput; // Инверсный выход

        // Входы для установки и сброса
        private int setInput;
        private int resetInput;

        // Конструктор по умолчанию
        public Memory(int inputCount = 1)
            : base("Memory", inputCount + 2, 2) // +2 для учета входов установки и сброса
        {
            inputValues = new int[inputCount + 2]; // Включая входы для установки и сброса
            directOutput = 0;
            invertedOutput = 1;
            setInput = 0;
            resetInput = 0;
        }
        public int getState()
        {
            return inputValues[1];
        }


        // Конструктор копирования
        public Memory(Memory other) : base(other.Name, other.InputCount, other.OutputCount)
        {
            inputValues = (int[])other.inputValues.Clone();
            directOutput = other.directOutput;
            invertedOutput = other.invertedOutput;
            setInput = other.setInput;
            resetInput = other.resetInput;
        }

        // Метод для задания значений на входах
        public void SetInputs(int[] inputs)
        {
            if (inputs.Length == inputValues.Length)
            {
                inputValues = inputs;
            }
            else
            {
                throw new ArgumentException("Количество входных значений не совпадает с ожидаемым");
            }
        }

        // Метод для опроса состояния отдельного входа
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

        // Метод для вычисления состояния триггера
        public void ComputeState()
        {
            // Если вход установки равен 1, то устанавливаем состояние в 1
            if (setInput == 1)
            {
                directOutput = 1;
                invertedOutput = 0;
            }
            // Если вход сброса равен 1, то сбрасываем состояние в 0
            else if (resetInput == 1)
            {
                directOutput = 0;
                invertedOutput = 1;
            }
            else
            {
                // Если нет установки или сброса, состояние сохраняется по предыдущему значению
                // Пример для D-триггера (можно изменить для других типов триггеров)
                directOutput = inputValues[0]; // Пример, предполагающий, что вход 0 — это информационный
                invertedOutput = 1 - directOutput;
            }
        }

        // Переопределение оператора == для сравнения экземпляров класса
        public override bool Equals(object obj)
        {
            if (obj is Memory other)
            {
                return this.directOutput == other.directOutput &&
                       this.invertedOutput == other.invertedOutput &&
                       this.setInput == other.setInput &&
                       this.resetInput == other.resetInput &&
                       this.inputValues.Length == other.inputValues.Length &&
                       this.inputValues.SequenceEqual(other.inputValues);
            }
            return false;
        }

        public override int ComputeOutput()
        {
            return directOutput;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(directOutput, invertedOutput, setInput, resetInput, inputValues).GetHashCode();
        }

        // Свойства для прямого и инверсного выходов
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

    }
}
 
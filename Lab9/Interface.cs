using System;
using System.Collections.Generic;

namespace Lab9
{
    // Интерфейс для инвертирования бинарного кода
    public interface IBinaryInvertible
    {
        // Метод для инверсии бинарного кода
        void InvertBinary();
    }

    // Интерфейс для работы сдвига регистра
    public interface IShiftable
    {
        // Метод для сдвига регистра на заданное количество бит
        void ShiftRegister(int shiftAmount);
    }

    // Общий интерфейс для элементов
    public interface IElement
    {
        string Name { get; }
        int InputsCount { get; }
        int OutputsCount { get; }

        bool Equals(object obj);
        string GetInfo(); // Метод для отображения информации о элементе
    }

    // Интерфейс для взаимодействия с триггерами
    public interface ITrigger : IElement, IBinaryInvertible
    {
        void SetInputs(bool[] inputs);
        bool GetInputState(int index);
        void CalculateState();
        bool DirectOutput { get; }
        bool InverseOutput { get; }
    }

    // Интерфейс для взаимодействия с регистрами
    public interface IRegister : IElement, IBinaryInvertible, IShiftable
    {
        void SetInputs(bool[] inputs);
        bool GetInputState(int index);
        void CalculateState();
        void Reset();
        void Set();
    }
}


/*
 * namespace Lab9
{
    public interface IInvertible
    {
        void Invert();
    }

    public interface IShiftable
    {
        void Shift(int bits);
    }
}
*

/*
 * 
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Interface
    {
    }
}


*/

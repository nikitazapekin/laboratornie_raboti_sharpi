using System;

namespace Lab9
{
    public class Register
    {
        private readonly Memory[] _cells;
        private readonly int _size;

        public Register(int size)
        {
            _size = size;
            _cells = new Memory[size];
            for (int i = 0; i < size; i++)
            {
                _cells[i] = new Memory();
            }
        }

        public void SetInputs(bool[][] inputs)
        {
            if (inputs.Length != _size)
                throw new ArgumentException("Invalid input size");
            for (int i = 0; i < _size; i++)
            {
                _cells[i].SetInputs(inputs[i]);
            }
        }

        public void CalculateStates()
        {
            foreach (var cell in _cells)
            {
                cell.CalculateState();
            }
        }

        public bool GetCellOutput(int index, bool inverted = false)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException();
            return _cells[index].GetOutput(inverted);
        }
    }
}


/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Register
    {
    }
}

*/
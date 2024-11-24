using System;
using System.Linq;

namespace Lab9
{
    public class CombinationElement : Element
    {
        private bool[] _inputValues;

        public CombinationElement(string name, int inputs) : base(name, inputs, 1)
        {
            _inputValues = new bool[inputs];
        }

        public void SetInputs(bool[] inputs)
        {
            if (inputs.Length != Inputs)
                throw new ArgumentException("Invalid number of inputs");
            _inputValues = inputs;
        }

        public bool GetInputState(int index)
        {
            if (index < 0 || index >= Inputs)
                throw new ArgumentOutOfRangeException();
            return _inputValues[index];
        }

        public bool CalculateOutput()
        {
 
            return _inputValues.Aggregate(false, (current, input) => current ^ input);
        }

        public override void Invert()
        {
            for (int i = 0; i < _inputValues.Length; i++)
            {
                _inputValues[i] = !_inputValues[i];
            }
        }
    }
}


/*
 * 
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class CombinationElement
    {
    }
}
*/
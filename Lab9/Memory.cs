using System;

namespace Lab9
{
    public class Memory : Element
    {
        private bool[] _inputValues;
        private bool _q;
        private bool _notQ;

        public Memory() : base("D-Trigger", 2, 2)
        {
            _inputValues = new bool[Inputs];
            Reset();
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

        public void CalculateState()
        {
            _q = _inputValues[0];
            _notQ = !_q;
        }

        public bool GetOutput(bool inverted = false) => inverted ? _notQ : _q;

        public void Reset()
        {
            _q = false;
            _notQ = true;
        }

        public override void Invert()
        {
            _q = !_q;
            _notQ = !_notQ;
        }

        public override bool Equals(object obj)
        {
            if (obj is Memory other)
            {
                return _q == other._q && _notQ == other._notQ;
            }
            return false;
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
    internal class Memory
    {
    }
}


*/
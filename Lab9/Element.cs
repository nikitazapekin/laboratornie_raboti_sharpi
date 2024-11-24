using System;

namespace Lab9
{
    public abstract class Element
    {
        private string _name;
        private int _inputs;
        private int _outputs;

        public string Name => _name;
        public int Inputs
        {
            get => _inputs;
            set => _inputs = value > 0 ? value : 1;
        }
        public int Outputs
        {
            get => _outputs;
            set => _outputs = value > 0 ? value : 1;
        }

        protected Element()
        {
            _name = "Unnamed";
            _inputs = 1;
            _outputs = 1;
        }

        protected Element(string name)
        {
            _name = name;
            _inputs = 1;
            _outputs = 1;
        }

        protected Element(string name, int inputs, int outputs)
        {
            _name = name;
            Inputs = inputs;
            Outputs = outputs;
        }

        public override bool Equals(object obj)
        {
            if (obj is Element other)
            {
                return _name == other._name && _inputs == other._inputs && _outputs == other._outputs;
            }
            return false;
        }

        public abstract void Invert();
    }
}


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Element
    {
    }
}



*/
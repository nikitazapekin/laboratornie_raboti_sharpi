using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab9Itog
{
    class RegisterChild : Register

    {



        public RegisterChild() : base()
        {
        }

      
        public RegisterChild(int size) : base(size)
        {
        }

        
        public bool ToggleParityBit
        {
            get
            {
               
                return ParityBit;
            }
            set
            {
                MessageBox.Show("Бит"+ParityBit.ToString());
                ParityBit = !ParityBit;
             
            }
        }




    }
}

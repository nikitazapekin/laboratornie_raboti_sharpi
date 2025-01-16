using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab9Itog
{
    class MemoryChild : Memory
    {

        public MemoryChild() : base(1)  
        {
        }

       
        public MemoryChild(int inputCount) : base(inputCount)
        {
        }


         public void ResizeInputs(int newInputCount)
           {
               if (newInputCount <= 0)
               {
                   throw new ArgumentException("Количество входов должно быть больше 0.");
               }

            MessageBox.Show("Num" + newInputCount);
               setNewInputs(newInputCount);

           }
 


       



        public override void RandomSetInputs()
        {
           
        }

    }
}

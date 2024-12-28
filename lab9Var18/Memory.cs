using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.IO;
using System.Windows;

public class Memory : Element
{
    private int tInput; 
    private int currentState;  

    public Memory() : base("TTrigger", 1, 1)
    {
        tInput = 0;
        currentState = 0; 
    }

 
    public override void SetInput(int t)
    {
        if (t != 0 && t != 1)
        {
            throw new ArgumentException("Вход T должен быть 0 или 1.");
        }
        tInput = t;
    }

  
    public override int ComputeOutput()
    {
        if (tInput == 1)
        {
            currentState = currentState == 0 ? 1 : 0; 
        }
     
        return currentState;
    }
    public int GetTInput()
    {
        return tInput;
    }

    public int GetState()
    {
        return currentState;
    }

    public override void Invert()
    {
        
        currentState = currentState == 0 ? 1 : 0;
        ComputeOutput();

    }

    public void SaveToBinary(string fileName)
    {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        using (var writer = new BinaryWriter(fs))
        {
            writer.Write(currentState);
        }
    }

    public void LoadFromBinary(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException("Файл не найден.");

        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (var reader = new BinaryReader(fs))
        {
            currentState = reader.ReadInt32();
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Memory other)
        {
            return this.currentState == other.currentState;
        }
        return false;
    }




}
/*
namespace lab9Var18
{
    class Memory
    {
    }
}
*/
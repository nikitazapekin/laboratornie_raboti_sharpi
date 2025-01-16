 

using System;
using System.IO;
using System.Windows;

public class Memory : Element
{ 
    private int[] inputValues;
   // private inputCount;
 
    private int directOutput;  
    private int invertedOutput; 

 
    private int setInput;
    private int resetInput;






    private bool parity; // Четность XOR всех входов
    private int onesCount; // Количество единиц на выходах



    private void UpdateFields()
    {
        // Обновляем четность XOR всех входов
        parity = CalculateParity();

        // Подсчитываем количество единиц на выходах
        onesCount = (directOutput == 1 ? 1 : 0) + (invertedOutput == 1 ? 1 : 0);
    }
    private bool CalculateParity()
    {
        int xorResult = 0;
        foreach (var value in inputValues)
        {
            xorResult ^= value;
        }
        return xorResult == 1;
    }

    // Свойства для новых полей
    public bool Parity
    {
        get => parity;
        set => parity = value; // Можно сделать поле только для чтения, убрав set
    }

    public int OnesCount
    {
        get => onesCount;
        set => onesCount = value; // Можно сделать поле только для чтения, убрав set
    }

    public Memory(int inputCount = 1)
        : base("Memory", inputCount + 1, 1)
    {
        inputValues = new int[inputCount + 1];
        directOutput = 0;
        invertedOutput = 1;
        setInput = 0;
        resetInput = 0;
    }

    public int getState()
    {
        return inputValues[1];
    }

   
    public Memory(Memory other) : base(other.Name, other.InputCount, other.OutputCount)
    {
        inputValues = (int[])other.inputValues.Clone();
        directOutput = other.directOutput;
        invertedOutput = other.invertedOutput;
        setInput = other.setInput;
        resetInput = other.resetInput;
        parity = other.parity;
        onesCount = other.onesCount;
    }
  
   
    public void SetInputs(int[] inputs)
    {
        MessageBox.Show("Typed" + inputs.Length, "Requered" + InputCount);

        if (inputs.Length != InputCount)
            throw new ArgumentException($"Expected {InputCount} inputs.");
        if (inputs[1] == 0)
        {
            inputValues[1] = 0;
        }
        else
        {
            inputValues = inputs;
        }
        UpdateFields();
    }
    

 

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

 
    public void ComputeState()
    {
        
        if (setInput == 1)
        {
            directOutput = 1;
            invertedOutput = 0;
        }
    
        else if (resetInput == 1)
        {
            directOutput = 0;
            invertedOutput = 1;
        }
        else
        {
         
            directOutput = inputValues[0];
            invertedOutput = 1 - directOutput;
        }
    }


    public override bool Equals(object obj)
    {
        if (obj is Memory other)
        {
            if (this.directOutput != other.directOutput)
            {
                return false;
            }

            for (int i = 0; i < inputValues.Length; i++)
            {
                if (this.inputValues[i] != other.inputValues[i])
                {
                    return false;
                }
            }

            return true;
        }
        return false;
    }

    public int[] GetAllInputs()
    {
        return inputValues;
    }

    public override int ComputeOutput()
    {
        return directOutput;
    }


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

  
    public override void Invert()
    {
        if (inputValues[1] == 1)
        {
            inputValues[0] = inputValues[0] == 0 ? 1 : 0;

            directOutput = directOutput == 0 ? 1 : 0;

            invertedOutput = invertedOutput == 0 ? 1 : 0;
        }
    }

    public void SaveToBinary(string fileName)
    {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        using (var writer = new BinaryWriter(fs))
        {
            writer.Write(inputValues[0]);  
            writer.Write(inputValues[1]);  
        }
    }
    public void LoadFromBinary(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException("Файл не найден.");

        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (var reader = new BinaryReader(fs))
        {
            inputValues[0] = reader.ReadInt32(); 
            inputValues[1] = reader.ReadInt32();  
        }
    }



    public void setNewInputs(int number )
    {

        inputValues = new int[number + 1];
       // InputCount = number;
      //  inputValues = new int[InputCount + 1];
        MessageBox.Show(""+InputCount);
        for(int i=0; i< InputCount; i++)
        {
            inputValues[i] = 0;
        }
      //  SetInput()
        //   return inputValues[1];
    }



    public override string ToString()
    {
        return $"Входы: [{string.Join(" ", inputValues)}]";
    }


}

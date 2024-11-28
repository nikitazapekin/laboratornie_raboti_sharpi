using System;
using System.IO;

public class Memory : Element
{ 
    private int[] inputValues;

 
    private int directOutput;  
    private int invertedOutput; 

 
    private int setInput;
    private int resetInput;
 
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
    }
  
   
    public void SetInputs(int[] inputs)
    {
    

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
            if (this.directOutput != other.directOutput) return false;
      

            for (int i = 0; i < inputValues.Length; i++)
            {
                if (this.inputValues[i] != other.inputValues[i]) return false;
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

    /*

    public override string ToBinaryString()
    {
        using (var ms = new MemoryStream())
        using (var writer = new BinaryWriter(ms))
        {
            writer.Write(inputValues.Length); // Записываем длину массива
            foreach (var value in inputValues)
            {
                writer.Write(value); // Записываем каждый элемент массива
            }

            writer.Write(directOutput); // Записываем прямой вывод
            writer.Write(invertedOutput); // Записываем инверсный вывод

            return Convert.ToBase64String(ms.ToArray()); // Конвертируем в строку Base64
        }
    }

    public override void FromBinaryString(string dataString)
    {
        var data = Convert.FromBase64String(dataString);
        using (var ms = new MemoryStream(data))
        using (var reader = new BinaryReader(ms))
        {
            int inputValuesLength = reader.ReadInt32();
            inputValues = new int[inputValuesLength];

            for (int i = 0; i < inputValuesLength; i++)
            {
                inputValues[i] = reader.ReadInt32();
            }

            directOutput = reader.ReadInt32();
            invertedOutput = reader.ReadInt32();

        //    ValidateInputValues(); // Проверяем значения
        }
    }

    */


    public void SaveToBinary(string fileName)
    {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        using (var writer = new BinaryWriter(fs))
        {
            writer.Write(inputValues[0]); // Первое число
            writer.Write(inputValues[1]); // Второе число
        }
    }
    public void LoadFromBinary(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException("Файл не найден.");

        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        using (var reader = new BinaryReader(fs))
        {
            inputValues[0] = reader.ReadInt32(); // Первое число
            inputValues[1] = reader.ReadInt32(); // Второе число
        }
    }








}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System;
using System.IO;
using System.Windows;
public interface IShiftable
{
    void Shift(int bits);
}

public class Register : Element, IShiftable
{
    private static int ResetState = 0;
    private static int SetState = 0;

    
    private int[][] memories;

    public Register(int inputCount = 1) : base("Register", inputCount, 1)
    {
     
        memories = new int[inputCount][];
        for (int i = 0; i < inputCount; i++)
        {
            memories[i] = new int[2];  // [состояние, вход]
        }
    }

    public override int ComputeOutput()
    {
        
        int output = 0;
        foreach (var memory in memories)
        {
            output ^= memory[0];  
        }
        return output;
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override void FromBinaryString(string dataString)
    {
        base.FromBinaryString(dataString);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void Invert()
    {
      
        foreach (var memory in memories)
        {
            memory[0] = memory[0] == 0 ? 1 : 0;   
        }
    }

    public void SetInputs(int[][] inputValues)
    {
        if (inputValues.Length != memories.Length)
            throw new ArgumentException($"Ошибка: Размерность входных данных не совпадает с размером памяти.");

        for (int i = 0; i < memories.Length; i++)
        {
            if (inputValues[i].Length != 2)
          //      throw new ArgumentException($"Ошибка: Каждый входной массив должен содержать два элемента.");

          
            memories[i][0] = inputValues[i][0];   
            memories[i][1] = inputValues[i][1];  
        }
    }

    public void Shift(int bits)
    {
        if (bits != 1)
        {
            throw new ArgumentException("Метод поддерживает только сдвиг на 1 бит.");
        }

        foreach (var memory in memories)
        {
            try
            {
               
                int lastState = memory[0];
                int lastInput = memory[1];

            
                memory[0] = lastInput;  
                memory[1] = lastState;  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сдвиге памяти: {ex.Message}");
            }
        }
    }

    public override string ToBinaryString()
    {
        return base.ToBinaryString();
    }

    public override string? ToString()
    {
        return base.ToString();
    }

    public int[][] GetInputs()
    {
        return memories;
    }

    public void LoadFromBinary(string fileName)
    {
        try
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не найден.");

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                for (int i = 0; i < memories.Length; i++)
                {
                    memories[i][0] = reader.ReadInt32();  // Состояние
                    memories[i][1] = reader.ReadInt32();  // Вход
                }
            }

            MessageBox.Show("Данные успешно загружены из бинарного файла.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке бинарных данных: {ex.Message}");
        }
    }

    public void SaveToBinary(string fileName)
    {
        try
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                for (int i = 0; i < memories.Length; i++)
                {
                    writer.Write(memories[i][0]);  // Состояние
                    writer.Write(memories[i][1]);  // Вход
                }
            }

            MessageBox.Show($"Данные успешно сохранены в файл: {fileName}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении бинарных данных: {ex.Message}");
        }
    }
}

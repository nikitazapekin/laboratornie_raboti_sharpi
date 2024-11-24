using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab9
{
    public partial class MainWindow : Window
    {
        // Список элементов
        private readonly List<IElement> elements = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Добавить элемент
        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = ElementNameInput.Text;
                int inputs = int.Parse(InputsCountInput.Text);
                int outputs = int.Parse(OutputsCountInput.Text);

                // Создаем новый элемент (например, Комбинационный)
                IElement newElement = new CombinationalElement(name, inputs, outputs);
                elements.Add(newElement);

                ElementsList.Items.Add(name); // Добавляем имя в список отображения
                ElementNameInput.Clear();
                InputsCountInput.Clear();
                OutputsCountInput.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Инвертировать бинарный код
        private void InvertBinary_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsList.SelectedIndex >= 0)
            {
                var selectedElement = elements[ElementsList.SelectedIndex] as IBinaryInvertible;
                selectedElement?.InvertBinary();
                DisplayElementInfo();
            }
            else
            {
                MessageBox.Show("Выберите элемент из списка!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Сдвиг влево
        private void ShiftLeft_Click(object sender, RoutedEventArgs e)
        {
            PerformShift(-1);
        }

        // Сдвиг вправо
        private void ShiftRight_Click(object sender, RoutedEventArgs e)
        {
            PerformShift(1);
        }

        private void PerformShift(int direction)
        {
            if (ElementsList.SelectedIndex >= 0)
            {
                var selectedElement = elements[ElementsList.SelectedIndex] as IShiftable;
                selectedElement?.ShiftRegister(direction);
                DisplayElementInfo();
            }
            else
            {
                MessageBox.Show("Выберите элемент из списка!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Отобразить информацию об элементе
        private void DisplayElementInfo()
        {
            if (ElementsList.SelectedIndex >= 0)
            {
                var selectedElement = elements[ElementsList.SelectedIndex];
                ElementInfo.Text = selectedElement.GetInfo();
            }
            else
            {
                ElementInfo.Text = "Нет выбранного элемента.";
            }
        }
    }

    // Пример реализации класса Комбинационного элемента
    public class CombinationalElement : IElement, IBinaryInvertible, IShiftable
    {
        public string Name { get; }
        public int InputsCount { get; }
        public int OutputsCount { get; }

        private List<int> binaryData = new(); // Пример хранения данных

        public CombinationalElement(string name, int inputs, int outputs)
        {
            Name = name;
            InputsCount = inputs;
            OutputsCount = outputs;
        }

        public void InvertBinary()
        {
            for (int i = 0; i < binaryData.Count; i++)
            {
                binaryData[i] = binaryData[i] == 0 ? 1 : 0;
            }
        }

        public void ShiftRegister(int shiftAmount)
        {
            // Простой сдвиг
            if (shiftAmount < 0) binaryData.Insert(0, 0); // Влево
            else if (shiftAmount > 0) binaryData.Add(0); // Вправо
        }

        public bool Equals(object obj)
        {
            if (obj is CombinationalElement other)
            {
                return Name == other.Name &&
                       InputsCount == other.InputsCount &&
                       OutputsCount == other.OutputsCount;
            }
            return false;
        }

        public string GetInfo()
        {
            return $"Комбинационный элемент {Name}, входов: {InputsCount}, выходов: {OutputsCount}.";
        }
    }
}



/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
*/

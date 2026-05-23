using Lab7Library;

namespace Lab7Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота 7. Варіант 9 ===");

            DoublyLinkedList list = new DoublyLinkedList();

            int elementsCount = 0;
            bool isCountValid = false; 

            while (!isCountValid) 
            {
                Console.Write("Скільки чисел ви хочете ввести? ");
                string? countInput = Console.ReadLine();

                if (int.TryParse(countInput, out elementsCount))
                {
                    if (elementsCount > 0) isCountValid = true; 
                    else if (elementsCount == 0)
                    {
                        Console.WriteLine("Ви обрали 0 елементів. Програму завершено.");
                        return; 
                    }
                    else Console.WriteLine("Помилка: Кількість не може бути від'ємною!");
                }
                else Console.WriteLine("Помилка: Введіть ціле число");
            }

            Console.WriteLine($"\nТепер введіть {elementsCount} чисел (можна дробові та від'ємні)");

            for (int i = 0; i < elementsCount; i++)
            {
                bool isNumberValid = false; 
                while (!isNumberValid) 
                {
                    Console.Write($"Введіть число {i + 1} з {elementsCount}: ");
                    string? input = Console.ReadLine();

                    if (double.TryParse(input, out double number))
                    {
                        list.AddFirst(number);
                        isNumberValid = true; 
                    }
                    else Console.WriteLine("Помилка: Це не число! Використовуйте цифри");
                }
            }

            Console.WriteLine("\n\n=== ВВЕДЕННЯ ЗАВЕРШЕНО ===");
            Console.WriteLine("Початковий стан списку:");
            PrintList(list);

            if (list.Count >= 3)
            {
                Console.WriteLine($"\nЕлемент за індексом 1: {list[1]}");
                Console.WriteLine("Видаляємо елемент за індексом 2...");
                list.RemoveAt(2);
                PrintList(list);
            }
            else Console.WriteLine("\n(Пропускаємо перевірку видалення за індексом, бо ви ввели менше 3 елементів)");

            Console.WriteLine("\n=== Операції згідно з варіантом 9 ===");

            // 1. Отримуємо і значення, і пораховане середнє
            double? lessThanAvg = list.FindFirstLessThanAverage(out double avg);
            Console.WriteLine($"\n1. Перше входження елемента, меншого за середнє ({avg:F2}): {(lessThanAvg.HasValue ? lessThanAvg.Value.ToString() : "Не знайдено")}");

            // 2. Отримуємо суму і максимальне число
            double sumAfter = list.SumAfterMax(out double maxVal);
            Console.WriteLine($"2. Сума елементів після максимального ({maxVal}): {sumAfter}");

            // 3. Безпечне введення числа для перевірки (з тими ж правилами)
            double threshold = 0;
            bool isThresholdValid = false;
            while (!isThresholdValid)
            {
                Console.Write("\nВведіть значення порогу для 3-ї операції (число): ");
                string? threshInput = Console.ReadLine();
                if (double.TryParse(threshInput, out threshold))
                {
                    isThresholdValid = true;
                }
                else
                {
                    Console.WriteLine("Помилка: Це не число! Спробуйте ще раз.");
                }
            }

            DoublyLinkedList greaterList = list.GetElementsGreaterThan(threshold);
            Console.WriteLine($"\n3. Новий список з елементів, більших за {threshold}:");
            PrintList(greaterList);

            // 4. Останнє видалення
            Console.WriteLine("\n4. Видаляємо всі елементи до максимального...");
            list.RemoveBeforeMax();
            Console.WriteLine("Фінальний список:");
            PrintList(list);
            
        }

        static void PrintList(DoublyLinkedList list)
        {
            Console.Write("[ ");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("]");
            Console.WriteLine($"Кількість елементів: {list.Count}");
        }
    }
}

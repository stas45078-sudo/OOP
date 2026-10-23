using System;
using System.Globalization;
using System.Text;

public class Program
{
    private const string DefaultText = "В лесу родилась елка! В лесу она росла. Зимой и летом была стройная, зеленая!";
    private static readonly char[] SentenceDelimiters = { '.', '!', '?' };
    private static readonly char[] WordDelimiters = { ' ', ',' };
    private static int[,] matrix;
    private static int[][] jaggedArray;
    private static string text = string.Empty;
    private static string processedText = string.Empty;

    public static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        Console.OutputEncoding = Encoding.UTF8;

        string choice;
        do
        {
            Console.WriteLine(@"Лабораторная работа №5 (Вариант 9)
1. Работа с двумерным массивом
2. Работа с рваным массивом
3. Работа со строками
4. Выход");

            Console.Write("Выберите пункт: ");
            choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    RunMatrixMenu();
                    break;
                case "2":
                    RunJaggedMenu();
                    break;
                case "3":
                    RunStringMenu();
                    break;
                case "4":
                    Console.WriteLine("Выход из программы");
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    Console.WriteLine();
                    break;
            }
        } while (choice != "4");
    }

    private static void RunMatrixMenu()
    {
        string choice;
        do
        {
            Console.WriteLine(@"Двумерный массив
1. Создать матрицу
2. Напечатать матрицу
3. Добавить K строк в начало
4. Назад");

            Console.Write("Выберите пункт: ");
            choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    CreateMatrixFromInput();
                    break;
                case "2":
                    if (matrix == null)
                    {
                        Console.WriteLine("Матрица ещё не создана");
                    }
                    else if (matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
                    {
                        Console.WriteLine("Матрица пустая");
                    }
                    else
                    {
                        PrintMatrix(matrix);
                    }
                    break;
                case "3":
                    if (matrix == null)
                    {
                        Console.WriteLine("Сначала создайте матрицу");
                        break;
                    }
                    ProcessMatrixTask();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
            Console.WriteLine();
        } while (choice != "4");
    }

    private static void CreateMatrixFromInput()
    {
        int rows;
        int cols;

        ReadIntSafely("Введите количество строк матрицы: ", out rows);
        ReadIntSafely("Введите количество столбцов матрицы: ", out cols);

        if (rows < 0 || cols < 0)
        {
            Console.WriteLine("Ошибка: Размер матрицы не может быть отрицательным");
            return;
        }

        try
        {
            matrix = new int[rows, cols];
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: Слишком большой размер матрицы");
            return;
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine("Ошибка: Недостаточно памяти для матрицы такого размера");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании матрицы: {ex.Message}");
            return;
        }

        Console.WriteLine(@"Выберите способ заполнения:
1. Случайными числами
2. Вручную");
        Console.Write("Ваш выбор: ");

        var fillChoice = Console.ReadLine();
        while (fillChoice != "1" && fillChoice != "2")
        {
            Console.WriteLine("Ошибка: неверный выбор. Пожалуйста, введите 1 или 2");
            Console.Write("Ваш выбор: ");
            fillChoice = Console.ReadLine();
        }
        Console.WriteLine();
        var isManual = fillChoice == "2";
        var random = new Random();

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (isManual)
                {
                    ReadIntSafely($"Элемент [{i},{j}]: ", out var val);
                    matrix[i, j] = val;
                }
                else
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }
        }

        Console.WriteLine("Матрица создана");
    }

    private static void ProcessMatrixTask()
    {
        ReadIntSafely("Введите количество строк K для добавления в начало: ", out var rowsToAdd);

        if (rowsToAdd <= 0)
        {
            Console.WriteLine("Ошибка: Количество строк должно быть положительным");
            return;
        }

        Console.WriteLine(@"Выберите способ заполнения добавляемых строк:
1. Случайными числами
2. Вручную");
        Console.Write("Ваш выбор: ");

        var fillChoice = Console.ReadLine();
        while (fillChoice != "1" && fillChoice != "2")
        {
            Console.WriteLine("Ошибка: неверный выбор. Пожалуйста, введите 1 или 2");
            Console.Write("Ваш выбор: ");
            fillChoice = Console.ReadLine();
        }
        Console.WriteLine();
        var isManual = fillChoice == "2";

        matrix = AddRowsToStart(matrix, rowsToAdd, isManual);

        Console.WriteLine($"Добавлено {rowsToAdd} строк в начало");
        PrintMatrix(matrix, "Результат:");
    }

    private static int[,] AddRowsToStart(int[,] sourceMatrix, int rowsToAdd, bool isManual)
    {
        var rows = sourceMatrix.GetLength(0);
        var cols = sourceMatrix.GetLength(1);
        var newMatrix = new int[rows + rowsToAdd, cols];
        var random = new Random();

        for (var i = 0; i < rowsToAdd; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (isManual)
                {
                    ReadIntSafely($"Новый элемент [{i},{j}]: ", out var val);
                    newMatrix[i, j] = val;
                }
                else
                {
                    newMatrix[i, j] = random.Next(1, 10);
                }
            }
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                newMatrix[i + rowsToAdd, j] = sourceMatrix[i, j];
            }
        }

        return newMatrix;
    }

    private static void PrintMatrix(int[,] sourceMatrix, string title)
    {
        var rows = sourceMatrix.GetLength(0);
        var cols = sourceMatrix.GetLength(1);

        if (rows == 0 || cols == 0)
        {
            Console.WriteLine("Матрица пустая");
            return;
        }

        if (!string.IsNullOrEmpty(title)) Console.WriteLine(title);
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write(sourceMatrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    private static void PrintMatrix(int[,] sourceMatrix)
    {
        PrintMatrix(sourceMatrix, "");
    }

    private static void RunJaggedMenu()
    {
        string choice;
        do
        {
            Console.WriteLine(@"Рваный массив
1. Создать рваный массив
2. Напечатать рваный массив
3. Удалить строку с заданным номером
4. Назад");

            Console.Write("Выберите пункт: ");
            choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    CreateJaggedFromInput();
                    break;
                case "2":
                    if (jaggedArray == null)
                    {
                        Console.WriteLine("Рваный массив ещё не создан");
                    }
                    else if (jaggedArray.Length == 0)
                    {
                        Console.WriteLine("Рваный массив пуст");
                    }
                    else
                    {
                        PrintJaggedArray(jaggedArray);
                    }
                    break;
                case "3":
                    if (jaggedArray == null || jaggedArray.Length == 0)
                    {
                        Console.WriteLine("Сначала создайте непустой рваный массив");
                        break;
                    }
                    ProcessJaggedTask();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
            Console.WriteLine();
        } while (choice != "4");
    }

    private static void CreateJaggedFromInput()
    {
        ReadIntSafely("Введите количество строк рваного массива: ", out var rows);

        if (rows < 0)
        {
            Console.WriteLine("Ошибка: Количество строк не может быть отрицательным");
            return;
        }

        try
        {
            jaggedArray = new int[rows][];
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: Слишком большой размер массива");
            return;
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine("Ошибка: Недостаточно памяти для массива такого размера");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании рваного массива: {ex.Message}");
            return;
        }

        Console.WriteLine(@"Выберите способ заполнения:
1. Случайными числами
2. Вручную");
        Console.Write("Ваш выбор: ");

        var fillChoice = Console.ReadLine();
        while (fillChoice != "1" && fillChoice != "2")
        {
            Console.WriteLine("Ошибка: неверный выбор. Пожалуйста, введите 1 или 2.");
            Console.Write("Ваш выбор: ");
            fillChoice = Console.ReadLine();
        }
        Console.WriteLine();
        var isManual = fillChoice == "2";
        var random = new Random();

        for (var i = 0; i < rows; i++)
        {
            ReadIntSafely($"Введите количество элементов в строке {i}: ", out var cols);

            if (cols < 0)
            {
                Console.WriteLine($"Ошибка: Количество элементов в строке {i} не может быть отрицательным");
                return;
            }

            try
            {
                jaggedArray[i] = new int[cols];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании строки {i}: {ex.Message}");
                return;
            }

            for (var j = 0; j < cols; j++)
            {
                if (isManual)
                {
                    ReadIntSafely($"Элемент [{i}][{j}]: ", out var val);
                    jaggedArray[i][j] = val;
                }
                else
                {
                    jaggedArray[i][j] = random.Next(1, 10);
                }
            }
        }

        Console.WriteLine("Рваный массив создан");
    }

    private static void ProcessJaggedTask()
    {
        ReadIntSafely("Введите номер строки для удаления (от 0): ", out var rowToDelete);

        if (rowToDelete < 0 || rowToDelete >= jaggedArray.Length)
        {
            Console.WriteLine("Ошибка: Недопустимый номер строки");
            return;
        }

        jaggedArray = RemoveRowFromJagged(jaggedArray, rowToDelete);

        Console.WriteLine($"Строка {rowToDelete} удалена");
        PrintJaggedArray(jaggedArray, "Результат:");
    }

    private static int[][] RemoveRowFromJagged(int[][] sourceArray, int rowToDelete)
    {
        if (sourceArray.Length == 1)
        {
            return new int[0][];
        }

        var newArray = new int[sourceArray.Length - 1][];
        var newIndex = 0;

        for (var i = 0; i < sourceArray.Length; i++)
        {
            if (i == rowToDelete)
            {
                continue;
            }

            newArray[newIndex] = sourceArray[i];
            newIndex++;
        }

        return newArray;
    }

    private static void PrintJaggedArray(int[][] sourceArray, string title)
    {
        if (sourceArray == null || sourceArray.Length == 0)
        {
            Console.WriteLine("Рваный массив пустой");
            return;
        }

        if (!string.IsNullOrEmpty(title)) Console.WriteLine(title);
        for (var i = 0; i < sourceArray.Length; i++)
        {
            if (sourceArray[i] == null || sourceArray[i].Length == 0)
            {
                Console.WriteLine("[пустая строка]");
                continue;
            }

            for (var j = 0; j < sourceArray[i].Length; j++)
            {
                Console.Write(sourceArray[i][j] + " ");
            }

            Console.WriteLine();
        }
    }

    private static void PrintJaggedArray(int[][] sourceArray)
    {
        PrintJaggedArray(sourceArray, "");
    }

    private static void RunStringMenu()
    {
        string choice;
        do
        {
            Console.WriteLine(@"Строки
1. Ввести строку
2. Напечатать строку
3. Перевернуть каждое слово, номер которого в предложении совпадает с его длиной
4. Назад");

            Console.Write("Выберите пункт: ");
            choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    CreateStringFromInput();
                    break;
                case "2":
                    if (text == null)
                    {
                        Console.WriteLine("Строка ещё не создана");
                    }
                    else if (text.Length == 0)
                    {
                        Console.WriteLine("Строка пустая");
                    }
                    else
                    {
                        Console.WriteLine("Текущая строка:");
                        Console.WriteLine(text);
                    }
                    break;
                case "3":
                    if (text == null)
                    {
                        Console.WriteLine("Строка ещё не создана");
                        break;
                    }
                    if (text.Length == 0)
                    {
                        Console.WriteLine("Строка пустая, переворачивать нечего");
                        break;
                    }
                    processedText = ProcessString(text);
                    Console.WriteLine("Результат:");
                    if (string.IsNullOrWhiteSpace(processedText))
                    {
                        Console.WriteLine("(результат пуст)");
                    }
                    else
                    {
                        Console.WriteLine(processedText);
                    }
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
            Console.WriteLine();
        } while (choice != "4");
    }

    private static void CreateStringFromInput()
    {
        Console.WriteLine(@"Выберите способ задания строки:
1. Использовать готовую строку
2. Ввести вручную");
        Console.Write("Ваш выбор: ");

        var mode = Console.ReadLine();
        while (mode != "1" && mode != "2")
        {
            Console.WriteLine("Ошибка: неверный выбор. Пожалуйста, введите 1 или 2");
            Console.Write("Ваш выбор: ");
            mode = Console.ReadLine();
        }
        Console.WriteLine();

        if (mode == "1")
        {
            text = DefaultText;
            Console.WriteLine("Загружена готовая строка.");
        }
        else
        {
            Console.Write("Введите строку: ");
            var input = Console.ReadLine();
            Console.WriteLine();
            text = input ?? string.Empty;
            Console.WriteLine("Строка установлена");
        }

        processedText = text;
    }

    private static string ProcessString(string input)
    {
        var sentences = input.Split(SentenceDelimiters, StringSplitOptions.RemoveEmptyEntries);
        var delimiters = new char[sentences.Length];
        var delimiterIndex = 0;

        for (var i = 0; i < sentences.Length; i++)
        {
            var sentenceEndIndex = input.IndexOf(sentences[i], StringComparison.Ordinal) + sentences[i].Length;
            if (sentenceEndIndex < input.Length)
            {
                delimiters[delimiterIndex] = input[sentenceEndIndex];
                delimiterIndex++;
            }
        }

        var resultBuilder = new StringBuilder();
        for (var i = 0; i < sentences.Length; i++)
        {
            var sentence = sentences[i].Trim();
            if (string.IsNullOrEmpty(sentence))
            {
                continue;
            }

            var processedSentence = ProcessSentence(sentence);
            resultBuilder.Append(processedSentence);

            if (i < delimiters.Length && delimiters[i] != '\0')
            {
                resultBuilder.Append(delimiters[i]);
                resultBuilder.Append(' ');
            }
        }

        return resultBuilder.ToString().Trim();
    }

    private static string ProcessSentence(string sentence)
    {
        var words = sentence.Split(WordDelimiters, StringSplitOptions.RemoveEmptyEntries);
        if (words.Length == 0) return string.Empty;

        var separators = new string[words.Length > 1 ? words.Length - 1 : 0];

        var searchIndex = 0;
        for (var i = 0; i < words.Length - 1; i++)
        {
            var wordEndIndex = sentence.IndexOf(words[i], searchIndex, StringComparison.Ordinal) + words[i].Length;
            var separatorStart = wordEndIndex;
            while (separatorStart < sentence.Length && (sentence[separatorStart] == ' ' || sentence[separatorStart] == ','))
            {
                separatorStart++;
            }

            separators[i] = sentence.Substring(wordEndIndex, separatorStart - wordEndIndex);
            searchIndex = separatorStart;
        }

        var resultBuilder = new StringBuilder();
        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i];
            var wordNumber = i + 1;
            if (wordNumber == word.Length)
            {
                resultBuilder.Append(ReverseString(word));
            }
            else
            {
                resultBuilder.Append(word);
            }

            if (i < separators.Length)
            {
                resultBuilder.Append(separators[i]);
            }
        }

        return resultBuilder.ToString();
    }

    private static string ReverseString(string textValue)
    {
        if (string.IsNullOrEmpty(textValue)) return textValue;

        var characters = textValue.ToCharArray();
        Array.Reverse(characters);
        return new string(characters);
    }

    private static void ReadIntSafely(string prompt, out int result)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: ввод не может быть пустым. Попробуйте снова");
                continue;
            }

            if (int.TryParse(input, out result))
            {
                return;
            }

            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число");
        }
    }
}
using ClassLibrary1;
using lab10;
namespace lab12_4;

class Program
{
    static void Main(string[] args)
    {
        MyCollection<Carriage> collection = new MyCollection<Carriage>();
        int answer = 1;
        while (answer != 8)
        {
            try
            {
                PrintMenu();
                answer = IsInt(1, 8);
                switch (answer)
                {
                    case 1:
                        Console.WriteLine("Размер коллекции? (от 0 до 100)");
                        int size = IsInt(0, 100);
                        collection = new MyCollection<Carriage>(size);
                        Console.WriteLine("Список создан");
                        break;
                    case 2:
                        collection.PrintList();
                        break;
                    case 3:
                        Console.WriteLine("Введите число элементов, сколько вы хотите добавить (от 1 до 10)");
                        int forAdd = IsInt(1, 10);
                        List<Carriage> listForAdd = new List<Carriage>();
                        for (int i = 0; i < forAdd; i++)
                        {
                            Carriage CarriageForAdd = new Carriage();
                            CarriageForAdd.RandomInit();
                            listForAdd.Add(CarriageForAdd);
                        }
                        collection.AddItems(listForAdd);
                        Console.WriteLine("элементы успешно добавлены");
                        break;
                    case 4:
                        if (collection.Count > 0)
                        {
                            Console.WriteLine("Введите элемент для удаления из коллекции");
                            Carriage CarriageForRemove = new Carriage();
                            CarriageForRemove.Init();

                            if (collection.Contains(CarriageForRemove))
                            {
                                collection.RemoveItem(CarriageForRemove);
                                Console.WriteLine("Элемент успешно удалён");
                            }
                            else
                            {
                                Console.WriteLine("Коллекция не содержит данного элемента");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Коллекция пуста. Удаление не может быть выполнено.");
                        }
                        break;
                    case 5:
                        if (collection.Count > 0)
                        {
                            Console.WriteLine("Введите элемент для поиска в коллекции");
                            Carriage CarriageForFind = new Carriage();
                            CarriageForFind.Init();
                            Carriage foundItem = collection.FindItem(item => item.Equals(CarriageForFind));

                            if (foundItem != null)
                            {
                                Console.WriteLine("Элемент найден в коллекции");
                            }
                            else
                            {
                                Console.WriteLine("Элемент НЕ найден в коллекции");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Коллекция пуста. Поиск не может быть выполнен.");
                        }
                        break;
                    case 6:
                        if (collection.Count > 0)
                        {
                            MyCollection<Carriage> clonedCollection = collection.DeepCopy();
                            Console.WriteLine("Склонированная коллекция:");
                            clonedCollection.PrintList();
                        }
                        else
                        {
                            Console.WriteLine("Коллекция пуста. Клонирование не может быть выполнено.");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("1. Создать коллекцию");
        Console.WriteLine("2. Напечатать коллекцию");
        Console.WriteLine("3. Добавить в коллекцию элементы");
        Console.WriteLine("4. Удалить из коллекции элемент");
        Console.WriteLine("5. Поиск элемента");
        Console.WriteLine("6. Глубокое копирование");
        Console.WriteLine("7. Выход");
    }

    static int IsInt(int min, int max)
    {
        bool isConvert;
        int number;
        do
        {
            string buf = Console.ReadLine();
            isConvert = int.TryParse(buf, out number);
            if (!isConvert || number < min || number > max)
            {
                Console.WriteLine($"Неправильно введено число. Введите значение от {min} до {max}");
            }
        } while (!isConvert || number < min || number > max);
        return number;
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WarehouseСontrol;
using static System.Net.Mime.MediaTypeNames;

public class MainClass
{


    static void Main(string[] args)
    {
        Dictionary<DateOnly, List<Pallet>> Groups = new();
        int CountPallet = 30;
        MainClass main = new();


        //Создание паллет
        for (int i = 0; i < CountPallet; i++)
        {
            Pallet newPallet = main.GeneratePallets();

            if (!Groups.ContainsKey(newPallet.GeneralMinExpirationDate))
                Groups.Add(newPallet.GeneralMinExpirationDate, new List<Pallet>() { newPallet });
            else
                Groups[newPallet.GeneralMinExpirationDate].Add(newPallet);
        }


        //Сортировка всех групп
        Console.WriteLine("Group sorting:");

        Groups = main.SortDictionary(Groups);

        //Вывод в консоль отсортированных групп
        foreach (var item in Groups)
        {
            Console.WriteLine($"Group [{item.Key}]\t\t\t");
            for (int i = 0; i < item.Value.Count; i++)
                Console.WriteLine($"------- Width:[{item.Value[i].Width}] | Height:[{item.Value[i].Height}] | Depth:[{item.Value[i].Depth}] | Weight:[{item.Value[i].Weight}] | Volume:[{item.Value[i].Volume}] | Boxes count:[{item.Value[i].Boxes.Count}]");

        }


        Console.WriteLine("MaxDate: ");
        List<Pallet> findPallet = new();

        foreach (var group in Groups)
        {
            findPallet.AddRange(group.Value);
        }
        findPallet.Sort(delegate (Pallet x, Pallet y)
        {
            if (x.GeneralMaxExpirationDate == null && y.GeneralMaxExpirationDate == null) return 0;
            else if (x.GeneralMaxExpirationDate == null) return -1;
            else if (y.GeneralMaxExpirationDate == null) return 1;
            else return x.GeneralMaxExpirationDate.CompareTo(y.GeneralMaxExpirationDate);
        });

        List<Pallet> lastPallet = findPallet.TakeLast(3).ToList();
        lastPallet.Sort(delegate (Pallet x, Pallet y)
        {
            if (x.Volume == null && y.Volume == null) return 0;
            else if (x.Volume == null) return -1;
            else if (y.Volume == null) return 1;
            else return x.Volume.CompareTo(y.Volume);
        });


        foreach (var item in lastPallet)
        {
            Console.WriteLine(item.GeneralMaxExpirationDate);
            Console.WriteLine($"------- Width:[{item.Width}] | Height:[{item.Height}] | Depth:[{item.Depth}] | Weight:[{item.Weight}] | Volume:[{item.Volume}] | Boxes count:[{item.Boxes.Count}]");
        }


    }
    /// <summary>
    /// Создаем паллету 
    /// шириной от 100см до 300см 
    /// длинной от 100см до 300см
    /// высотой от 10см до 30см
    /// </summary>
    Pallet GeneratePallets()
    {
        Random random = new Random();

        int PalletWidth = random.Next(100,301);
        int PalletDepth = random.Next(100,301);
        int PalletHeight = random.Next(10,31);

        
        int CountBoxOnPallet = random.Next(1, 15);

        List<Box> Boxes = new List<Box>();
        
        for (int i = 0; i < CountBoxOnPallet; i++)
        {
            Boxes.Add(
                new Box(
                    new DateOnly(2023, 1, 1).AddDays(random.Next(0, 1000)),
                    i.ToString(),
                    random.Next(1, 300),
                    random.Next(1, PalletWidth + 1),
                    random.Next(1, PalletDepth + 1),
                    //Грамм
                    random.Next(10,500001)
                    )
                );
            //Console.WriteLine($"Box on pallet #{i} \t|{Boxes[i].Height}|{Boxes[i].Width}|{Boxes[i].Depth}|{Boxes[i].Weight}|{Boxes[i].Volume}|");
        }

        return new Pallet(Guid.NewGuid().ToString(), PalletHeight, PalletWidth, PalletDepth, (CountBoxOnPallet > 0) ? Boxes : null);
    }

    /// <summary>
    /// Сортирует словарь по ключу, а также List<Pallet> по весу
    /// </summary>
    /// <param name="_Groups"></param>
    /// <returns></returns>
    Dictionary<DateOnly, List<Pallet>> SortDictionary (Dictionary<DateOnly, List<Pallet>> _Groups)
    {
        var sortedDict = new SortedDictionary<DateOnly, List<Pallet>>(_Groups);
        
        foreach (var item in sortedDict)
        {
            for (int i = 0; i < item.Value.Count - 1; i++)
            {
                for (int j = i+1; j < item.Value.Count; j++)
                {
                    if (item.Value[i].Weight <= item.Value[j].Weight) continue;

                    float a = item.Value[i].Weight;
                    item.Value[i].Weight = item.Value[j].Weight;
                    item.Value[j].Weight = a;
                }
            }
        }

        _Groups = new Dictionary<DateOnly, List<Pallet>>(sortedDict);
        return _Groups;
    }
}



using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLinkedList
{
    class Program
    {
        private const int MAX_ITEM_VALUE = 1000;

        private static readonly Random RANDOM = new Random();
        private static DoubleLinkedList _doubleLinkedListInstance;

        private enum ListAction
        {
            None,
            CreateList,
            AddItemToTop,
            AddItemToEnd,
            SortBubbleMethod,
            QuickSort,
            InsertSorting,
            MixItem,
            WriteList,
            Exit
        }

        static void Main(string[] args)
        {
            while (true)
            {
                foreach (var action in Enum.GetValues(typeof(ListAction)).Cast<ListAction>().OrderBy(a => a))
                {
                    Console.WriteLine($"{(int)action} - {action.ToString()}");
                }

                Console.Write("Input number:");

                var response = InputIntValue();
                if (!response.HasValue)
                {
                    continue;
                }

                switch ((ListAction)response.Value)
                {
                    case ListAction.CreateList:

                        Console.Write("Input count items:");
                        var countItems = InputIntValue();
                        if (countItems.HasValue)
                        {
                            _doubleLinkedListInstance = new DoubleLinkedList();                          
                            for (var i = 0; i < countItems.Value; i++)
                            {
                                _doubleLinkedListInstance.AddToEnd(RANDOM.Next(MAX_ITEM_VALUE));
                            }
                            Console.WriteLine("List created");
                        }

                        break;
                    case ListAction.AddItemToTop:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        Console.Write("Input new item value:");
                        var newFirstItemValue = InputIntValue();
                        if (newFirstItemValue.HasValue)
                        {
                            _doubleLinkedListInstance.AddToTop(newFirstItemValue.Value);
                            Console.WriteLine("Item added");
                        }

                        break;
                    case ListAction.AddItemToEnd:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        Console.Write("Input new item value:");
                        var newLastItemValue = InputIntValue();
                        if (newLastItemValue.HasValue)
                        {
                            _doubleLinkedListInstance.AddToEnd(newLastItemValue.Value);
                            Console.WriteLine("Item added");
                        }

                        break;
                    case ListAction.MixItem:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        _doubleLinkedListInstance.MixItems(RANDOM);
                        Console.WriteLine("Items mixed");
                        break;
                    case ListAction.WriteList:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        Console.WriteLine(string.Join(", ", _doubleLinkedListInstance.GetItems().Select(i => i.Value)));
                        break;
                  
                    case ListAction.SortBubbleMethod:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        WriteSortResult(_doubleLinkedListInstance.BubbleMethodSort());
                        break;
                    case ListAction.QuickSort:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        WriteSortResult(_doubleLinkedListInstance.QuickSort());
                        break;
                    case ListAction.InsertSorting:
                        if (!TestListInitialized())
                        {
                            break;
                        }
                        _doubleLinkedListInstance = _doubleLinkedListInstance.InsertSort(out var comparisonsCount);
                        Console.WriteLine($"Sorted list created. Comparisons count - {comparisonsCount}");
                        break;

                    case ListAction.Exit:
                        return;

                }
            }
        }

        private static bool TestListInitialized()
        {
            if (_doubleLinkedListInstance == null)
            {
                Console.WriteLine("List do not initialized");
            }

            return true;
        }

        private static int? InputIntValue()
        {
            return int.TryParse(Console.ReadLine(), out var responseInt)
                    ? responseInt
                    : (int?)null;
        }


        private static void WriteSortResult(SortResult sortResult)
        {
            Console.WriteLine("Sorted");
            Console.WriteLine($"Comparisons count: {sortResult.ComparisonsCount}");
            Console.WriteLine($"Swaps count: {sortResult.SwapsCount}");
        }   

    }
}

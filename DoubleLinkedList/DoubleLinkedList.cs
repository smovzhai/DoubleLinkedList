using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DoubleLinkedList
{
    public class DoubleLinkedList
    {
        public ListItem First { get; private set; }

        public ListItem Last { get; private set; }

        public int ItemsCount { get; private set; }

        public ListItem AddToTop(int itemValue)
        {
            if (First == null)
            {
                return InitializeLits(itemValue);
            }

            var newItem = new ListItem { Value = itemValue, Next = First };
            First.Previous = newItem;
            First = newItem;
            ItemsCount++;
            return newItem;
        }

        public ListItem AddToEnd(int itemValue)
        {
            if (First == null)
            {
                return InitializeLits(itemValue);
            }

            var newItem = new ListItem { Value = itemValue, Previous = Last };
            Last.Next = newItem;
            Last = newItem;
            ItemsCount++;
            return newItem;
        }

        public ListItem TakeFirst()
        {
            if (First == null)
            {
                return null;
            }

            var result = First;
            First = First.Next;
            if (First != null) First.Previous = null;
            ItemsCount--;
            return result;
        }

        public void InsertBefore(ListItem insertingItem, ListItem positionItem)
        {
            var previous = positionItem.Previous;
            insertingItem.Previous = previous;
            insertingItem.Next = positionItem;
            positionItem.Previous = insertingItem;
            if (previous != null)
            {
                previous.Next = insertingItem;
            }
            else
            {
                First = insertingItem;
            }

            ItemsCount++;
        }

        public void AddToEnd(ListItem item)
        {
            ItemsCount++;
            if (First == null)
            {
                item.Previous = null;
                item.Next = null;
                First = item;
                Last = item;
                ItemsCount = 1;
                return;
            }

            Last.Next = item;
            item.Previous = Last;
            item.Next = null;
            Last = item;
        }

        public IEnumerable<ListItem> GetItems()
        {
            var current = First;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        public void MixItems(Random random)
        {
            for (var i = 0; i < ItemsCount / 2; i++)
            {
                var rnd = random.Next(ItemsCount - 1) + 1;
                var current = First;
                for (var j = 0; j < rnd; j++)
                {
                    current = current.Next;
                }

                Last.Next = First;
                First.Previous = Last;
                Last = current.Previous;
                Last.Next = null;
                current.Previous = null;
                First = current;
                First.Previous = null;
            }
        }

        public SortResult BubbleMethodSort()
        {
            var result = new SortResult();

            for (var i = 0; i < ItemsCount - 1; i++)
            {
                var current = First;
                for (var j = 0; j < ItemsCount - i - 1; j++)
                {
                    var next = current.Next;
                    result.IncComparisons();
                    if (current.Value > current.Next.Value)
                    {
                        Swap(current, current.Next);
                        result.IncSwaps();
                    }
                    else
                    {
                        current = next;
                    }
                }
            }

            return result;
        }

        public DoubleLinkedList InsertSort(out int comparisonsCount)
        {
            var newlist = new DoubleLinkedList();
            comparisonsCount = 0;
            while (First != null)
            {
                var item = TakeFirst();
                var firstLargerItem = newlist.GetItems().Select((i, index) => new { Item = i, Index = index }).FirstOrDefault(i => i.Item.Value > item.Value);
                comparisonsCount += firstLargerItem?.Index + 1 ?? newlist.ItemsCount;
                if (firstLargerItem == null)
                {
                    newlist.AddToEnd(item);
                }
                else
                {
                    newlist.InsertBefore(item, firstLargerItem.Item);
                }
            }

            return newlist;
        }

        public SortResult QuickSort()
        {
            var result = new SortResult();
            SplitByPivot(First, Last, result);
            return result;
        }

        private void SplitByPivot(ListItem start, ListItem end, SortResult sortResult, int? directPivote = null)
        {
            if (start == end)
            {
                return;
            }
            var startCursor = start;
            var endCursor = end;
            var pivot = directPivote ?? (start.Value + end.Value) / 2;

            while (true)
            {
                while (startCursor != endCursor && startCursor.Value <= pivot)
                {
                    sortResult.IncComparisons();
                    startCursor = startCursor.Next;
                }
              
                while (startCursor != endCursor && endCursor.Value > pivot)
                {
                    sortResult.IncComparisons();
                    endCursor = endCursor.Previous;                  
                }

                if (startCursor == endCursor) break;

              
                var cursorsAreNeighbors = startCursor.Next == endCursor;

                sortResult.IncSwaps();
                Swap(startCursor, endCursor);

                if (end == endCursor)
                {
                    end = startCursor;
                }

                if (start == startCursor)
                {
                    start = endCursor;
                }

                if (cursorsAreNeighbors)
                {
                    break;
                }

                var startCursorTmp = startCursor;
                startCursor = endCursor.Next;
                endCursor = startCursorTmp.Previous;
            }

            if (startCursor == end && startCursor.Value <= pivot) //wrong pivot or all values are the same  
            {
                var curent = start;
                var firstValue = curent.Value;
                while (curent != end)
                {
                    curent = curent.Next;
                    sortResult.IncComparisons();
                    if (firstValue != curent.Value)
                    {
                        SplitByPivot(start, end, sortResult, (firstValue + curent.Value) / 2);
                        return;
                    }
                }
                return;
            }

            var splitItem = startCursor.Value <= pivot
                    ? startCursor
                    : startCursor.Previous;

            SplitByPivot(start, splitItem, sortResult);
            SplitByPivot(splitItem.Next, end, sortResult);

            //Task.WaitAll(Task.Factory.StartNew(() => SplitByPivot(start, splitItem, sortResult)), Task.Factory.StartNew(() => SplitByPivot(splitItem.Next, end, sortResult)));
            //if use an array, then sorting parts can be performed in different threads because parts data do not intersected
            //item of double linked list intersect his references. I do not found simple way to lock. If lock all list then efficient do not improve               

        }

        private void Swap(ListItem item1, ListItem item2)
        {
            var item1Previous = item1.Previous;
            var item1Next = item1.Next;
            var item2Previous = item2.Previous;
            var item2Next = item2.Next;

            if (item1Previous != null) item1Previous.Next = item2;
            if (item2Next != null) item2Next.Previous = item1;

            if (item1.Next == item2)
            {
                item2.Next = item1;
                item1.Next = item2Next;
                item2.Previous = item1Previous;
                item1.Previous = item2;
            }
            else
            {
                if (item2Previous != null) item2Previous.Next = item1;
                if (item1Next != null) item1Next.Previous = item2;
                item1.Previous = item2Previous;
                item1.Next = item2Next;
                item2.Previous = item1Previous;
                item2.Next = item1Next;
            }

            if (First == item1) First = item2;
            else if (First == item2) First = item1;

            if (Last == item1) Last = item2;
            else if (Last == item2) Last = item1;
        }

        private ListItem InitializeLits(int itemValue)
        {
            var newItem = new ListItem { Value = itemValue };
            First = newItem;
            Last = newItem;
            ItemsCount = 1;
            return newItem;
        }
    }
}
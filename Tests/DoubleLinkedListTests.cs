using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DoubleLinkedListTests
    {
        private static readonly int[] LIST_DATA = { 62, 84, 96, 9, 12, 45, 45, 645, 775, 85, 545 };

        [TestMethod]
        public void DoubleLinkedListSouldCorrectAddItemToTop()
        {
            var doubleLinkedList = new DoubleLinkedList.DoubleLinkedList();
            foreach (var i in LIST_DATA)
            {
                doubleLinkedList.AddToTop(i);
            }
            Assert.IsTrue(LIST_DATA.SequenceEqual(doubleLinkedList.GetItems().Select(i => i.Value).Reverse()));
            Assert.AreEqual(doubleLinkedList.ItemsCount, LIST_DATA.Length);
        }

        [TestMethod]
        public void DoubleLinkedListSouldCorrectAddItemToEnd()
        {
            var doubleLinkedList = new DoubleLinkedList.DoubleLinkedList();
            foreach (var i in LIST_DATA)
            {
                doubleLinkedList.AddToEnd(i);
            }
            Assert.IsTrue(LIST_DATA.SequenceEqual(doubleLinkedList.GetItems().Select(i => i.Value)));
            Assert.AreEqual(doubleLinkedList.ItemsCount, LIST_DATA.Length);
        }

        private DoubleLinkedList.DoubleLinkedList CreateList()
        {
            var doubleLinkedList = new DoubleLinkedList.DoubleLinkedList();
            foreach (var i in LIST_DATA)
            {
                doubleLinkedList.AddToEnd(i);
            }
            return doubleLinkedList;
        }

        [TestMethod]
        public void DoubleLinkedListSouldCorrectBubbleMethodSort()
        {
            var doubleLinkedList = CreateList();
            doubleLinkedList.BubbleMethodSort();
            Assert.IsTrue(LIST_DATA.OrderBy(d => d).SequenceEqual(doubleLinkedList.GetItems().Select(i => i.Value)));
        }

        [TestMethod]
        public void DoubleLinkedListSouldCorrectInsertSort()
        {
            var doubleLinkedList = CreateList();
            var sortedList = doubleLinkedList.InsertSort(out var c);
            Assert.IsTrue(LIST_DATA.OrderBy(d => d).SequenceEqual(sortedList.GetItems().Select(i => i.Value)));
        }

        [TestMethod]
        public void DoubleLinkedListSouldCorrectQuickSort()
        {
            var doubleLinkedList = CreateList();
            doubleLinkedList.QuickSort();
            Assert.IsTrue(LIST_DATA.OrderBy(d => d).SequenceEqual(doubleLinkedList.GetItems().Select(i => i.Value)));
        }
    }
}

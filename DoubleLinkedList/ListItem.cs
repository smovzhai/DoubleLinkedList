using System;
using System.Collections;
using System.Linq;

namespace DoubleLinkedList
{
    public class ListItem
    {
        public ListItem Previous { get; set; }

        public ListItem Next { get; set; }

        public int Value { get; set; }
    }
}
using System;
using System.Collections;
using System.Linq;

namespace DoubleLinkedList
{
    public class SortResult
    {   
        public int ComparisonsCount { get; private set; }

        public int SwapsCount { get; private set; }

        public void IncComparisons()
        {
            ComparisonsCount++;
        }

        public void IncSwaps()
        {
            SwapsCount++;
        }


    }
}

using System;
using System.Collections;
using System.Linq;

namespace DoubleLinkedList
{
    public class SortResult
    {
        public SortResult()
        {
        }

        public SortResult(int comparisonsCount, int swapsCount)
        {
            ComparisonsCount = comparisonsCount;
            SwapsCount = swapsCount;
        }

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
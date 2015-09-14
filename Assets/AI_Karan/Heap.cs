using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapObj<T>
{
    T[] heapObjects;
    int heapCount;

    public Heap(int maxSize)
    {
        heapObjects = new T[maxSize];
    }

    public void Add(T obj)
    {
        obj.heapIndex = heapCount;
        heapObjects[heapCount] = obj;
        SortUp(obj);
        heapCount++;
    }

    void SortUp(T obj)
    {
        int parentIndex = (obj.heapIndex - 1) / 2;
        while(true)
        {
            T parentItem = heapObjects[parentIndex];
            //cmpare and check with the parent if the priority is higher, if yes, swap. if not break. while its true, keep calculating the parent index
            if (obj.CompareTo(parentItem) > 0)
                Swap(obj, parentItem);
            else
                break;

            parentIndex = (obj.heapIndex - 1) / 2;
        }
    }

    void Swap(T objA, T objB)
    {
        heapObjects[objA.heapIndex] = objB;
        heapObjects[objB.heapIndex] = objA;
        int tempObjAIndex = objA.heapIndex;
        objA.heapIndex = objB.heapIndex;
        objB.heapIndex = tempObjAIndex;
    }

    public T RemoveFirst()
    {
        T firstObj = heapObjects[0];
        heapCount--;
        heapObjects[0] = heapObjects[heapCount];
        heapObjects[0].heapIndex = 0;
        SortDown(heapObjects[0]);
        return firstObj;
    }

    void SortDown(T obj)
    {
        while(true)
        {
            int indexLeftChild = (2 * obj.heapIndex) + 1;
            int indexRightChild = (2 * obj.heapIndex) + 2;
            int indexToSwap = 0;
        
            if (indexLeftChild < heapCount)
            {
                indexToSwap = indexLeftChild;          
                if (indexRightChild < heapCount)
                {
                    if (heapObjects[indexLeftChild].CompareTo(heapObjects[indexRightChild]) < 0)
                        indexToSwap = indexRightChild;               
                }
                if (obj.CompareTo(heapObjects[indexToSwap]) < 0)
                    Swap(obj, heapObjects[indexToSwap]);
                else
                    return;

            }
            else
                return;
        }
    }

    public int Count
    {
        get { return heapCount; }
    }

    public void UpdateItem(T obj)
    {
        SortUp(obj);
    }

    public bool Contains(T obj)
    {
        return Equals(heapObjects[obj.heapIndex], obj);
    }
}

public interface IHeapObj<T> : IComparable<T>
{
    int heapIndex
    {
        get;
        set;
    }
}

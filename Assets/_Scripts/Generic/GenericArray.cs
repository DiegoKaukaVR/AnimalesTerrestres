using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class IAClass { }
class IAClass2 { }
/// <summary>
/// Inheritance in Generic Class may be another generic or a typed class
/// </summary>
class BaseClass<T> { }
class DerivedClass : BaseClass<int>{ }
class DerivedGenericClass<T> : BaseClass<T> { }

public class GenericStruct<T> where T : struct
{

}

public class GenericPharameter<T> where T : new()
{

}

class Genericenemy<T> where T : IAClass
{

}
public class GenericArray<T> where T : class, new()
{
    T[] array;

    public GenericArray(int size) { array = new T[size]; }

    public void SetItem(int index, T value) 
    {
        if (index > array.Length -1)
        {
            return;
        }
        array[index] = value;
    }

    public T GetItem(int index)
    {
        if (array[index] == null)
        {
            Debug.LogError("Null");
        }
        if (index > array.Length - 1)
        {
            Debug.LogError("OutOfRange");
        }
        return array[index];
    }

    // In generics comparison must be done with "Equals()" not  "=="
    public bool Check(T value, int number)
    {
        if (value.Equals(number)) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  
  
    class Compare
    {
        public static bool CompareValues <Y, H> (Y values1, H values2)
        {
            return values1.Equals(values2);
        }

        public static bool CompareTypes <J, K>(J value, K value2)
        {
            return value.GetType().Equals(value2.GetType());
        }
        public static bool CompareTypes2<J,K>(J value, K value2)
        {
            return typeof(J).Equals(typeof(K));
        }

        
    }

    public class GenericStack<T>
    {
        public class Node
        {
            public T data;
            public Node next;

            public Node(T t)
            {
                data = t;
                next = null;
            }
        }

        Node head;

        public GenericStack()
        {
            head = null;
        }

        public void AddItem(T t) 
        {
            Node n = new Node(t);
            n.next = head;
            head = n;
        }
        public T GetHead() 
        {
            T data = head.data;
            head = head.next;
            return data;
        }
    }


}

using System.Collections;

namespace Lab7Library;

public class DoublyLinkedList : IEnumerable<double>
{
    private Node? head;
    private Node? tail;
    private int count;

    public int Count => this.count;

    public void AddFirst(double data) //включення до початку
    {
        Node newNode = new Node(data);

        if (this.head == null)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            newNode.SetNext(this.head);
            this.head.SetPrevious(newNode);
            this.head = newNode;
        }
        this.count++;
    }

    public double this[int index] //індексатор
    {
        get
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException("Індекс поза межами списку.");

            Node current = this.head!;
            for (int i = 0; i < index; i++)
            {
                current = current.Next!;
            }
            return current.Data;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= this.count)
            throw new IndexOutOfRangeException("Індекс поза межами списку.");

        if (index == 0)
        {
            this.head = this.head!.Next;
            if (this.head != null)
                this.head.SetPrevious(null);
            else
                this.tail = null;
        }
        else if (index == this.count - 1)
        {
            this.tail = this.tail!.Previous;
            if (this.tail != null)
                this.tail.SetNext(null);
            else
                this.head = null;
        }
        else
        {
            Node current = this.head!;
            for (int i = 0; i < index; i++)
            {
                current = current.Next!;
            }

            current.Previous!.SetNext(current.Next);
            current.Next!.SetPrevious(current.Previous);
        }
        this.count--;
    }
    public double? FindFirstLessThanAverage(out double average)
    {
        average = 0;
        if (this.count == 0) return null;

        double sum = 0;
        Node? current = this.head;

        while (current != null)
        {
            sum += current.Data;
            current = current.Next;
        }

        average = sum / this.count;

        current = this.head;
        while (current != null)
        {
            if (current.Data < average)
                return current.Data;
            current = current.Next;
        }

        return null;
    }

    private Node? FindMaxNode()
    {
        if (this.head == null) return null;

        Node maxNode = this.head;
        Node? current = this.head.Next;

        while (current != null)
        {
            if (current.Data > maxNode.Data)
            {
                maxNode = current;
            }
            current = current.Next;
        }
        return maxNode;
    }

    public double SumAfterMax(out double maxValue)
    {
        maxValue = 0;
        Node? maxNode = this.FindMaxNode();
        if (maxNode == null || maxNode.Next == null) return 0;

        maxValue = maxNode.Data;

        if (maxNode.Next == null) return 0;

        double sum = 0;
        Node? current = maxNode.Next;
        while (current != null)
        {
            sum += current.Data;
            current = current.Next;
        }
        return sum;
    }

    public DoublyLinkedList GetElementsGreaterThan(double threshold)
    {
        DoublyLinkedList newList = new DoublyLinkedList();
        
        Node? current = this.tail;
        while (current != null)
        {
            if (current.Data > threshold)
            {
                newList.AddFirst(current.Data);
            }
            current = current.Previous;
        }
        return newList;
    }

    public void RemoveBeforeMax()
    {
        Node? maxNode = this.FindMaxNode();
        if (maxNode == null || maxNode == this.head) return;

        this.head = maxNode;
        this.head.SetPrevious(null);

        int newCount = 0;
        Node? current = this.head;
        while (current != null)
        {
            newCount++;
            current = current.Next;
        }
        this.count = newCount;
    }

    public IEnumerator<double> GetEnumerator() //форіч
    {
        Node? current = this.head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
 }

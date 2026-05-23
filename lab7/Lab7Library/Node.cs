namespace Lab7Library;

public class Node
{
    private double data;
    private Node? next;
    private Node? previous;

    public double Data => this.data;
    public Node? Next => this.next;
    public Node? Previous => this.previous;

    public Node(double data) => this.data = data;

    public void SetNext(Node? nextNode) => this.next = nextNode;
    public void SetPrevious(Node? previousNode) => this.previous = previousNode;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;

    public int x;
    public int y;
    public int g;
    public int h;
    public bool open;

    public Node(int x, int y, bool open)
    {
        this.x = x;
        this.y = y;
        this.open = open;
    }

    public int getF()
    {
        return g + h;
    }
}

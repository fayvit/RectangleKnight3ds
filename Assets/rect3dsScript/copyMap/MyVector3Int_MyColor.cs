using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MyVector2Int
{
    int X;
    int Y;

    public MyVector2Int(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int x { get { return X; } }
    public int y { get { return Y; } }
}

[System.Serializable]
public struct MyColor
{
    private float r;
    private float g;
    private float b;

    public MyColor(Color C)
    {
        this.r = C.r;
        this.g = C.g;
        this.b = C.b;
    }

    public Color cor { get { return new Color(r, g, b); } }
}
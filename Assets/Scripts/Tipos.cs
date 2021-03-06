﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Terreno { libre,agua, cienaga, muro };
public enum Direccion { arriba, abajo, izquierda, derecha, idle };
public enum ColorUnidad { rojo, ninguno };

public class Pos
{
    public Pos(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public int GetX() { return _x; }
    public int GetY() { return _y; }

    public void SetX(int x) { _x = x; }
    public void SetY(int y) { _y = y; }


    int _x;
    int _y;

    public static bool operator ==(Pos a, Pos b)
    {
        return (a.GetX() == b.GetX() && a.GetY() == b.GetY());
    }

    public static bool operator !=(Pos a, Pos b)
    {
        return (a.GetX() != b.GetX() || a.GetY() != b.GetY());
    }

    public override string ToString()
    {
        return string.Format(_x.ToString() + ":" + _y.ToString());
    }
}





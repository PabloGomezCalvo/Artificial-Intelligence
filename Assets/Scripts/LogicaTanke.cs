using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LogicaTanke
{
    ColorUnidad _tipo;
    Pos _pos;
    Pos _flecha;

    public LogicaTanke()
    {
        _pos = new Pos(0, 0);
        _flecha = new Pos(0, 0);
        _tipo = ColorUnidad.ninguno;
    }

    public LogicaTanke(ColorUnidad tipo, Pos pos)
    {
        _tipo = tipo;
        _pos = pos;
        _flecha = new Pos(pos.GetX(), pos.GetY());
    }

    public ColorUnidad GetTipoTanke() { return _tipo; }
    public void SetTipoTanke(ColorUnidad tipo) { _tipo = tipo; }


    public Pos GetPos() { return _pos; }
    public void SetPos(Pos pos) { _pos = pos; }


    public Pos GetFlecha() { return _flecha; }
    public void SetFlecha(Pos flecha) { _flecha = flecha; }

}


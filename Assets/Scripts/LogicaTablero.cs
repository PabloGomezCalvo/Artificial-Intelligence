using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LogicaTablero
{

    LogicaTile[,] _matriz;

    public LogicaTablero(int alto, int ancho, bool predeterminado)
    {
        _matriz = new LogicaTile[alto, ancho];
        Random rnd = new Random();

        //i son filas
        if (!predeterminado) { 

        for (int y = 0; y < alto; y++)
        {
            for (int x = 0; x < ancho; x++)
            {
                int random = rnd.Next(0, 10);
                //Mar
                if (random <= 4)
                    _matriz[y, x] = new LogicaTile(Terreno.libre, new Pos(x, y));
                else if (random <= 7 && random > 4)
                    _matriz[y, x] = new LogicaTile(Terreno.agua, new Pos(x, y));

                //Mar profundo
                else if (random == 8)
                    _matriz[y, x] = new LogicaTile(Terreno.cienaga, new Pos(x, y));

                //Muro
                else
                    _matriz[y, x] = new LogicaTile(Terreno.muro, new Pos(x, y));
            }

        }
    }
        else
        {
            for (int y = 0; y < alto; y++)
                for (int x = 0; x < ancho; x++)
                    _matriz[y, x] = new LogicaTile(Terreno.libre, new Pos(x, y));

        }

    }
    

    public LogicaTile GetLogicaTile(int x, int y) { return _matriz[y, x]; }
    public LogicaTile GetLogicaTile(Pos pos) { return _matriz[pos.GetY(), pos.GetX()]; }
    public LogicaTile[,] GetMatriz() { return _matriz; }


}

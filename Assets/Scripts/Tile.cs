using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{

    LogicaTile _logicaTile;

    // Use this for initialization


    public void ConstruyeCasilla(LogicaTile logicaTile)
    {
        _logicaTile = logicaTile;
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.GetSeleccionado() == ColorUnidad.ninguno)
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();

            switch (_logicaTile.GetTerreno())
            {
                case Terreno.libre:
                    _logicaTile.SetTerreno(Terreno.agua);
                    render.sprite = GameManager.instance.spriteAgua;

                    break;

                case Terreno.agua:
                    _logicaTile.SetTerreno(Terreno.cienaga);
                    render.sprite = GameManager.instance.spriteCienaga;
                    break;
                case Terreno.cienaga:
                    _logicaTile.SetTerreno(Terreno.muro);
                    render.sprite = GameManager.instance.spriteMuro;
                    break;
                case Terreno.muro:
                    _logicaTile.SetTerreno(Terreno.libre);
                    render.sprite = GameManager.instance.spriteLibre;
                    break;

            }
        }
        else if (_logicaTile.GetTerreno() == Terreno.muro)
        {
            GameManager.instance.Deselecciona();
        }

        //Mover
        else
        {
            GameManager.instance.MoverPJ(_logicaTile.GetPos());
        }

    }
}

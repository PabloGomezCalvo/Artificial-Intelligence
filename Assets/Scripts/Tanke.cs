using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;



public class Tanke : MonoBehaviour
{

    LogicaTanke _logicaTanke;

    Sprite _spriteTanke;
    Sprite _spriteTankeSeleccionado;

    GameObject _flecha;

    List<GameObject> flechasCamino;

    Stopwatch _reloj;
    AEstrella A;

    // Use this for initialization
    void Start()
    {
        _flecha.SetActive(false);
        _reloj = new Stopwatch();
    }

    public void ConstruyeTanke(LogicaTanke logicaTanke, Sprite spriteTanke, Sprite spriteTankeSeleccionado, GameObject flecha)
    {
        _logicaTanke = logicaTanke;
        _spriteTanke = spriteTanke;
        _spriteTankeSeleccionado = spriteTankeSeleccionado;
        _flecha = flecha;
        flechasCamino = new List<GameObject>();
        GetComponent<SpriteRenderer>().sprite = spriteTanke;
    }

   

    public void SetSpriteDeseleccionado()
    {
        GetComponent<SpriteRenderer>().sprite = _spriteTanke;
    }

    public LogicaTanke GetLogicaTanke()
    {
        return _logicaTanke;
    }
    public void Mueve(List<Pos> camino)
    {
        if (camino != null)
            StartCoroutine("AvanzaUnPaso", camino);
        else
            QuitaFlecha();
    }


    public void EmpiezaMovimiento(Pos pos)
    {
        _logicaTanke.SetFlecha(new Pos(pos.GetX(), pos.GetY()));

        _flecha.transform.position = new Vector3(pos.GetX() * GameManager.Distancia, -pos.GetY() * GameManager.Distancia, 0);

        _flecha.SetActive(true);

        GameManager.instance.SetSeleccionado(ColorUnidad.ninguno, null);

        SetSpriteDeseleccionado();

        _reloj.Reset();

        _reloj.Start();



        AEstrella A = new AEstrella(GameManager.instance.GetLogicaTablero().GetMatriz(), _logicaTanke.GetPos(), pos);


        _reloj.Stop();
        GameManager.instance.escribeTiempo(_reloj.ElapsedMilliseconds.ToString());


        DisplayPath(A.GetCamino());
        Mueve(A.GetCamino());


    }


    public void DisplayPath(List<Pos> camino)
    {
        
        var a = camino.GetEnumerator();
        while (a.MoveNext())
        {
            Pos newPos = a.Current;
            flechasCamino.Add(Instantiate(_flecha, new Vector3(newPos.GetX() * GameManager.Distancia, -newPos.GetY() * GameManager.Distancia), Quaternion.identity));
        }

    }

 

    IEnumerator AvanzaUnPaso(List<Pos> camino)
    {
        while (camino.Any() && GameManager.instance.GetLogicaTablero().GetLogicaTile(camino.First()).GetTerreno() != Terreno.muro)
        {

            Pos newPos = camino.Last();

            _logicaTanke.SetPos(newPos);

            this.gameObject.transform.position = new Vector3(newPos.GetX() * GameManager.Distancia, -newPos.GetY() * GameManager.Distancia, -1);

            if (GameManager.instance.GetLogicaTablero().GetLogicaTile(newPos).GetTerreno() == Terreno.agua)
            {
                camino.RemoveAt(camino.Count -1);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                camino.RemoveAt(camino.Count - 1);
                yield return new WaitForSeconds(0.4f);
            }
        }

        var a = flechasCamino.GetEnumerator();

        while (a.MoveNext())
        {

            Destroy(a.Current);

        }

        QuitaFlecha();

    }

    void QuitaFlecha()
    {
        _logicaTanke.SetFlecha(_logicaTanke.GetPos());
        _flecha.SetActive(false);
        _flecha.transform.position = new Vector3(_logicaTanke.GetPos().GetX() * GameManager.Distancia, -_logicaTanke.GetPos().GetY() * GameManager.Distancia, 0);
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.GetSeleccionado() == ColorUnidad.ninguno && _logicaTanke.GetFlecha() == _logicaTanke.GetPos())
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            render.sprite = _spriteTankeSeleccionado;

            GameManager.instance.SetSeleccionado(_logicaTanke.GetTipoTanke(), this.gameObject);
        }
        else if (GameManager.instance.GetSeleccionado() != ColorUnidad.ninguno)
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            render.sprite = _spriteTanke;

            GameManager.instance.Deselecciona();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public Text textoReloj;
    public const int Ancho = 13;
    public const int Alto = 10;
    public const int WorldSize = Alto * Ancho;
    LogicaTablero _logicaTablero;

    public const float Distancia = 0.64f;

    //--------ATRIBUTOS--------

    ColorUnidad _seleccionado;
    public GameObject tilePrefab;
    public GameObject personajePrefab;
    public GameObject flechaPrefab;

    public Sprite spriteLibre;
    public Sprite spriteAgua;
    public Sprite spriteCienaga;
    public Sprite spriteMuro;

    public Sprite spritePersonaje;
    public Sprite spritePersonajeSeleccionado;


    public Sprite spriteFlecha;



    public int mode;

    //--------ATRIBUTOS--------

    GameObject _personajeSeleccionado;


    // Use this for initialization
    void Start()
    {
        mode = 1;
        instance = this;
        _personajeSeleccionado = null;

        _logicaTablero = new LogicaTablero(Alto,Ancho,false);
        colocaTablero();
        _seleccionado = ColorUnidad.ninguno;
        ConstruyeUnidades();
    }

    //---------------CONSTRUCCIÓN TILES------------------------

    //Pasa la representación lógica del tablero (matriz) a la representación física (gameobjects)
    void colocaTablero()
    {
        GameObject GOTablero = new GameObject("Tablero");

        for (int y = 0; y < Alto; y++)
        {
            for (int x = 0; x < Ancho; x++)
            {
                //Creamos gameObject
                GameObject GOTileAux = Instantiate(tilePrefab, new Vector3(x * Distancia, -y * Distancia, 5), Quaternion.identity, GOTablero.transform);

                LogicaTile tileAux = _logicaTablero.GetLogicaTile(x, y);

                //SpriteRenderer
                switch (tileAux.GetTerreno())
                {
                    case Terreno.agua:
                        GOTileAux.GetComponent<SpriteRenderer>().sprite = spriteAgua;
                        break;

                    case Terreno.libre:
                        GOTileAux.GetComponent<SpriteRenderer>().sprite = spriteLibre;
                        break;

                    case Terreno.muro:
                        GOTileAux.GetComponent<SpriteRenderer>().sprite = spriteMuro;
                        break;
                    case Terreno.cienaga:
                        GOTileAux.GetComponent<SpriteRenderer>().sprite = spriteCienaga;
                        break;
                }

                //Casilla
                GOTileAux.GetComponent<Tile>().ConstruyeCasilla(tileAux);
            }

        }

    }

    //---------------CONSTRUCCIÓN TILES------------------------


    //---------------CONSTRUCCIÓN UNIDADES------------------------

    void ConstruyeUnidades()
    {
        Pos[] pos = new Pos[1];

        for (int i = 0; i < 1; i++)
            pos[i] = new Pos(-1, -1);

        CreaPersonaje("Personaje", ColorUnidad.rojo, spritePersonaje, spritePersonajeSeleccionado, spriteFlecha, ref pos);

    }

    void CreaPersonaje(string nombre, ColorUnidad tipo, Sprite spritePersonaje, Sprite spritePersonajeSeleccionado, Sprite spriteFlecha, ref Pos[] pos)
    {
        Pos posAux = new Pos(Random.Range(0, 10), Random.Range(0, 10));

        bool haypj = HayPJ(posAux, pos);

        while (_logicaTablero.GetLogicaTile(posAux).GetTerreno() == Terreno.muro || haypj)
        {
            posAux = new Pos(Random.Range(0, 10), Random.Range(0, 10));
            haypj = HayPJ(posAux, pos);

        }

        pos[(int)tipo] = posAux;
        GameObject pj = Instantiate(personajePrefab, new Vector3(posAux.GetX() * Distancia, -posAux.GetY() * Distancia, -1), Quaternion.identity);
        pj.name = nombre;

        LogicaTanke logicaTanke = new LogicaTanke(tipo, posAux);

        //Construcción de flecha
        GameObject flecha = Instantiate(flechaPrefab, new Vector3(posAux.GetX() * Distancia, -posAux.GetY() * Distancia, 3), Quaternion.identity);
        flecha.GetComponent<SpriteRenderer>().sprite = spriteFlecha;

        pj.GetComponent<Tanke>().ConstruyeTanke(logicaTanke, spritePersonaje, spritePersonajeSeleccionado, flecha);
    }
    //Comprueba si hay un PJ en una posición
    bool HayPJ(Pos pos, Pos[] posPJs)
    {
        bool haypj = false;

        int i = 0;
        while (!haypj && i < 1)
        {
            if (posPJs[i] == pos)
                haypj = true;
            i++;
        }
        return haypj;
    }

    //---------------CONSTRUCCIÓN UNIDADES------------------------


    public ColorUnidad GetSeleccionado() { return _seleccionado; }

    public void SetSeleccionado(ColorUnidad color, GameObject tanke)
    {
        _seleccionado = color;
        _personajeSeleccionado= tanke;

    }

    public void MoverPJ(Pos pos)
    {
        _personajeSeleccionado.GetComponent<Tanke>().EmpiezaMovimiento(pos);
    }

    public LogicaTablero GetLogicaTablero()
    {
        return _logicaTablero;
    }

    public void Deselecciona()
    {
        _personajeSeleccionado.GetComponent<Tanke>().SetSpriteDeseleccionado();
        SetSeleccionado(ColorUnidad.ninguno, null);
    }
    public void escribeTiempo(string texto)
    {
        textoReloj.text = "Tiempo: " + texto + "ms";
    }

    //---------------Cambio De Heuristica------------------------
    public void setMode1()
    {
        mode = 1;
    }

    public void setMode2()
    {
        mode = 2;
    }


    public void setMode3()
    {
        mode = 3;
    }

    public void reConstruirMapa()
    {
        
        Destroy(GameObject.Find("Tablero"));
        Destroy(GameObject.Find("Personaje"));
        Destroy(GameObject.Find("Flecha(Clone)"));
        _personajeSeleccionado = null;

        _logicaTablero = new LogicaTablero(Alto, Ancho,false);
        colocaTablero();
        _seleccionado = ColorUnidad.ninguno;
        ConstruyeUnidades();

    }

    public void mapaFijo()
    {
        Destroy(GameObject.Find("Tablero"));
        Destroy(GameObject.Find("Personaje"));
        Destroy(GameObject.Find("Flecha(Clone)"));
        _personajeSeleccionado = null;
        _logicaTablero = new LogicaTablero(Alto, Ancho,true);
        colocaTablero();
        _seleccionado = ColorUnidad.ninguno;
        ConstruyeUnidades();
    }
}

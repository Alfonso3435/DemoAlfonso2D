using UnityEngine;

/**
Para saber si el personaje está en el piso o no.
Autor: Juan Alfonso Vega Sol
*/

public class EstadoPersonaje : MonoBehaviour
{
    public static bool enPiso { get; private set; }

    void Start()
    {
        enPiso = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        enPiso = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        enPiso = false;
    }

}

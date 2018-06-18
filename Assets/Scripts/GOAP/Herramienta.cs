using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herramienta : MonoBehaviour {

    public int duracion = 2;

	// Use this for initializatio

    public void Usar()
    {
        duracion--;

        if (EstaRota())
            gameObject.SetActive(false);//sumular que se rompio
    }

    public bool EstaRota()
    {
        if (duracion < 0)
            return true;
        else
            return false;
    }

    public void Restaurar()
    {
        duracion=2;
        gameObject.SetActive(true);

    }

	

}

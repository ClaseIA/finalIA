using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion_AlmacenarMadera : GoapAction
{
    public Accion_AlmacenarMadera()
    {
        // Donde vamos a poner las precondiciones y efectos de la acción
        AddPrecondition("hayMadera", true);

        AddEffect("almacenarMadera", true);
        AddEffect("hayMadera", false);
    }

    // variables de la acción
    private bool terminado = false;
    private float tiempoInicio = 0;
    public float duracionAccion = 1;

    public override bool isDone()
    {
        return terminado;
    }

    public override bool requiresInRange()
    {
        // Si, necesita estar cerca de un almacen para depositar la herramienta
        return true;
    }

    public override void reset()
    {
        terminado = false;
        Target = null;
        tiempoInicio = 0;
    }

    public override bool checkPreconditions(GameObject go)
    {
        // Esto se refiere a precondiciones procedurales, es decir, que conllevan otras operaciones

        // Tengo que estar cerca de un "almacen" para dejar la herramienta
        GameObject[] almacenes = GameObject.FindGameObjectsWithTag("Almacen");

        GameObject almacenCercano = null;
        float distanciaMenor = 0;

        foreach(GameObject almacen in almacenes)
        {
            if(almacenCercano == null)
            {
                almacenCercano = almacen;
                distanciaMenor = Vector3.Distance(transform.position, almacen.transform.position);
            }
            else
            {
                float dist = Vector3.Distance(almacen.transform.position, transform.position);

                if(dist < distanciaMenor)
                {
                    // encontramos uno mas cercano
                    almacenCercano = almacen;
                    distanciaMenor = dist;
                }
            }
        } // foreach

        if (almacenCercano == null)
            return false;

        Target = almacenCercano;

        return true;
    }

    public override bool Perform(GameObject gameobject)
    {
        // El agente se tardará un poco en realizar esta acción
        if (tiempoInicio == 0)
            tiempoInicio = Time.timeSinceLevelLoad;

        if(Time.timeSinceLevelLoad > tiempoInicio + duracionAccion)
        {
           //tomar la madera que tengo en el inventario y ponerla en el almacern
            Target.GetComponent<Inventario>().madera += GetComponent<Inventario>().madera;
            //como ya deje la madera, el agente se queda sin madera
            GetComponent<Inventario>().madera = 0;
            terminado = true;
        }
        return true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

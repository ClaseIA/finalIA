using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion_ConstruirCasita : GoapAction
{
    public Accion_ConstruirCasita()
    {
        // Donde vamos a poner las precondiciones y efectos de la acción
        AddPrecondition("tengoMaterialCasita", true);
        AddPrecondition("tengoCasa", false);



        AddEffect("tenerCasa", true);
        AddEffect("tengoMaterialCasita", false);
        AddEffect("tengoCasa", true);
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
    

        Target = GetComponent<Inventario>().miCasa;

        return true;
    }

    public override bool Perform(GameObject gameobject)
    {
        GameObject herramienta = GetComponent<Inventario>().herramienta;
        // El agente se tardará un poco en realizar esta acción
        if (tiempoInicio == 0)
            tiempoInicio = Time.timeSinceLevelLoad;

        if(Time.timeSinceLevelLoad > tiempoInicio + duracionAccion)
        {
          
            GetComponent<Inventario>().casita = 0;
            GetComponent<Inventario>().miCasa.GetComponent<casitaScript>().destruida = false;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion_CortarArbol : GoapAction
{
    public Accion_CortarArbol()
    {
        // Donde vamos a poner las precondiciones y efectos de la acción
        AddPrecondition("hayMadera", false);
        AddPrecondition("hayHerramienta", true);
        
        // AddPrecondition("tenerCasa", true);

        AddEffect("hayMadera",true);
       
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
        // Si, necesita estar cerca de unarbol
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

        // Tengo que estar cerca de un "arbol"
        GameObject[] forjas = GameObject.FindGameObjectsWithTag("Arbol");

        GameObject forjaCercana = null;
        float distanciaMenor = 0;

        foreach(GameObject forja in forjas)
        {
            if(forjaCercana == null)
            {
                forjaCercana = forja;
                distanciaMenor = Vector3.Distance(transform.position, forja.transform.position);
            }
            else
            {
                float dist = Vector3.Distance(forja.transform.position, transform.position);

                if(dist < distanciaMenor)
                {
                    // encontramos uno mas cercano
                    forjaCercana = forja;
                    distanciaMenor = dist;
                }
            }
        } // foreach

        if (forjaCercana == null)
            return false;

        Target = forjaCercana;

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

            if (!herramienta.GetComponent<Herramienta>().EstaRota())
            {
                //necesito acceder a la herramienta para cortar el arbol

                // TODO: herramietna sufra desgaste

                herramienta.GetComponent<Herramienta>().Usar();

                //tomar madera del arbol

                if (Target.GetComponent<Inventario>().madera >= 5)
                {
                    //le queda madera al arbol
                    GetComponent<Inventario>().madera += 5; //tomo madera
                    Target.GetComponent<Inventario>().madera -= 5; //le quito madera

                    terminado = true;
                }
            }

            else {
                Debug.Log("sin herramienta");
                terminado = true;

            }
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

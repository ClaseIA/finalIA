using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion_TomarHerramienta : GoapAction
{
    public Accion_TomarHerramienta()
    {
        // Donde vamos a poner las precondiciones y efectos de la acción
        AddPrecondition("hayHerramienta", false);

        AddEffect("hayHerramienta", true);
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
        // Si, necesita estar cerca de un almacen
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
        GameObject[] almacenes = GameObject.FindGameObjectsWithTag("Tienda");

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
            // Verificar si hay mineral en el almacen
            if(Target.GetComponent<Inventario>().herramientas >= 1) // 3 es el mineral que necesito para trabajar
            {
                // le quito al almacen
                Target.GetComponent<Inventario>().herramientas -= 1;

                GetComponent<Inventario>().herramientas += 1;

                terminado = true;

                return true;
            }
            else
            {
                // llego al almacen pero no hay mineral
                return false;
            }
        }
        return true;
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accion_IrCanasta : GoapAction
{
    public Accion_IrCanasta()
    {
        // Donde vamos a poner las precondiciones y efectos de la acción
        AddPrecondition("hayComida", true);




        // AddEffect("tenerCasa", true);
        AddEffect("cosechar", true);
    }

    // variables de la acción
    private bool terminado = false;
    private float tiempoInicio = 0;
    public float duracionAccion = 1;
    public GameObject canasta;

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
     

        Target = canasta;

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
           //tomar la madera que tengo en el inventario y ponerla en el almacern
            Target.GetComponent<Inventario>().comida += GetComponent<Inventario>().comida;
            //como ya deje la madera, el agente se queda sin madera
            GetComponent<Inventario>().comida = 0;
           
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

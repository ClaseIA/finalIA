using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hobbit : Trabajador
{
   
    // Su objetivo es crear herramientas y ponerlas en un almacen

    public override Dictionary<string, object> CreateGoalState()
    {
        Dictionary<string, object> meta = new Dictionary<string, object>();

        // puede tener varias metas

        if (inventario.miCasa.GetComponent<casitaScript>().destruida && almacen.GetComponent<Inventario>().casita > 0)
            meta.Add("tenerCasa", true);

        else
        {
            if (campo.GetComponent<CampoScript>().listoCosechar && canasta.activeSelf&& canasta.GetComponent<Inventario>().comida<8)
            {
               // Debug.Log("holaaaaaaaaaaaaaaaaaa");
                meta.Add("cosechar", true);
            }
            else
                meta.Add("almacenarMadera", true);
        }

        
       

        Debug.Log("Se propuso una meta en la vida");

        return meta;
    }

  

    }

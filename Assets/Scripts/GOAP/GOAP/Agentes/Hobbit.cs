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
        meta.Add("almacenarMadera", true);

        Debug.Log("Se propuso una meta en la vida");

        return meta;
    }

}

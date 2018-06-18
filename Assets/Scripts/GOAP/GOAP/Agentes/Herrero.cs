﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herrero : Trabajador
{
    // Su objetivo es crear herramientas y ponerlas en un almacen

    public override Dictionary<string, object> CreateGoalState()
    {
        Dictionary<string, object> meta = new Dictionary<string, object>();

        // puede tener varias metas
        meta.Add("almacenarHerramienta", true);

        Debug.Log("Se propuso una meta en la vida");

        return meta;
    }

}

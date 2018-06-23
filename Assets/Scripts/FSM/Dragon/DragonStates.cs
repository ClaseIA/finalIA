using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieranC:\Users\carlos.ahuactzin\Documents\finalIA\Assets\Scripts\Elfo\Elfo.cs
    {
        Mining,
        DepositInBank,
        Drinking,
        Sleeping,
        Eating
    }

    //=============================================================
    //===================================================Mining
    public class Mining : State
    {
        private Dragon dragon;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Mining(Dragon _dragon)
        {
            dragon = _dragon;
        }

        public override void OnEnter(GameObject objeto)
        {
           
        }
        public override void Act(GameObject objeto)
        {
           
        }
        public override void Reason(GameObject objeto)
        {
           
        }
        public override void OnExit(GameObject objeto)
        {
           
        }

    }
}

    
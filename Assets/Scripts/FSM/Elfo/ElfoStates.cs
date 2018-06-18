using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfoStates
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
        private Elfo bob;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Mining(Elfo _bob)
        {
            bob = _bob;
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

    
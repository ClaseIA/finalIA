using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrcoStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieranC:\Users\carlos.ahuactzin\Documents\finalIA\Assets\Scripts\Elfo\Elfo.cs
    {
        Deambular,
        Comer,
        Dormir,
        Destruir
    }

    //=============================================================
    //===================================================deambular
    public class Deambular : State
    {
        private Orco orco;

        // Semaforo o candado para tiempos
        private bool Hambre;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Deambular(Orco _orco)
        {
            orco = _orco;
        }

        public override void OnEnter(GameObject objeto)
        {
            orco.steering.wander = true;
            Hambre = false;
        }
        public override void Act(GameObject objeto)
        {
           
        }
        public override void Reason(GameObject objeto)
        {
            if (!Hambre)
            {
                fsm.myMono.StartCoroutine(HambrientoFunction());
            }

            if (orco.Hambre >= 10)
            {
                ChangeState(StateID.Comer);
            }
           
        }
        public override void OnExit(GameObject objeto)
        {
            orco.steering.wander = false;
        }

        IEnumerator HambrientoFunction()
        {
            Hambre = true;
            yield return new WaitForSeconds(5f);
            orco.Hambre += 1;
            Hambre = false;


        }

    }


}


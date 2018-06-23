using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonStates
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
        private Dragon dragon;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Deambular(Dragon _dragon)
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
    //=============================================================
    //===================================================Comer

    public class Comer : State
    {
        private Dragon dragon;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Comer(Dragon _dragon)
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
    //=============================================================
    //===================================================Dormir
    public class Dormir : State
    {
        private Dragon dragon;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Dormir(Dragon _dragon)
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
    //=============================================================
    //===================================================Destruir
    public class Destruir : State
    {
        private Dragon dragon;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Destruir(Dragon _dragon)
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


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
        IrCanasta
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
            if (orco.canasta.GetComponent<Inventario>().comida>=8)
            {
                ChangeState(StateID.IrCanasta);

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
    //=============================================================
    //===================================================Dormir
    public class Dormir : State
    {
        private Orco orco;
        GameObject Bosque;

        // Semaforo o candado para tiempos
        private bool Hambre;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Dormir(Orco _orco)
        {
            orco = _orco;
        }

        public override void OnEnter(GameObject objeto)
        {
            Bosque = GameObject.FindWithTag("Bosque");
            Debug.Log("busandoBosque");
            orco.steering.arrive = true;
            orco.steering.Target = Bosque.transform;
            Hambre = false;
        }
        public override void Act(GameObject objeto)
        {

        }
        public override void Reason(GameObject objeto)
        {
            Debug.Log(Vector3.Distance(orco.transform.position, Bosque.transform.position));

            if (Vector3.Distance(orco.transform.position, Bosque.transform.position) <= 2)
            {
                ChangeState(StateID.Deambular);
            }

            }
        public override void OnExit(GameObject objeto)
        {
            orco.micanasta.SetActive(false);
            orco.steering.arrive = false;
        }

        IEnumerator HambrientoFunction()
        {
            Hambre = true;
            yield return new WaitForSeconds(5f);
            orco.Hambre += 1;
            Hambre = false;


        }

    }

    //=============================================================
    //===================================================IrCanasta
    public class IrCanasta : State
    {
        private Orco orco;
        GameObject Bosque;

        // Semaforo o candado para tiempos
        private bool Hambre;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public IrCanasta(Orco _orco)
        {
            orco = _orco;
        }

        public override void OnEnter(GameObject objeto)
        {
            Bosque = GameObject.FindWithTag("Canasta");
            Debug.Log("buscandoCanasta");
            orco.steering.arrive = true;
            orco.steering.Target = Bosque.transform;
            Hambre = false;
        }
        public override void Act(GameObject objeto)
        {

        }
        public override void Reason(GameObject objeto)
        {
            Debug.Log(Vector3.Distance(orco.transform.position, Bosque.transform.position));

            if (Vector3.Distance(orco.transform.position, Bosque.transform.position) <= 0.5)
            {
                Hambre = true;
           
                ChangeState(StateID.Dormir);
            }
           else if (!orco.canasta.activeSelf)
            {
                Hambre = false;
                ChangeState(StateID.Deambular);
            }

        }
        public override void OnExit(GameObject objeto)
        {
            if (Hambre)
            {
                orco.micanasta.SetActive(true);

                orco.canasta.GetComponent<Inventario>().comida = 0;
                orco.canasta.SetActive(false);

            }
         


            orco.steering.arrive = false;
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


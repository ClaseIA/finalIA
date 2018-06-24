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
        private bool Hambre;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;

        public Deambular(Dragon _dragon)
        {
            dragon = _dragon;
        }

        public override void OnEnter(GameObject objeto)
        {
            dragon.steering.wander = true;
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

            if (dragon.Hambre >= 10)
            {
                ChangeState(StateID.Comer);
            }
           
        }
        public override void OnExit(GameObject objeto)
        {
            dragon.steering.wander = false;
        }

        IEnumerator HambrientoFunction()
        {
            Hambre = true;
            yield return new WaitForSeconds(5f);
            dragon.Hambre += 1;
            Hambre = false;


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
        private Coroutine CheckingCoroutine;
        private Coroutine EatingCoroutine;
        private GameObject cueva;
        bool waitEnds;
        bool comiendo;


        public Comer(Dragon _dragon)
        {
            dragon = _dragon;
        }

        public override void OnEnter(GameObject objeto)
        {
            waitEnds = false;
            comiendo = false;
            cueva = GameObject.FindWithTag("Cueva");
            dragon.steering.arrive = true;
            dragon.steering.maxForce = 1;
            dragon.steering.maxSpeed = 10;
            dragon.steering.Target =cueva.transform;
          

        }
        public override void Act(GameObject objeto)
        {
            if (Vector3.Distance(dragon.transform.position, cueva.transform.position) <= 1)
            {
                Debug.Log("llegando a Cueva");
                dragon.steering.arrive = false;
                dragon.transform.position = cueva.transform.position;
                CheckingCoroutine = fsm.myMono.StartCoroutine(CheckingFunction());


            }

        }
        public override void Reason(GameObject objeto)
        {
            //Debug.Log(Vector3.Distance(dragon.transform.position, cueva.transform.position));
            if (waitEnds)
            {
                if (dragon.manager.comida == false)
                {
                    Debug.Log("no hay comida.. malditos hobits");
                    ChangeState(StateID.Destruir);
                }

                else
                {
                    Debug.Log("yum yum comidita");
                    EatingCoroutine=fsm.myMono.StartCoroutine(ComiendoFunction());
                }

            }

            if (comiendo)
            {
                dragon.Hambre = 0;
                dragon.manager.comida = false;
                fsm.myMono.StopCoroutine(EatingCoroutine);
                ChangeState(StateID.Deambular);
            }


         }
        public override void OnExit(GameObject objeto)
        {
            fsm.myMono.StopCoroutine(CheckingCoroutine);

        }

        IEnumerator CheckingFunction()
        {
            yield return new WaitForSeconds(5f);
            waitEnds = true;


        }


        IEnumerator ComiendoFunction()
        {

            yield return new WaitForSeconds(5f);
             comiendo= true;


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
        private bool arrive;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;
        GameObject comarca;

        public Destruir(Dragon _dragon)
        {
            dragon = _dragon;
        }

        public override void OnEnter(GameObject objeto)
        {
            arrive = false;
            comarca = GameObject.FindWithTag("Comarca");
            Debug.Log("Yendo a la comarca");
            dragon.steering.arrive = true;
            dragon.steering.Target = comarca.transform;

        }
        public override void Act(GameObject objeto)
        {
            if (Vector3.Distance(dragon.transform.position, comarca.transform.position) <= 2)
            {
                Debug.Log("ora si hijos de su jovita madre..");
                arrive = true;
            }

            }
        public override void Reason(GameObject objeto)
        {
            if (arrive)
            {
                arrive = false;
                int casa =  Random.Range(0, dragon.manager.CasasComarca.Length);

                if (dragon.manager.CasasComarca[casa].activeSelf == true)
                {
                    dragon.manager.CasasComarca[casa].SetActive(false);
                    ChangeState(StateID.Comer);
                }
            }

        }
        public override void OnExit(GameObject objeto)
        {
            dragon.steering.arrive = false;

        }

    }
}


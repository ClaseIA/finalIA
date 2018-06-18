using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfosStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
        Patrullando,
        Peleando,
        Descansando,
        Perder,
        Ganar,
        Alimentan

    }

    //=============================================================
    //===================================================
    public class Patrullando : StateID
    {
        private Elfos elfos;

        // Semaforo o candado para tiempos
        private bool patrullar;
        // Una referencia a la corutina
        private Coroutine patrullarCoroutine;

        public Patrullando(Elfos _elfos)
        {
            elfos = _elfos;
        }

        public override void OnEnter(GameObject objeto)
        {
            patrullar = false; // Abrir el candado o apagar el semáforo para permitir la corutina
            Debug.Log("Elfos empezando a patrullar");
        }
        public override void Act(GameObject objeto)
        {
            // Verificar que el candado este abierto o el semaforo apagado
            if (!patrullar)
            {
                patrullarCoroutine = fsm.myMono.StartCoroutine(PatrullarFunction());
            }
        }
        public override void Reason(GameObject objeto)
        {
            if (elfos.fatiga >= 10)
            {
                // Si por alguna razón sigue en ejecución la corutina, la detenemos
                fsm.myMono.StopCoroutine(patrullarCoroutine);

                // Si tiene animacion, la ejecutamos
                SetAnimationTrigger("Descansar");

                ChangeState(StateID.Descansando);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Voy a descansar");
        }

        IEnumerator PatrullarFunction()
        {
            // Cerrar el candado o prender el semáforo
            patrullar = true;

            // Ejecuto las acciones del estado
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Patrullando");
            elfos.fatiga++;

            // Abro el candado o apago el semáforo
            patrullar = false;
        }


        //=============================================================
        //===================================================
        public class Descansando : StateID
        {
            private Elfos elfos;

            // Semaforo o candado para tiempos
            private bool descansar;
            // Una referencia a la corutina
            private Coroutine descansarCoroutine;

            public Descansando(Elfos _elfos)
            {
                elfos = _elfos;
            }

            public override void OnEnter(GameObject objeto)
            {
                descansar = false; // Abrir el candado o apagar el semáforo para permitir la corutina
                Debug.Log("Elfos van a descansar");
            }
            public override void Act(GameObject objeto)
            {
                // Verificar que el candado este abierto o el semaforo apagado
                if (!descansar)
                {
                    descansarCoroutine = fsm.myMono.StartCoroutine(DescansarFunction());
                }
            }
            public override void Reason(GameObject objeto)
            {
                if (elfos.fatiga == 0)
                {
                    // Si por alguna razón sigue en ejecución la corutina, la detenemos
                    fsm.myMono.StopCoroutine(descansarCoroutine);

                    // Si tiene animacion, la ejecutamos
                    SetAnimationTrigger("Patrullar");

                    ChangeState(StateID.Patrullando);
                }
            }
            public override void OnExit(GameObject objeto)
            {
                Debug.Log("Voy a trabajar");
            }

            IEnumerator DescansarFunction()
            {
                // Cerrar el candado o prender el semáforo
                descansar = true;

                // Ejecuto las acciones del estado
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Descansando");
                elfos.fatiga--;

                // Abro el candado o apago el semáforo
                descansar = false;
            }
        }
    }
}
    
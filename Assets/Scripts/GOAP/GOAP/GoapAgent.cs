using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapAgent : MonoBehaviour
{
    public SteeringCombined steering;
    private List<GoapAction> AccionesDisponibles;
    private Queue<GoapAction> AccionesActuales;

    // Para conocer el estado del mundo y actualizarlo
    private IGOAP goapData;

    // Necesita su planeador de acciones
    private GoapPlanner Planeador;

    private FSMGOAP maquinaDeEstados;

    private FSMGOAP.FSMGOAPState idleState;
    private FSMGOAP.FSMGOAPState ActState;
    private FSMGOAP.FSMGOAPState MoveState;

	// Use this for initialization
	void Start ()
    {
        steering = GetComponent<SteeringCombined>();
        Planeador = new GoapPlanner();

        maquinaDeEstados = new FSMGOAP();
        AccionesActuales = new Queue<GoapAction>();
        AccionesDisponibles = new List<GoapAction>();

        // proveedor de datos del mundo
        goapData = GetComponent<IGOAP>();

        //Crear nuestros estados
        CrearEstadoIdle();
        CrearEstadoActuar();
        CrearEstadoMoverse();

        // empezamos pensando
        maquinaDeEstados.pushState(idleState);

        // Cargar las acciones que puede hacer el agente
        GoapAction[] acciones = GetComponents<GoapAction>();
        foreach (GoapAction a in acciones)
            AccionesDisponibles.Add(a);
	}
	
    public void CrearEstadoIdle()
    {
        // Este estado lo usa el agente para calcular un plan
        idleState = (fsm, gameObj) =>
        {
            // Planeación goap
            // Obtener el estado del mundo
            Dictionary<string, object> worldState = goapData.GetWorldState();
            // Obtener la meta del agente
            Dictionary<string, object> goal = goapData.CreateGoalState();

            // Crear un plan
            Queue<GoapAction> plan = Planeador.ElPlan(
                gameObject, AccionesDisponibles, worldState, goal);

            // ¿Logramos tener un plan?
            if (plan != null)
            {
                Debug.Log("Tengo un plan!");
                AccionesActuales = plan;
                goapData.PlanFound(goal, plan);

                maquinaDeEstados.popState(); // Saca el estado idle
                maquinaDeEstados.pushState(ActState); // pasa al estado de actuar
            }
            else
            {
                Debug.Log("No tengo plan :(");
                goapData.PlanFailed(goal);

                maquinaDeEstados.popState();
                maquinaDeEstados.pushState(idleState); // Vuelva a intentar calcular un plan
            }
        };
    }

    private void CrearEstadoActuar()
    {
        ActState = (fsm, gameObj) =>
        {
            // Ejecutar la acción
            if (AccionesActuales.Count <= 0) // No tengo un plan
            {
                // No hay acciones que hacer, puedo ir a Idle
                fsm.popState(); // salir de actuar
                fsm.pushState(idleState);

                goapData.ActionsFinished();

                return;
            }

            // Si si tengo accioens, entonces obtengo la primera
            GoapAction accion = AccionesActuales.Peek();
            if (accion.isDone())
            {
                // si ya se terminó la acción, la quito
                AccionesActuales.Dequeue();
            }
            // Si no ha terminado y quedan acciones, hay que ejecutarla
            if (AccionesActuales.Count > 0)
            {
                accion = AccionesActuales.Peek();

                // Verifico si requiere estar en rango (cerca de su objetivo)
                bool enRango = accion.requiresInRange() ? accion.IsInRange() : true;

                if (enRango) // Ya está donde debe de estar
                {
                    bool exito = accion.Perform(gameObj);

                    // Si la acción no se ejecutó
                    if (!exito)
                    {
                        // Planeo otra vez
                        fsm.popState(); // salgo de actuar
                        fsm.pushState(idleState);

                        goapData.PlanAborted(accion);
                    }
                }
                else
                {
                    // No está en donde debería estar, no está en rango
                    Debug.Log("Estoy lejos del objetivo");
                    fsm.pushState(MoveState);
                }
            } // if
            else
            {
                // No quedan acciones, entonces puedo volver a planear
                fsm.popState();
                fsm.pushState(idleState);

                goapData.ActionsFinished();
            }
        };
    }

    private void CrearEstadoMoverse()
    {
        MoveState = (fsm, gameObj) =>
        {
            GoapAction accion = AccionesActuales.Peek();
            // Mover al agente hacia el objetivo de la acción, si es que tiene
            if (accion.requiresInRange() && accion.Target == null)
            {
                Debug.Log("La acción requiere un target, pero no tiene");
                fsm.popState(); // salir de actuar
                fsm.popState(); // salir de moverse
                fsm.pushState(idleState);
                return;
            }

            // Que se mueva
            if (goapData.MoveAgent(accion))
            {
                // salir de idle o de actuar
                fsm.popState();
            }

            /*  // código de movimiento
              gameObj.transform.position = Vector3.MoveTowards(
                  gameObj.transform.position,
                  accion.Target.transform.position,
                  Time.deltaTime * 5f);*/
            steering.Target = accion.Target.transform;




            if (Vector3.Distance(
                gameObj.transform.position, accion.Target.transform.position) < 1f)
            {
                // llega al objetivo
                accion.SetInRange(true);
                fsm.popState(); //salir de moverse
            }
        };
    }

	// Update is called once per frame
	void Update ()
    {
        maquinaDeEstados.UpdateFSM(gameObject);
	}
}

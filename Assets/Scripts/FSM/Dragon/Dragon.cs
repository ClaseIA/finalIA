using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonStates;

public class Dragon : MonoBehaviour 
{
    public FSM fsm;

    // Variables de dragon
    public int Hambre;
 

    void InitMinerData()
    {
        Hambre = 0;
      
    }

	// Use this for initialization
	void Start () 
    {
        InitMinerData();
        
        // Hay que hacer la fsm del agented
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar Bob
        Deambular deambulando = new Deambular(this);
        Comer comiendo = new Comer(this);
        Dormir durmiendo = new Dormir(this);
        Destruir destruir = new Destruir(this);
    

        // Agregar eventos a los estados
        //sleep.AddEvent(EventList.events.dinnerReady);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Deambular, deambulando);
        fsm.AddState(StateID.Comer, comiendo);
        fsm.AddState(StateID.Dormir, durmiendo);
        fsm.AddState(StateID.Destruir, destruir);


        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.Deambular);

        // Activo la fsm
        fsm.Activate();
	}
	
	void Update () 
    {
		if(fsm != null && fsm.IsActive())
        {
            fsm.UpdateFSM();
        }
	}
}

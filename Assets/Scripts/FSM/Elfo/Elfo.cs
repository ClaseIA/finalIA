using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfoStates;

public class Elfo : MonoBehaviour 
{
    public FSM fsm;

    // Variables de bob
    public int goldInPockets;
 

    void InitMinerData()
    {
        goldInPockets = 0;
      
    }

	// Use this for initialization
	void Start () 
    {
        InitMinerData();
        
        // Hay que hacer la fsm del agented
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar Bob
        Mining mining = new Mining(this);
    

        // Agregar eventos a los estados
        //sleep.AddEvent(EventList.events.dinnerReady);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Mining, mining);
     

        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.Mining);

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

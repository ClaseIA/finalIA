using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrcoStates;

public class Orco : MonoBehaviour 
{
    public FSM fsm;
    public SteeringCombined steering;
    public GameManagerScript manager;

    // Variables de dragon
    public int Hambre;
    public int Cansancio;
 

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
        // Asignar el steering
        steering = GetComponent<SteeringCombined>();
     

        // Crear los estados en que puede estar Bob
        Deambular deambulando = new Deambular(this);
     
    

        // Agregar eventos a los estados
        //sleep.AddEvent(EventList.events.dinnerReady);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Deambular, deambulando);
      


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMGOAP
{
    private Stack<FSMGOAPState> pilaEstados = new Stack<FSMGOAPState>();

    public void pushState(FSMGOAPState estado)
    {
        pilaEstados.Push(estado);
    }

    public void popState()
    {
        pilaEstados.Pop();
    }

    public void UpdateFSM(GameObject gameObject)
    {
        if(pilaEstados.Peek() != null)
        {
            // Ejecuto el estado actual
            pilaEstados.Peek().Invoke(this, gameObject);
        }
    }

    public delegate void FSMGOAPState(FSMGOAP fsm, GameObject gameobject);


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

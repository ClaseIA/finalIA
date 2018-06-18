using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElfosStates;

public class Elfos : MonoBehaviour
{

    public FSM fsm;

    // Variables de bob
    public int fatiga;

    void InitElfosData()
    {
        fatiga = 0;
    }

    // Use this for initialization
    void Start()
    {
        InitElfosData();

        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar Bob
        Patrullando patrullar = new Patrullando(this);
        Descansando descansar = new Descansando(this);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Patrullando, patrullar);
        fsm.AddState(StateID.Descansando, descansar);

        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.Patrullando);

        // Activo la fsm
        fsm.Activate();
    }

    void Update()
    {
        if (fsm != null && fsm.IsActive())
        {
            fsm.UpdateFSM();
        }
    }
}

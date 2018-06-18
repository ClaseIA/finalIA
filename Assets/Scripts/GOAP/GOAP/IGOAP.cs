using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * CUALQUIER AGENTE QUE QUIERA USAR goap TIENE QUE IMPLEMENTAR
 * ESTA INTERFAZ.
 * AYUDARÁ AL PLANEADOR DE ACCIONES PARA DECIDIR QUE 
 * ACCIONES TOMAR.
 * LA USAREMO TAMBIÉN PARA COMUNICARNOS CON EL AGNETE Y 
 * HACERLE SABER SI UNA ACCIÓN FALLA O SE CUMPLE
 * */

public interface IGOAP
{
    // Información del estado del juego o del mundo
    //HashSet<KeyValuePair<string, object>> getworldState();
    Dictionary<string, object> GetWorldState();

    // Proporcionar al planeador una meta para que pueda
    // construir la secuencia de acciones a seguir, el plan
    Dictionary<string, object> CreateGoalState();

    // Si encontramos un plan
    void PlanFound(
        Dictionary<string, object> Goal,
        Queue<GoapAction> Actions
        );

    // Si el plan falla, podríamos tratar de encontrar uno nuevo
    void PlanFailed(Dictionary<string, object> FailedGoal);

    void ActionsFinished();

    void PlanAborted(GoapAction abortedAction);

    bool MoveAgent(GoapAction nextAction);

}
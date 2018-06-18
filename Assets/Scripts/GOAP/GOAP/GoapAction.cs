using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    // Una acción puede tener un costo para realizarse
    // Dependiendo del costo será la secuencia que tome el planeador
    public float cost;

    // Precondiciones
    private Dictionary<string, object> Precondiciones;

    // Efectos
    private Dictionary<string, object> Efectos;

    // Una acción por lo general se ejecuta sobre un objeto
    public GameObject Target;

    public GoapAction()
    {
        Precondiciones = new Dictionary<string, object>();
        Efectos = new Dictionary<string, object>();
    }

    // Limpiar las variables de la acción
    public void Reset()
    {
        inRange = false;
        Target = null;
        reset();
    }
    // Para que cada acción resetee sus variables
    public abstract void reset();
    // Cada acción debe decir cuando ya terminó
    public abstract bool isDone();
    // Cada acción debe checar si sus Precondiciones se cumplen
    public abstract bool checkPreconditions(GameObject go);
    // Cada acción debe ejecutar sus tareas
    public abstract bool Perform(GameObject gameobject);

    public bool inRange;
    //Si la acción necesita estar cerca de un objetivo
    public abstract bool requiresInRange();
    public bool IsInRange()
    {
        return inRange;
    }
    public void SetInRange(bool range)
    {
        inRange = range;
    }

    // Agregar precondiciones
    public void AddPrecondition(string key, object value)
    {
        Precondiciones.Add(key, value);
    }
    // Agregar efectos
    public void AddEffect(string key, object value)
    {
        Efectos.Add(key, value);
    }

    // Revisar las precondiciones de la acción
    public Dictionary<string, object> GetPrecondiciones
    {
        get { return Precondiciones; }
    }
    // Revisar los efectos de la acción
    public Dictionary<string, object> GetEfectos
    {
        get { return Efectos; }
    }
    
}

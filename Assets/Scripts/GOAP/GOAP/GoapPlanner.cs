using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Planear las acciones que pueden ser ejecutadas para cumplir una meta
/// </summary>

public class GoapPlanner
{
    // La secuencia de acciones para cumplir una meta
    public Queue<GoapAction> ElPlan(
        GameObject agente,
        List<GoapAction> AccionesDisponibles,
        Dictionary<string, object> WorldState,
        Dictionary<string, object> Goal
        )
    {
        // Limpiar las acciones
        foreach (GoapAction accion in AccionesDisponibles)
            accion.Reset();

        // De las acciones disponibles vamos a checar cuales se pueden
        // ejecutar a través de sus precondiciones.
        List<GoapAction> AccionesUsables = new List<GoapAction>();

        foreach(GoapAction accion in AccionesDisponibles)
        {
            if (accion.checkPreconditions(agente))
                AccionesUsables.Add(accion);
        }

        // Una vez que sabemos cuales acciones se pueden llevar acabo,
        // construimos el árbol de acciones para encontrar la meta
        List<Node> arbol = new List<Node>();
        // Construir el grafo o arbol
        Node inicial = new Node(null, 0, null, WorldState);
        bool exito = ConstruyeGrafo(inicial, arbol, AccionesUsables, Goal);

        if(!exito)
        {
            // No encontró un plan
            Debug.Log("No encontró una solución :(");
            return null;
        }

        // Si llega aquí, encontré al menos una solución.
        //Tengo que ver que solución es la de menor costo
        Node masBarato = null;
        foreach(Node hoja in arbol)
        {
            if (masBarato == null)
                masBarato = hoja;
            else
                if (hoja.costo < masBarato.costo)
                    masBarato = hoja;
        }

        // Como ya encontré el nodo más barato, hago un backtracking para
        // regresar la lista de acciones a seguir.
        List<GoapAction> resultado = new List<GoapAction>();
        Node temp = masBarato;
        while(temp!=null)
        {
            if(temp.accion != null)
            {
                // Agrego la acción al frente de la lista de acciones
                resultado.Insert(0, temp.accion);
            }
            temp = temp.padre;
        }
        Queue<GoapAction> colaAcciones = new Queue<GoapAction>();
        foreach (GoapAction a in resultado)
            colaAcciones.Enqueue(a);

        // Lo logramos! Ten tu plan!
        return colaAcciones;
    }

    private bool ConstruyeGrafo(
        Node inicial,
        List<Node> arbol,
        List<GoapAction> acciones,
        Dictionary<string, object> goal
        )
    {
        bool encontreSolucion = false;

        // Ver cada accion disponible y ver si puede usarse
        foreach(GoapAction accion in acciones)
        {
            // Si el estado tiene las condiciones para cumplir
            // las precondiciones de esta accion
            if(CheckState(accion.GetPrecondiciones, inicial.estado))
            {
                // Los efectos de la acción se realizan en el nodo
                Dictionary<string, object> estadoActual =
                    ActualizaEstado(inicial.estado, accion.GetEfectos);

                // Con el estado actualizado, creamos su nodo
                Node nodo = new Node(
                    inicial, inicial.costo + accion.cost, accion, estadoActual);

                // Verifico si este nuevo nodo cumple con la meta
                if(CheckState(goal, estadoActual))
                {
                    // Encontró una solución
                    arbol.Add(nodo);
                    encontreSolucion = true;
                }
                else
                {
                    // Esta no es una solución, tiene que seguir construyendo 
                    // el árbol en busca de opciones.
                    // Como estoy en un nuevo nodo(estado del mundo), las 
                    // acciones que puede hacer ya no son las mismas, entonces
                    // le paso un nuevo subconjunto de acciones.
                    List<GoapAction> subConjunto =
                        SubconjuntoAcciones(acciones, accion);

                    bool nuevaSolucion =
                        ConstruyeGrafo(nodo, arbol, subConjunto, goal);
                    if (nuevaSolucion)
                        encontreSolucion = true;
                }// else
            }// if
        }// foreach
        return encontreSolucion;
    }

    private List<GoapAction> SubconjuntoAcciones(
        List<GoapAction> acciones, GoapAction accionActual)
    {
        List<GoapAction> subconjunto = new List<GoapAction>();
        foreach(GoapAction a in acciones)
        {
            if (!a.Equals(accionActual))
                subconjunto.Add(a);
        }
        return subconjunto;
    }

    // Actualizar el estado actual con los efectos que tenga una acción
    private Dictionary<string, object> ActualizaEstado(
        Dictionary<string, object> estadoActual,
        Dictionary<string, object> efectoDeAccion
        )
    {
        // Primero copio la información del estadoActual
        Dictionary<string, object> estadoTem = new Dictionary<string, object>();
        foreach (KeyValuePair<string, object> valor in estadoActual)
            estadoTem.Add(valor.Key, valor.Value);

        // Ahora vy a agregar los efectos al estado si es que no existen
        foreach(KeyValuePair<string, object> efecto in efectoDeAccion)
        {
            bool yaExiste = false;

            // Puede que el dato ya exista pero que su valor haya cambiado
            foreach(KeyValuePair<string, object> dato in estadoTem)
            {
                //if(dato.Equals(efecto))
                if (dato.Key.Equals(efecto.Key))
                {
                    yaExiste = true;
                    break;
                }
            }
            if(yaExiste)
            {
                // Reemplazo el contenido del estado
                estadoTem[efecto.Key] = efecto.Value;
            }
            else
            {
                // si no existe, solo lo agrego
                estadoTem.Add(efecto.Key, efecto.Value);
            }
        } //foreach
        return estadoTem;
    }

    // Verificar que las precondiciones estén en el esatdo del mundo
    // en el nodo actual
    // Basta que una precondición no esté en el estado del mundo
    // para que no se cumpla la condición.
    private bool CheckState(
        Dictionary<string, object> precondiciones,
        Dictionary<string, object> estado
        )
    {
        bool sonIguales = true;

        foreach (KeyValuePair<string, object> precon in precondiciones)
        {
            bool igual = false;
            foreach (KeyValuePair<string, object> valor in estado)
            {
                if (valor.Equals(precon))
                {
                    igual = true;
                    break;
                }
            }
            if (!igual)
                sonIguales = false;
        }
        return sonIguales;
    }

    private class Node
    {
        public Node padre;
        public float costo;
        public GoapAction accion;
        public Dictionary<string, object> estado;

        public Node(Node padre, float costo, GoapAction accion,
            Dictionary<string,object> estado)
        {
            this.padre = padre;
            this.costo = costo;
            this.accion = accion;
            this.estado = estado;
        }
    }
}

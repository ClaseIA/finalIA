using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FSMGOAPState
{
    void Update(FSMGOAP fsm, GameObject gameobject);
}
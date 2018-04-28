using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition {

    public Decision decision;   // decision to evaluate
    public State trueState;
    public State falseState;

}

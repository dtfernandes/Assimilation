using UnityEngine;

public class Transition : MonoBehaviour
{
    public State fromState;
    public State toState;
    public System.Func<bool> condition;

    public Transition(State fromState, State toState, System.Func<bool> condition)
    {
        this.fromState = fromState;
        this.toState = toState;
        this.condition = condition;
    }

    public bool Check()
    {
        return condition();
    }
}

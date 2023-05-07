using System.Collections.Generic;

public class StateMachine 
{
    private State currentState;
    private List<Transition> transitions;

    public void Initialize(State startingState, List<Transition> transitions)
    {
        currentState = startingState;
        currentState.Enter?.Invoke();
        this.transitions = transitions;
    }

    public void Update()
    {
        if (currentState != null)
        {
            foreach (Transition transition in transitions)
            {
                if (transition.fromState == currentState && transition.Check())
                {
                    ChangeState(transition.toState);
                    break;
                }
            }

            currentState.Update?.Invoke();
        }
    }

    private void ChangeState(State newState)
    {
        currentState.Exit?.Invoke();
        currentState = newState;
        currentState.Enter?.Invoke();
    }
}

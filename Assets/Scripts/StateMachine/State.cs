using System;

public class State 
{
    public Action Enter;
    public Action Exit;
    public Action Update;

    public State(Action enter, Action exit, Action update)
    {
        Enter = enter;
        Exit = exit;
        Update = update;
    }
}

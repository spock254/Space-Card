using Godot;
using System;

public partial class EnemyTurnState : State
{
    [Signal]
    public delegate void EnemyTurnEventHandler();
    public override void Enter()
    {
        GD.Print("ENEMY ENTER");
        EmitSignal(SignalName.EnemyTurn);
    }
    public override void HandleInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Keycode == Key.Enter && eventKey.IsPressed() && eventKey.Echo == false)
            {
                base.fsm.Transition(Global.State.Player_Turn);
            }
        }
    }

    public override void Exit()
    {
        GD.Print("ENEMY EXIT");
    }
}

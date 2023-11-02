using Godot;
using System;

public partial class PlayerTurnState : State
{
    [Signal]
    public delegate void PlayerTurnEventHandler();



    public override void Enter()
	{
		GD.Print("PLAYER ENTER");
        EmitSignal(SignalName.PlayerTurn);
        
	}

	public override void HandleInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) 
		{
			if (eventKey.Keycode == Key.Enter && eventKey.IsPressed() && eventKey.Echo == false) 
			{
                base.fsm.Transition(Global.State.Enemy_Turn);
            }
		}
	}

	public override void Exit()
	{
        GD.Print("PLAYER EXIT");
    }
}

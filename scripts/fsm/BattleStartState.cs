using Godot;
using System;

public partial class BattleStartState : State
{
	public override void Enter()
	{
		GD.Print("Battle Start");
		base.fsm.Transition(Global.State.Player_Turn);
	}
}

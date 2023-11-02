using Godot;
using System;

public partial class State : Node
{
	public StateMashine fsm;

	public virtual void Enter() { }
	public virtual void Exit() { }

	public virtual void StateReady() { }
	public virtual void Update(float delta) { }
	public virtual void PhisicsUpdate(float delta) { }
	public virtual void HandleInput(InputEvent @event) { }

}

using Godot;
using Godot.Collections;
using System;
/*
enum StateEnum
{
	Player_Turn,
	Enemy_Turn
}
*/
public partial class StateMashine : Node
{
	[Export] public NodePath initialState;

	Dictionary<string, State> states;
	State currentState = null;

	public override void _Ready()
	{
		states = new Dictionary<string, State>();

		foreach (Node node in GetChildren())
		{
			if (node is State s) 
			{
				states[node.Name] = s;
				s.fsm = this;
				
				s.StateReady();
				//s.Exit(); // reset all states
			}
		}

		currentState = GetNode<State>(initialState);
		currentState.Enter();
	}

	public override void _Process(double delta)
	{
		currentState.Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		currentState.PhisicsUpdate((float)delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		currentState.HandleInput(@event);
	}

	public void Transition(string key) 
	{
		if (!states.ContainsKey(key) || currentState == states[key]) 
		{ 
			return;
		}

		currentState.Exit();
		currentState = states[key];
		currentState.Enter();
	}
}

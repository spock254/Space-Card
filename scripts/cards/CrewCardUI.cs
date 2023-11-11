using Godot;
using System;

public partial class CrewCardUI : Node
{
	CrewCardBase crewCard = null;
	Label energyCost = null;
	public override void _Ready()
	{
		crewCard = GetNode<CrewCardBase>("../Crew Card");
		energyCost = GetNode<Label>("../Label");

		InitCard();
	}

	void InitCard() 
	{ 
		energyCost.Text = crewCard.GetEnergyCost().ToString();
	}
}


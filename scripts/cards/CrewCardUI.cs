using Godot;
using System;

public partial class CrewCardUI : Node
{
	CrewCardBase crewCard = null;
	Label energyCost = null;
	TextureRect icon = null;
	public override void _Ready()
	{
		crewCard = GetNode<CrewCardBase>("../Crew Card");
		energyCost = GetNode<Label>("../Label");
		icon = GetNode<TextureRect>("../Icon");
		InitCard();
	}

	public void InitCard() 
	{ 
		energyCost.Text = crewCard.GetEnergyCost().ToString();
		icon.Texture = crewCard.GetIcon();
	}

	void OnMouseEntered() 
	{
		//GD.Print("ENTER");
		Global.selectedCrewCard.SetSelectedCrewCard(GetParent<TextureRect>());
	}

    void OnMouseExited()
    {
		Global.selectedCrewCard.SetSelectedCrewCard(null);

    }
}


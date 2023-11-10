using Godot;
using System;

public partial class CardUI : VBoxContainer
{
	[Export] Label cardName;

	CardBase card;

	TextureRect hidedCard = null;
	TextureRect openedCard = null;
	CollisionShape2D collision = null;

	public override void _Ready()
	{
		card = GetNode<CardBase>("Card");

		hidedCard = GetNode<TextureRect>("TextureRect2");
        openedCard = GetNode<TextureRect>("TextureRect");

        InitCard();
	}

	public void InitCard() 
	{
        cardName.Text = card.GetName();
    }

	public bool UpdateCard(CrewCardBase crewCard) 
	{
		if (crewCard == null)
		{
			GD.Print("CREW CARD == NULL");
			return false;
		}
		else 
		{
			GD.Print("UPDATE");

			card.UpdateCard(crewCard);
		}

		return true;
	}

	void OnMouseEntered() 
	{
        Global.selectedSystemCard.SetSystemCardObj(this);
		ShowCard();
    }

	void OnMouseExited() 
	{
		Global.selectedSystemCard.SetSystemCardObj(null);
        HideCard();   
    }

	void HideCard() 
	{
        openedCard.Visible = false;
        hidedCard.Visible = true;
    }

	void ShowCard() 
	{
        hidedCard.Visible = false;
        openedCard.Visible = true;
    }
}

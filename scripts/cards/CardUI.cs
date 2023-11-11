using Godot;
using System;
using System.Collections.Generic;

public partial class CardUI : VBoxContainer
{
	[Export] Texture2D ocupatedCrewCardSlot = null;
	[Export] Texture2D freeCrewCardSlot = null;

	Label cardName = null;
	Label energyCost = null;

	List<TextureRect> crewCardSlots = new List<TextureRect>();

	CardBase card;

	TextureRect hidedCard = null;
	TextureRect openedCard = null;
	CollisionShape2D collision = null;

	public override void _Ready()
	{
		//ui elements
		cardName = GetNode<Label>("VBoxContainer/TextureRect/Label");
        energyCost = GetNode<Label>("VBoxContainer/HBoxContainer/Energy Frame/Label");

		for (int i = 0; i < 4; i++)
		{
			crewCardSlots.Add(GetNode<TextureRect>("VBoxContainer/HBoxContainer/Crew Card " + (i + 1).ToString())); // 4 cre card slots
		}

		card = GetNode<CardBase>("Card");

		hidedCard = GetNode<TextureRect>("TextureRect2");
        openedCard = GetNode<TextureRect>("TextureRect");

        InitCard();
	}

	public void InitCard() 
	{
        cardName.Text = card.GetName();
        energyCost.Text = card.GetEnergyCost().ToString();
		UpdateCrewCardSlots();
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

			//int updatedEnergyCost = card.GetEnergyCost() + crewCard.GetEnergyCost();
			
			//energyCost.Text = updatedEnergyCost.ToString();


			card.UpdateCard(crewCard);
			InitCard();
		}

		return true;
	}

	void UpdateCrewCardSlots() 
	{
		for (int i = card.GetMaxCrewCard(); i < crewCardSlots.Count; i++)
		{
			crewCardSlots[i].Modulate = new Color(crewCardSlots[i].Modulate.R, 
											      crewCardSlots[i].Modulate.G, 
												  crewCardSlots[i].Modulate.B, 0);
		}

		for (int i = 0; i < card.GetCrewCards().Count; i++)
		{
			crewCardSlots[i].Texture = ocupatedCrewCardSlot;
		}

		for (int i = card.GetCrewCards().Count; i < card.GetMaxCrewCard(); i++) 
		{
			crewCardSlots[i].Texture = freeCrewCardSlot;
		}
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

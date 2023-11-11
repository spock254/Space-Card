using Godot;
using System;
using System.Collections.Generic;
using static Global.Enums;

public partial class CardUI : VBoxContainer
{
	[Export] Texture2D ocupatedCrewCardSlot = null;
	[Export] Texture2D freeCrewCardSlot = null;

	[Export] Texture2D attackTop = null;
	[Export] Texture2D defenceTop = null;
	[Export] Texture2D systemTop = null;
	Dictionary<CardType, Texture2D> topTypes = new Dictionary<CardType, Texture2D>();
	TextureRect top = null;

	Label cardName = null;
	Label energyCost = null;

	List<TextureRect> crewCardSlots = new List<TextureRect>();

	CardBase card;

	TextureRect hidedCard = null;
	TextureRect openedCard = null;
	CollisionShape2D collision = null;

	PackedScene cardPref = null;
	Node crewCardParent = null;

	public override void _Ready()
	{
		//ui elements
		cardName = GetNode<Label>("VBoxContainer/TextureRect/Label");
        energyCost = GetNode<Label>("VBoxContainer/HBoxContainer/Energy Frame/Label");

		for (int i = 0; i < 4; i++)
		{
			crewCardSlots.Add(GetNode<TextureRect>("VBoxContainer/HBoxContainer/Crew Card " + (i + 1).ToString())); // 4 crew card slots
		}

		card = GetNode<CardBase>("Card");

		hidedCard = GetNode<TextureRect>("TextureRect2");
        openedCard = GetNode<TextureRect>("TextureRect");

		cardPref = (PackedScene) ResourceLoader.Load("res://pref/crew_card_base.tscn");
		crewCardParent = GetNode<Node>("../../Crew Panel");

		// ui top board
		topTypes.Add(CardType.ATTACK, attackTop);
		topTypes.Add(CardType.DEFFENCE, defenceTop);
		topTypes.Add(CardType.SYSTEM, systemTop);
		top = GetNode<TextureRect>("VBoxContainer/TextureRect");

        InitCard();
	}

	// SIGNAL
	void OnGuiInput(InputEvent @event) 
	{
		if (@event is InputEventMouseButton input) 
		{
			if (input.ButtonIndex == MouseButton.Right && input.IsPressed() == true) 
			{
				CrewCardBase releseCard = card.ReleseCrewCard();

				if (releseCard == null) 
				{
					return;
				}

				InstantiateCrewCard(releseCard);
                InitCard();
            }
		}
	}

	void InstantiateCrewCard(CrewCardBase crewCard) 
	{
		if (crewCard == null) 
		{
			return;
		}

		TextureRect instCrewCard = (TextureRect) cardPref.Instantiate();
		crewCardParent.AddChild(instCrewCard);
		CrewCardBase originCrewCardBase = instCrewCard.GetNode<CrewCardBase>("Crew Card");
		originCrewCardBase.Copy(crewCard);
		instCrewCard.GetNode<CrewCardUI>("Crew Card UI").InitCard();

    }

    public void InitCard() 
	{
        cardName.Text = card.GetName();
        energyCost.Text = card.GetEnergyCost().ToString();
		top.Texture = topTypes[card.GetCardType()];

		hidedCard.GetNode<TextureRect>("Icon").Texture = card.GetIcon();
		openedCard.GetNode<TextureRect>("Icon").Texture = card.GetIcon();

		UpdateCrewCardSlots();
    }

	public bool UpdateCard(CrewCardBase crewCard) 
	{
		if (crewCard == null)
		{
			GD.PrintErr("CREW CARD == NULL");
			return false;
		}
		else if (crewCard.GetCardType() != card.GetCardType() || card.IsCrewCardsFull() == true) 
		{
			GD.PrintErr("CREW TYPE NOT COMPATABLE OR CARD IS FULL");
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

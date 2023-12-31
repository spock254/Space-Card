using Godot;
using System;
using System.Collections.Generic;
using static Global.Enums;

public partial class CardBase : Node
{
	[Export] CardType cardType;
    [Export] string name;
    [Export] string description;
    [Export] int energyCost;
    [Export] Texture2D icon;
    [Export] int cooldown = 0;
	[Export] int maxCrewCard = 3;

	List<CrewCardBase> crewCards = new List<CrewCardBase>();

	bool isActive = true;
	int currentCooldown = 0;
	int originEnergyCost = 0;
	int energyCostCooldown = 0;
	string originDescription = string.Empty;

	public override void _Ready()
	{
		originEnergyCost = energyCost;
		originDescription = description;
	}

	public virtual void Turn() { }

	public bool UpdateCard(CrewCardBase crewCard) 
	{
        AddCrewCard(crewCard);

		SetEnergyCost(energyCost + crewCard.GetEnergyCost());

		return true;
	}

	public CrewCardBase ReleseCrewCard() 
	{
		if (crewCards.Count == 0) 
		{
			GD.Print("NO CREW CARD");
			return null;
		}

		CrewCardBase crewCardToRelese = crewCards[crewCards.Count - 1];
        SetEnergyCost(energyCost - crewCardToRelese.GetEnergyCost());
        crewCards.RemoveAt(crewCards.Count - 1);

		return crewCardToRelese;
	}

	public bool AddCrewCard(CrewCardBase crewCard) 
	{
		if (crewCards.Count >= maxCrewCard) 
		{
			return false;
		}

		if (this.cardType != crewCard.GetCardType()) 
		{
			return false;
		}

        crewCards.Add(crewCard);

		//update energy
		//update effect
		//update descr

		return true;
	}


	void OnPlayerTurn() 
	{
		GD.Print("Cooldown");
		DecreaseCooldown();
		DecreaseEnergyCostCooldown();

    }

	protected void SetCooldown() 
	{
		currentCooldown = cooldown;

		isActive = (currentCooldown == 0) ? true : false;
	}

	protected void DecreaseCooldown() 
	{
		if (currentCooldown == 0) 
		{
			isActive = true;
			return;
		}

		isActive = false;
		currentCooldown--;
	}

	protected void DecreaseEnergyCostCooldown() 
	{
		if (energyCostCooldown == 0) 
		{
			energyCost = originEnergyCost;
			return;
		}

		energyCostCooldown--;
	}


    public void UpdateEnergyCost(int energyCost, int energyCostCooldown) 
	{
		SetEnergyCost(energyCost);
		SetEnergyCostCooldown(energyCostCooldown);
	}
	public bool IsCrewCardsFull() 
	{
		return crewCards.Count == maxCrewCard;
	}

	public CardType GetCardType() => cardType;
    public string GetName() => name;
    public string GetDescription() => description;
    public int GetEnergyCost() => energyCost;
    public Texture2D GetIcon() => icon;
    public int GetCooldown() => cooldown;
	public bool IsActive() => isActive;
	public void SetEnergyCost(int energyCost) => this.energyCost = energyCost;
	public void SetEnergyCostCooldown(int energyCostCooldown) => this.energyCostCooldown = energyCostCooldown;
	public int GetMaxCrewCard() => maxCrewCard;
	public List<CrewCardBase> GetCrewCards() => crewCards;
}

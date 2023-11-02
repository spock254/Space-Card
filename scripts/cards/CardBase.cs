using Godot;
using System;
using System.Collections.Generic;

public partial class CardBase : Node
{
	[Export]
	public bool isSystemCard;
    [Export]
    public string name;
    [Export]
    public string description;
    [Export]
    public int energyCost;
    [Export]
    public Texture2D icon;
    [Export]
    public int cooldown = 0;
	

	bool isActive = true;
	int currentCooldown = 0;
	int originEnergyCost = 0;
	int energyCostCooldown = 0;

	public override void _Ready()
	{
		originEnergyCost = energyCost;
	}

	public virtual void Turn() { }

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

	public bool IsActive() => isActive;
	public void SetEnergyCost(int energyCost) => this.energyCost = energyCost;
	public void SetEnergyCostCooldown(int energyCostCooldown) => this.energyCostCooldown = energyCostCooldown;
}

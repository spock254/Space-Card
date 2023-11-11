using Godot;
using System;
using static Global.Enums;

public partial class CrewCardBase : Node
{
	[Export] CardType cardType;
    [Export] string name;
    [Export] string description;
    [Export] int energyCost;
    [Export] Texture2D icon;
    [Export] int cooldown = 0;

    public CardType GetCardType() => cardType;
    public string GetName() => name;
    public string GetDescription() => description;
    public int GetEnergyCost() => energyCost;
    public Texture2D GetIcon() => icon;
    public int GetCooldown() => cooldown;

    public void Copy(CrewCardBase other) 
    {
        this.cardType = other.cardType;
        this.name = other.name;
        this.description = other.description;
        this.energyCost = other.energyCost;
        this.icon = other.icon;
        this.cooldown = other.cooldown;

    }
}

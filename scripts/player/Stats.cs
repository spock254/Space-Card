using Godot;
using System;

public partial class Stats : Node
{
	[Export] bool isPlayerStats = true;
	[Export] public int maxHp = 0;
	[Export] public int currentHp = 0;
	[Export] public int maxShield = 0;
	[Export] public int currentShield = 0;
	[Export] public int maxEnergy = 0;
	[Export] public int currentEnergy = 0;

	public override void _Ready()
	{
		if (isPlayerStats == true)
		{
			Global.playerStats = this;
		}
		else 
		{
			Global.enemyStats = this;
		}
	}
}

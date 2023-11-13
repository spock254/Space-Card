using Godot;
using System;
using System.Collections.Generic;

public partial class StatBarUI : Node
{
	public enum UiField 
	{ 
		HP,
		SHIELD,
		ENERGY
	}

	[Export] UiField uiField;
	[Export] bool isPlayerUI = true;
	[Export] Texture2D hpSlotFill = null;
	[Export] Texture2D hpSlotEmpty = null;

	List<TextureRect> slots = null;
	Stats stats = null;

	int max = 0;
	int current = 0;
	public override void _Ready()
	{
		if (isPlayerUI == true)
		{
			stats = Global.playerStats;
		}
		else 
		{
            stats = Global.enemyStats;
		}


        slots = new List<TextureRect>();

		for (int i = 0; i < this.GetChildCount(); i++)
		{
			TextureRect slot = this.GetChild<TextureRect>(i);
			slot.Visible = false;
            slots.Add(slot);
		}

		SetUiField();

		InitHpSlots();
    }

	void SetUiField() 
	{
		if (uiField == UiField.HP)
		{
			max = stats.maxHp;
			current = stats.currentHp;
		}
		else if (uiField == UiField.SHIELD)
		{
			max = stats.maxShield;
			current = stats.currentShield;
		}
		else 
		{
			max = stats.maxEnergy;
			current = stats.currentEnergy;
			slots.Reverse();
		}
	}

	void InitHpSlots() 
	{ 

		for (int i = 0; i < max; i++)
		{
            slots[i].Texture = hpSlotEmpty;
            slots[i].Visible = true;
		}

        for (int i = 0; i < current; i++)
        {
            slots[i].Texture = hpSlotFill;
        }
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

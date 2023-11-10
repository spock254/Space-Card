using Godot;
using System;

public partial class SelectedSystemCard : Node
{
	VBoxContainer systemCard = null;
	public override void _Ready()
	{
		if (Global.selectedSystemCard == null) 
		{
			Global.selectedSystemCard = this;
		}
	}

	public void SetSystemCardObj(VBoxContainer sysCard) => this.systemCard = sysCard;
	public VBoxContainer GetSystemCardObj() => this.systemCard;
	public CardBase GetSystemCard() => GetSystemCardObj().GetNode<CardBase>("Card");
	public CardUI GetSystemCardUI() => GetSystemCardObj() as CardUI;
}

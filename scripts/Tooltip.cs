using Godot;
using Godot.Collections;
using System;
using System.Runtime.Intrinsics.Arm;
using static Global.Enums;

public partial class Tooltip : TextureRect
{
	[Export] Vector2 offset = Vector2.Zero;

	TextureRect header = null;

	[Export] Texture2D headerAttack = null;
	[Export] Texture2D headerDefence = null;
	[Export] Texture2D headerSystem = null;
	[Export] Texture2D headerNeutral = null;

	Dictionary<CardType, Texture2D> cardTypeHeader = null;

	TextureRect middleTex = null;
	TextureRect bottomTex = null;

	Label cardName = null;
	Label cardDescription = null;

	float hight = 0f;
	//TextureRect tooltip = null;
	public override void _Ready()
	{
		middleTex = GetNode<TextureRect>("Tooltip Middle");
        bottomTex = GetNode<TextureRect>("Tooltip Middle/Tooltip Bottom");
		cardName = GetNode<Label>("Name");
		cardDescription = GetNode<Label>("Tooltip Middle/Description");
		header = this;

        hight = this.Size.Y + middleTex.Size.Y + bottomTex.Size.Y;
		Visible = false;

		cardTypeHeader = new Dictionary<CardType, Texture2D>();

		cardTypeHeader.Add(CardType.ATTACK, headerAttack);
		cardTypeHeader.Add(CardType.DEFFENCE, headerDefence);
		cardTypeHeader.Add(CardType.SYSTEM, headerSystem);
		cardTypeHeader.Add(CardType.NEUTRAL, headerNeutral);
	}

	public override void _Process(double delta)
	{
		// move tooltip
		Vector2 mousePos = GetViewport().GetMousePosition();
		this.Position = mousePos - new Vector2(offset.X, offset.Y + hight);
		
		if (Global.selectedSystemCard.GetSystemCardObj() != null)
		{
			Visible = true;
			
			CardBase cardBase = Global.selectedSystemCard.GetSystemCard();
			
			cardName.Text = cardBase.GetName();
			cardDescription.Text = cardBase.GetDescription();

			header.Texture = cardTypeHeader[cardBase.GetCardType()];

        }
		else if (Global.selectedCrewCard.GetSelectedCrewCard() != null) 
		{
			Visible = true;

			CrewCardBase crewCardBase = Global.selectedCrewCard.GetCrewCard();

			cardName.Text = crewCardBase.GetName();
            cardDescription.Text = crewCardBase.GetDescription();

			header.Texture = cardTypeHeader[crewCardBase.GetCardType()];
        }
		else
		{
			Visible = false;
		}
	}

	
}

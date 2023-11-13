using Godot;
using System;

public partial class Tooltip : TextureRect
{
	[Export] Vector2 offset = Vector2.Zero;

	TextureRect middleTex = null;
	TextureRect bottomTex = null;

	float hight = 0f;
	//TextureRect tooltip = null;
	public override void _Ready()
	{
		middleTex = GetNode<TextureRect>("Tooltip Middle");
        bottomTex = GetNode<TextureRect>("Tooltip Middle/Tooltip Bottom");
        hight = this.Size.Y + middleTex.Size.Y + bottomTex.Size.Y;
		Visible = false;
	}

	public override void _Process(double delta)
	{
		// move tooltip
		Vector2 mousePos = GetViewport().GetMousePosition();
		this.Position = mousePos - new Vector2(offset.X, offset.Y + hight);
		
		if (Global.selectedSystemCard.GetSystemCardObj() != null)
		{
			Visible = true;
		}
		else if (Global.selectedCrewCard.GetSelectedCrewCard() != null) 
		{
			Visible = true;
		}
		else
		{
			Visible = false;
		}
	}
}

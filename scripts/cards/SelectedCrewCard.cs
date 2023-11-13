using Godot;
using System;

public partial class SelectedCrewCard : Node
{
    TextureRect crewCard = null;
    public override void _Ready()
    {
        if (Global.selectedCrewCard == null)
        {
            Global.selectedCrewCard = this;
        }
    }

    public void SetSelectedCrewCard(TextureRect crewCard) => this.crewCard = crewCard;
    public TextureRect GetSelectedCrewCard() => this.crewCard;
}

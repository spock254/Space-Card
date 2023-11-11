using Godot;
using System;
using System.Threading.Tasks;

public partial class DragAndDropCrewCard : TextureRect
{
    Vector2 originPosition = Vector2.Zero;
    bool selected = false;
    BoxContainer parentContainer;
    Control rootControl = null;
    int childOrder = 0;
    CrewCardBase crewCard = null;

    public override void _Ready()
    {
        parentContainer = GetParent<BoxContainer>();
        rootControl = parentContainer.GetParent<Control>();
        originPosition = Position;
        crewCard = GetNode<CrewCardBase>("Crew Card");
    }

    public override void _Process(double delta)
    {
        if (selected == true) 
        {
            Position = GetViewport().GetMousePosition() - (Size / 2);
            
            if (Input.IsMouseButtonPressed(MouseButton.Left) == false) 
            {
                
                VBoxContainer systemCard = Global.selectedSystemCard.GetSystemCardObj();
                
                if (systemCard == null)
                {
                    ReturnToCrewCardPanel();
                }
                else 
                {
                    bool updateResult = Global.selectedSystemCard.GetSystemCardUI().UpdateCard(crewCard);

                    if (updateResult == false)
                    {
                        ReturnToCrewCardPanel();
                        return;
                    }
                    
                    Free();
                    
                }

            }
        }
    }

    void ReturnToCrewCardPanel() 
    {
        selected = false;
        rootControl.RemoveChild(this);
        parentContainer.AddChild(this);
        parentContainer.MoveChild(this, childOrder);
        this.MouseFilter = MouseFilterEnum.Pass;
    }

    void OnGuiInput(InputEvent @event) 
	{
        if (@event is InputEventMouseButton eventMouseButton) 
        {
            if (eventMouseButton.IsPressed())
            {
                //originPosition = Position;

                selected = true;
                childOrder = GetIndex();
                parentContainer.RemoveChild(this);
                rootControl.AddChild(this);
                this.MouseFilter = MouseFilterEnum.Ignore;
            }
            
        }
    }
}

using Godot;
using System;

public class Camera : Godot.Camera
{
    
    [Export] bool follow = false;

    Spaceman spaceman;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        spaceman = GetParent().GetChild<Spaceman>(0); 
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (spaceman != null && follow) {
            GlobalTransform = GlobalTransform
                .InterpolateWith(spaceman.GlobalTransform.Translated(new Vector3(0, 10, -30)).LookingAt(spaceman.GlobalTransform.origin, Vector3.Up), delta);
        }
    }
}

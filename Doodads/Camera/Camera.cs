using Godot;
using System;

public class Camera : Godot.Camera
{
    
    [Export] bool follow = false;
    [Export] float horizontalDistance = 6.0f;
    [Export] float verticalDistance = 5.0f;
    [Export] float followSpeed = 0.005f;

    Spaceman spaceman;
    Spatial focusTarget;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        spaceman = GetParent().GetChild<Spaceman>(0); 
        GlobalTransform = GlobalTransform.Translated(GlobalTransform.origin - spaceman.GlobalTransform.origin + new Vector3(0, verticalDistance, horizontalDistance));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (spaceman != null && follow) {
            Translation = Translation.LinearInterpolate(spaceman.GlobalTransform.origin + new Vector3(0, verticalDistance, horizontalDistance), followSpeed);
            Transform = Transform.InterpolateWith(Transform.LookingAt(spaceman.GlobalTransform.origin, Vector3.Up), followSpeed);
        }
    }

    public void SetFocusTarget(Spatial spatial) {

    }
}

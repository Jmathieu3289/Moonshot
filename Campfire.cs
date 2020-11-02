using Godot;
using System;

public class Campfire : OmniLight
{
    [Export] float maxEnergy = 2;
    [Export] float minEnergy = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        LightEnergy = Mathf.Clamp(LightEnergy + (float)GD.RandRange(-0.1, 0.1), minEnergy, maxEnergy);
    }
}

using Godot;
using System;

public class Firefly : KinematicBody
{

    private Vector3 _home = Vector3.Zero;
    private Vector3 _velocity = Vector3.Zero;
    private float _speed = 0.006f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Randomize();
        _home = GlobalTransform.origin;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector3 distanceToHome = (_home - GlobalTransform.origin);
        _velocity += new Vector3(Speed(), Speed(), Speed()) + (distanceToHome/50);
        MoveAndCollide(_velocity * delta);
    }

    private float Speed()
    {
        return (float)GD.RandRange(-_speed, _speed);
    }
}

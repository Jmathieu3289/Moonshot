using Godot;
using System;

public class FishingPole : Spatial
{
    float _tension = 0;

    AnimationPlayer _poleAnimation;
    AnimationPlayer _handleAnimation;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _poleAnimation = GetNode<AnimationPlayer>("PoleAnimation");
        _handleAnimation = GetNode<AnimationPlayer>("HandleAnimation");
        _poleAnimation.AssignedAnimation = ("Bending");
        _poleAnimation.PlaybackSpeed = 0;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(float delta)
    // {
        
    // }

    public void SetTension(float tension) 
    {
        _poleAnimation.Seek(Mathf.Clamp(_tension, 0, 1));
    }

    public void StartReeling() 
    {
        _handleAnimation.Play("Reel");
    }

    public void StopReeling() 
    {
        _handleAnimation.Stop();
    }
}

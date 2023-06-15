using Godot;
using System;

public class SpaceCamera : Spatial
{
    
    Godot.Camera camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera = GetNode<Godot.Camera>("Camera");
    }

    public void StartAim()
    {
        camera.Current = true;
    }

    public void EndAim()
    {
        camera.Current = false;
    }

    public void ZoomIn()
    {
        GD.Print(camera.Fov);
        camera.Fov = Mathf.Clamp(camera.Fov-1, 35, 80);
    }

    public void ZoomOut()
    {
        camera.Fov = Mathf.Clamp(camera.Fov+1, 35, 80);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

using Godot;
using System;

public class World : Spatial
{

    [Export] bool debug = true;

    AnimationPlayer animationPlayer;

    AudioStreamPlayer musicPlayer;

    Spaceman spaceman;
    Camera camera;
    Control startMenu;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        musicPlayer = GetNode<AudioStreamPlayer>("Sound/Music");
        spaceman = GetNode<Spaceman>("Spaceman");
        startMenu = GetNode<Control>("StartMenu");
        camera = GetNode<Camera>("Camera");

        if (debug) {
            animationPlayer.Play("Intro Debug");
        } else {
            animationPlayer.Play("Intro");
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Start_pressed()
    {
        GD.Print("StartPressed");
        spaceman.GiveControl();
        startMenu.Hide();
        animationPlayer.Play("FadeOut");
    }
}

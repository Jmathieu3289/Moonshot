using Godot;
using System;

public class StartMenu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Input.GetConnectedJoypads().Count > 0)
        {
            GetNode<Button>("BasicContainer/VBoxContainer/HBoxContainer/Start").GrabFocus();
        }
    }

    public void _on_Credits_pressed()
    {
        if (!Visible)
            return;
        GetNode<Container>("BasicContainer").Hide();
        GetNode<Container>("CreditsContainer").Show();
        GetNode<Button>("CreditsContainer/VBoxContainer/Back").GrabFocus();
    }

    public void _on_Back_pressed()
    {
        if (!Visible)
            return;
        GetNode<Container>("BasicContainer").Show();
        GetNode<Container>("CreditsContainer").Hide();
        GetNode<Button>("BasicContainer/VBoxContainer/HBoxContainer/Start").GrabFocus();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (!Visible)
            return;
        if (Input.IsActionJustPressed("ui_up") ||
            Input.IsActionJustPressed("ui_down") ||
            Input.IsActionJustPressed("ui_accept"))
            {
                GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
            }
    }
}

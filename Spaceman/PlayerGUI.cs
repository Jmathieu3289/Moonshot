using Godot;
using System;

public class PlayerGUI : Control
{
    private Label labelMessage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        labelMessage = GetNode<Label>("ContextPress/Control/HBoxContainer/Message");
    }

    public void ShowMessage(String message)
    {
        labelMessage.Text = message;
    }

}

using Godot;
using System;

public class Spaceman : KinematicBody
{

    public enum State {
        Uncontrollable,
        Idle,
        Sitting,
        Walking
    }

    public enum HeldItem {
        None,
        Flashlight,
        Camera
    }

    private State _state = State.Uncontrollable;
    private HeldItem _heldItem = HeldItem.None;

    private Spatial _flashLight;
    private SpaceCamera _camera;

    AnimationPlayer animationPlayer;
    PlayerGUI playerGUI;

    Vector3 velocity;

    bool gettingOutFlashlight = false;
    bool gettingOutCamera = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Sitting");
        playerGUI = GetNode<PlayerGUI>("PlayerGUI");
        playerGUI.Hide();

        _flashLight = GetNode<Spatial>("Armature/Skeleton/BoneAttachment/Flashlight");
        _camera = GetNode<SpaceCamera>("Armature/Skeleton/BoneAttachment/Camera");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (_state == State.Uncontrollable) {
            return;
        }
        velocity = Vector3.Zero;
        velocity.y = IsOnFloor() ? 0 : -1;
        if (Input.IsActionPressed("move_left")) {
            velocity.x -= 1;
        }
        if (Input.IsActionPressed("move_right")) {
            velocity.x += 1;
        }
        velocity.x += Input.GetJoyAxis(0, 0);
        if (Input.IsActionPressed("move_up")) {
            velocity.z -= 1;
        }
        if (Input.IsActionPressed("move_down")) {
            velocity.z += 1;
        }
        velocity.z += Input.GetJoyAxis(0, 1);
        if (velocity.x != 0 || velocity.z != 0) {
            setState(State.Walking);

            float newAngle = Mathf.Atan2(velocity.x, velocity.z);
            //Rotation = Rotation.LinearInterpolate(new Vector3(Rotation.x, newAngle, Rotation.z), 0.1f);
            animationPlayer.PlaybackSpeed = 2;
            Rotation = new Vector3(Rotation.x, newAngle, Rotation.z);

        } else {
            setState(State.Idle);
        }
        if (Input.IsActionJustPressed("flashlight")) {
            ToggleFlashlight();
        }
        if (Input.IsActionJustPressed("camera")) {
            ToggleCamera();
        }
        if (Input.IsActionJustPressed("aim"))
        {
            _camera.StartAim();
            playerGUI.ShowMessage("Take Picture");
        }
        if (Input.IsActionJustReleased("aim"))
        {
            _camera.EndAim();
            playerGUI.ShowMessage("");
        }
        if (Input.IsActionPressed("zoom_in"))
        {
            _camera.ZoomIn();
        }
        if (Input.IsActionPressed("zoom_out"))
        {
            _camera.ZoomOut();
        }
    }

    private void ToggleFlashlight()
    {
        if (_heldItem == HeldItem.None) {
            _heldItem = HeldItem.Flashlight;
            _flashLight.Visible = true;
            setState(State.Uncontrollable);
            animationPlayer.Play("Retrieve Item");
        } else if (_heldItem != HeldItem.Flashlight) {
            gettingOutFlashlight = true;
            _heldItem = HeldItem.None;
            _camera.Visible = false;
            setState(State.Uncontrollable);
            animationPlayer.PlayBackwards("Retrieve Item");
        } else {
            _heldItem = HeldItem.None;
            _flashLight.Visible = false;
            setState(State.Uncontrollable);
            animationPlayer.PlayBackwards("Retrieve Item");
        }
    }

    private void ToggleCamera()
    {
        if (_heldItem == HeldItem.None) {
            _heldItem = HeldItem.Camera;
            _camera.Visible = true;
            setState(State.Uncontrollable);
            animationPlayer.Play("Retrieve Item");
        } else if (_heldItem != HeldItem.Camera) {
            gettingOutCamera = true;
            _heldItem = HeldItem.None;
            _flashLight.Visible = false;
            setState(State.Uncontrollable);
            animationPlayer.PlayBackwards("Retrieve Item");
        } else {
            _heldItem = HeldItem.None;
            _camera.Visible = false;
            setState(State.Uncontrollable);
            animationPlayer.PlayBackwards("Retrieve Item");
        }
    }

    private void _on_AnimationPlayer_animation_finished(String animationName) 
    {
        switch(animationName) 
        {
            case "Retrieve Item":
                setState(State.Idle);
                if (gettingOutCamera)
                {
                    gettingOutCamera = false;
                    ToggleCamera();
                }
                if (gettingOutFlashlight)
                {
                    gettingOutFlashlight = false;
                    ToggleFlashlight();
                }
                break;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_state != State.Uncontrollable) {
            MoveAndSlideWithSnap(velocity, Vector3.Zero, Vector3.Up, true);
        }
    }

    public void setState(State state) 
    {
        if (this._state != state) {
            this._state = state;

            switch(state) {
                case State.Idle:
                    animationPlayer.Play(_heldItem == HeldItem.None ? "Idle" : _heldItem == HeldItem.Flashlight ? "Idle Holding" : "Idle Camera");     
                    playerGUI.Show();          
                    break;
                case State.Walking:
                    animationPlayer.Play(_heldItem == HeldItem.None ? "Walking": "Walking Holding");
                    break;
                case State.Uncontrollable:
                    velocity = Vector3.Zero;
                    playerGUI.Hide();
                    break;
            }
        }
    }

    public void GiveControl() 
    {
        if (this._state == State.Uncontrollable) {
            setState(State.Idle);
        }
    }

    public void TakeControl() 
    {
        if (this._state != State.Uncontrollable) {
            setState(State.Uncontrollable);
        }
    }

}
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

    private State _state = State.Uncontrollable;

    AnimationPlayer animationPlayer;

    Vector3 velocity;
    float speed = 0.5f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("Sitting");
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
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlideWithSnap(velocity, Vector3.Zero, Vector3.Up, true);
    }

    public void setState(State state) {
        if (this._state != state) {
            this._state = state;

            switch(state) {
                case State.Idle:
                    animationPlayer.Stop();
                    break;
                case State.Walking:
                    animationPlayer.Play("Walking");
                    break;
            }
        }
    }

    public void GiveControl() {
        if (this._state == State.Uncontrollable) {
            this._state = State.Idle;
        }
    }

    public void TakeControl() {
        if (this._state != State.Uncontrollable) {
            this._state = State.Uncontrollable;
        }
    }
}

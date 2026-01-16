using Godot;
using System;

public partial class MainCamera : Camera2D
{
	Vector2 mouseInitial;
	Vector2 mouseFinal;
	bool checkMouse; // stores whether or not the left mouse button has been held for at least one frame.
	public override void _Ready()
	{
		mouseFinal = GetViewport().GetMousePosition();
		checkMouse = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsMouseButtonPressed(MouseButton.Left)) // checks if the left mouse button is held. 
		{
			if (checkMouse) // validates that the button has been held for at least one frame before performing transformation.
			{
				mouseFinal = GetViewport().GetMousePosition(); // gets the final mouse position vector for use in calculating a movement vector.

				this.GlobalPosition -= (mouseFinal - mouseInitial) * 3; // transforms the camera's position by the negative of the mouse's movement vector.
			}
			mouseInitial = GetViewport().GetMousePosition(); // gets the initial mouse position vector for the following frame's transformation.
			checkMouse = true; // allows the transformation to occur on the next frame.
		}
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) // processes a mouse click event
		{
			switch(mouseEvent.ButtonIndex) // checks the button which was pressed.
			{
				case MouseButton.WheelUp:

					this.Zoom *= new Vector2(1.25f,1.25f); // increases zoom in the event the wheel is scrolled up.
					break;
				
				case MouseButton.WheelDown:

					this.Zoom *= new Vector2(0.75f,0.75f); // decreases zoom in the event the wheel is scrolled down.
					break;
			}
		}
		else if (@event is InputEventMouseButton mouseRelease && !mouseRelease.Pressed)
		{
			checkMouse = false; // resets the value of checkMouse when the left mouse button is released.
		}
    }
}

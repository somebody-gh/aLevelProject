using Godot;
using System;

public partial class SimpleButton : TextureButton
{
	/// <summary>
	/// The button alters the global "roomComplexity" variable when pushed. This button sets
	/// the complexity to 1. The button then broadcasts a signal which resets the state of all other
	/// buttons in the menu, before disabling itself.
	/// </summary>
	userSettings settings; // the singleton containing the user's settings
	buttonResetter buttonSignal; // the singleton containing the signal to be broadcast
	public override void _Ready()
	{
		// instantiating the singletons
		settings = GetNode<userSettings>("/root/UserSettings");
		buttonSignal = GetNode<buttonResetter>("/root/ButtonResetter");

		// connecting the signals
		buttonSignal.buttonReset += onReceive;
		this.Pressed += () => onPressed();

		if (settings.getRoomComplexity() == 1) // disables this button if the room complexity is already 1.
		{
			this.Disabled = true;
		}
	}

	void onPressed()
	{
		settings.setRoomComplexity(1); // calls "roomComplexity"'s setter function with parameter '1'
		buttonSignal.EmitSignal(nameof(buttonSignal.buttonReset)); // emits the reset signal
		this.Disabled = true; // disables this button. The texture changes automatically to indicate this.
	}

	void onReceive() // handles receiving the reset signal. 
	{
			this.Disabled = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _ExitTree()
    {
        buttonSignal.buttonReset -= onReceive;
    }
}

using Godot;
using System;

public partial class ComplexButton : TextureButton
{
	/// <summary>
	/// The button alters the global "roomComplexity" variable when pushed. This button sets
	/// the complexity to 0. The button then broadcasts a signal which resets the state of all other
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

		if (settings.getRoomComplexity() == 2) // disables this button if the room complexity is already 2.
		{
			this.Disabled = true;
		}
	}

	void onPressed()
	{
		settings.setRoomComplexity(2); // calls "roomComplexity"'s setter function with parameter '2'
		buttonSignal.EmitSignal(nameof(buttonSignal.buttonReset)); // emits the reset
		this.Disabled = true; // disables this button. The texture changes automatically to indicate this.
	}

	void onReceive() // handles receiving the reset signal. 
	{
		this.Disabled = false;
	}

    public override void _ExitTree()
    {
        buttonSignal.buttonReset -= onReceive;
    }
}

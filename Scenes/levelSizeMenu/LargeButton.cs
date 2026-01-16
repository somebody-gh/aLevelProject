using Godot;
using System;

public partial class LargeButton : TextureButton
{
		/// <summary>
	/// The button alters the global "levelSize" variable when pushed. This button sets
	/// the size to 15. The button then broadcasts a signal which resets the state of all other
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

		if (settings.getLevelSize() == 15) // disables this button if the level size is already 15.
		{
			this.Disabled = true;
		}
	}

	void onPressed()
	{
		settings.setLevelSize(15); // calls "levelSize"'s setter function with parameter '15'
		buttonSignal.EmitSignal(nameof(buttonSignal.buttonReset)); // emits the reset signal
		this.Disabled = true; // disables this button. The texture changes automatically to indicate this.
	}

	void onReceive() // handles receiving the reset signal. 
	{
			this.Disabled = false;
	}

    public override void _ExitTree()
    {
		buttonSignal.buttonReset -= onReceive; //disconnects the signal upon leaving the scene tree
    }
}

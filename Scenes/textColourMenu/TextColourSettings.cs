using Godot;
using System;

public partial class TextColourSettings : Control
{
	/// <summary>
	/// The control for the text colour menu. Stores the currently selected colour and sets the example label to 
	/// whatever colour button presses. When the user presses the "confirm" button, the settings singleton is updated to 
	/// represent the currently selected colour.
	/// </summary>
	userSettings settings;
	TextureButton[] colourButtons;
	TextureButton confirmationButton;
	RichTextLabel exampleLabel;
	RichTextLabel confirmationLabel;
	Color currentColour;
	public override void _Ready()
	{
		settings = GetNode<userSettings>("/root/UserSettings"); // gets the user settings singleton

		colourButtons = new TextureButton[7]; // instantiates an array to put the colour buttons in

		//gets the buttons themselves, and places them in the array
		colourButtons[0] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/blackButton");
		colourButtons[1] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/whiteButton");
		colourButtons[2] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/redButton");
		colourButtons[3] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/greenButton");
		colourButtons[4] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/skyBlueButton");
		colourButtons[5] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/yellowButton");
		colourButtons[6] = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/HBoxContainer/pinkButton");
		
		// gets the example label and confirmation button for manipulation
		confirmationButton = this.GetNode<TextureButton>("CanvasLayer/MarginContainer/VBoxContainer/confirmationButton");
		exampleLabel = this.GetNode<RichTextLabel>("CanvasLayer/MarginContainer/VBoxContainer/exampleLabel");
		confirmationLabel = this.GetNode<RichTextLabel>("CanvasLayer/MarginContainer/VBoxContainer/confirmationButton/confirmationLabel");
		currentColour = settings.getTextColour();

		// connects the pressed signals of the various colour buttons to their respective methods.
		colourButtons[0].Pressed += onBlackPressed;
		colourButtons[1].Pressed += onWhitePressed;
		colourButtons[2].Pressed += onRedPressed;
		colourButtons[3].Pressed += onGreenPressed;
		colourButtons[4].Pressed += onSkyBluePressed;
		colourButtons[5].Pressed += onYellowPressed;
		colourButtons[6].Pressed += onPinkPressed;
		confirmationButton.Pressed += onConfirm;

		// changes the font colour and size of the example label to be in line with the users settings.
		exampleLabel.AddThemeFontSizeOverride("normal_font_size", settings.getTextSize());
		exampleLabel.AddThemeColorOverride("default_color", settings.getTextColour());

		if (currentColour == new Color(0,0,0,1))
		{
			colourButtons[0].Disabled = true;
		}
		else if (currentColour == new Color(1,1,1,1))
		{
			colourButtons[1].Disabled = true;
		}
		else if (currentColour == new Color(1,0,0,1))
		{
			colourButtons[2].Disabled = true;
		}
		else if (currentColour == new Color(0.294F,0.549F,0.094F,1))
		{
			colourButtons[3].Disabled = true;
		}
		else if (currentColour == new Color(0.639F,0.937F,1,1))
		{
			colourButtons[4].Disabled = true;
		}
		else if (currentColour == new Color(1,0.953F,0,1))
		{
			colourButtons[5].Disabled = true;
		}
		else if (currentColour == new Color(0.945F,0.561F,0.827F,1))
		{
			colourButtons[6].Disabled = true;
		}
	}

	void onBlackPressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[0].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(0,0,0,1); // sets the current colour to black
		exampleLabel.AddThemeColorOverride("default_color", currentColour); // changes the colour of the example label to black
	}

	void onWhitePressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[1].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(1,1,1,1); // sets the current colour to white
		exampleLabel.AddThemeColorOverride("default_color",currentColour); // changes the example label to white
	}

	void onRedPressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[2].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(1,0,0,1); // sets the current colour to red
		exampleLabel.AddThemeColorOverride("default_color",currentColour); // changes the example label to red
	}

	void onGreenPressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[3].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(0.294F,0.549F,0.094F,1); // sets the current colour to green
		exampleLabel.AddThemeColorOverride("default_color",currentColour); // sets the example label to green
	}

	void onSkyBluePressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[4].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(0.639F,0.937F,1,1); // changes the current colour to blue
		exampleLabel.AddThemeColorOverride("default_color",currentColour); // sets the example label to blue
	}

	void onYellowPressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[5].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(1,0.953F,0,1); // sets the current colour to yellow
		exampleLabel.AddThemeColorOverride("default_color",currentColour); // sets the example label to yellow
	}

	void onPinkPressed()
	{
		enableButtons(); // enables all the buttons
		colourButtons[6].Disabled = true; // disables the button that was just pressed

		currentColour = new Color(0.945F,0.561F,0.827F,1); // sets the current colour to pink
		exampleLabel.AddThemeColorOverride("default_color",currentColour); // sets the example label to pink.
	}

	void onConfirm()
	{
		settings.setTextColour(currentColour); // writes the current colour to the user settings singleton
		confirmationLabel.AddThemeColorOverride("default_color",currentColour);
	}

	void enableButtons()
	{
		foreach (TextureButton t in colourButtons) // iterates through and enables all the buttons
		{
			t.Disabled = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

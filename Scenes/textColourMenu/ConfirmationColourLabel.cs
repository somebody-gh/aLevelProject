using Godot;
using System;

public partial class ConfirmationColourLabel : RichTextLabel
{
	userSettings settings;
	TextureButton confirmButton;
	public override void _Ready()
	{
		//initialises variables
		confirmButton = this.GetParent<TextureButton>();
		settings = GetNode<userSettings>("/root/UserSettings");

		//connects signal
		confirmButton.Pressed += onConfirm;
	}

	void onConfirm()
	{
		//changes this text colour when the button is pressed.
		this.AddThemeColorOverride("default_color", settings.getTextColour());
	}

	public override void _Process(double delta)
	{
	}
}

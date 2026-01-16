using Godot;
using System;

public partial class changeText : RichTextLabel
{
	/// <summary>
	/// Gets the text settings from the singleton, and applies them to this text label.
	/// </summary>
	private userSettings textSettings;
	public override void _Ready()
	{
		textSettings = GetNode<userSettings>("/root/UserSettings"); //gets the user settings singleton

		//adds the settings as overrides.
		this.AddThemeFontSizeOverride("normal_font_size", textSettings.getTextSize());
		this.AddThemeColorOverride("default_color", textSettings.getTextColour());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

using Godot;
using System;

public partial class ExampleTextLabel : RichTextLabel
{
	/// <summary>
	/// Changes the size of the text in the label every time the slider is changed. 
	/// The label begins as the size stored in the settings singleton.
	/// </summary>
	HSlider sizeSlider;
	VBoxContainer vBox;
	userSettings settings;
	public override void _Ready()
	{
		// gets other nodes in the current scene
		vBox = this.GetParent<VBoxContainer>();
		sizeSlider = vBox.GetNode<HSlider>("textSizeSlider");

		settings = GetNode<userSettings>("/root/UserSettings"); // gets the user settings singleton

		sizeSlider.Value = settings.getTextSize(); // sets the value of the slider to the value currently stored in the settings singleton
		this.AddThemeFontSizeOverride("normal_font_size", settings.getTextSize()); // sets the size of this label to the size currently stored in the settings singleton.
		this.AddThemeColorOverride("default_color", settings.getTextColour()); // sets the colour of the example label to the colour stored in the settings singleton.

		sizeSlider.ValueChanged += (double value) => onValueChanged(); // connects to the sliders "value changed" signal
	}

	void onValueChanged()
	{
		this.AddThemeFontSizeOverride("normal_font_size", Convert.ToInt32(sizeSlider.Value)); // changes the size of the example label whenever the slider is adjusted, to the value currently displayed on the slider
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

using Godot;
using System;

public partial class ConfirmationButton : TextureButton
{
	userSettings settings;
	HSlider sizeSlider;
	VBoxContainer vBox;
	RichTextLabel confirmationLabel;
	public override void _Ready()
	{
		// gets other nodes in the current scene
		vBox = this.GetParent<VBoxContainer>();
		sizeSlider = vBox.GetNode<HSlider>("textSizeSlider");
		confirmationLabel = this.GetNode<RichTextLabel>("confirmationLabel");

		settings = GetNode<userSettings>("/root/UserSettings"); // gets the user settings singleton

		this.Pressed += onPressed; // connects to its own "pressed" signal
	}

	void onPressed()
	{
		confirmationLabel.AddThemeFontSizeOverride("normal_font_size", Convert.ToInt32(sizeSlider.Value)); // changes the text on this button to the size displayed on the slider

		settings.setTextSize(Convert.ToInt32(sizeSlider.Value)); // writes the size selected on the slider to the settings singleton.
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

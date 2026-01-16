using Godot;
using System;

public partial class TextColourButton : TextureButton
{
	sceneChanger changeScene;
	public override void _Ready()
	{
		changeScene = GetNode<sceneChanger>("/root/SceneChanger"); //gets the sceneChanger singleton

		this.Pressed += onPressed; // connects to its own "pressed" signal
	}

	void onPressed()
	{
		changeScene.newScene("res://Scenes/textColourMenu/text_colour_settings.tscn"); // calls the sceneChange method with the path to the text colour settings page
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

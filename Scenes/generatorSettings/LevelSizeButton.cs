using Godot;
using System;

public partial class LevelSizeButton : TextureButton
{
	private sceneChanger changeScene;
	public override void _Ready()
	{
		changeScene = GetNode<sceneChanger>("/root/SceneChanger"); // gets the sceneChanger singleton

		this.Pressed += onPressed; // connects to its own "pressed" signal
	}

	void onPressed()
	{
		changeScene.newScene("res://Scenes/levelSizeMenu/levelSizeSettings.tscn"); // calls the sceneChange method with the path to the level size settings page
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

using Godot;
using System;

public partial class interfaceButton : TextureButton
{
	private sceneChanger changeScene;
	public override void _Ready()
	{
		changeScene = GetNode<sceneChanger>("/root/SceneChanger"); // gets the sceneChanger singleton

		this.Pressed += () => onPressed(); // connects to its own "pressed" symbol
	}

	void onPressed()
	{
		changeScene.newScene("res://Scenes/interfaceSettings/interfaceSettings.tscn"); // calls the sceneChange method with the path to the interface settings page
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

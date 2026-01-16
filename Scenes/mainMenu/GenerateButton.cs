using Godot;
using System;

public partial class GenerateButton : TextureButton
{
	sceneChanger changeScene;
	public override void _Ready()
	{
		changeScene = GetNode<sceneChanger>("/root/SceneChanger");
		this.Pressed += onPressed;
	}

	void onPressed()
	{
		changeScene.newScene("res://Scenes/generator/generator.tscn");
	}

	
	public override void _Process(double delta)
	{
	}
}

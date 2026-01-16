using Godot;
using System;

public partial class ExitButton : Control
{
	/// <summary>
	/// Calls the "backScene" method of the scene changer singleton when pressed
	/// </summary>
	sceneChanger priorScene;
	TextureButton thisButton;
	public override void _Ready()
	{
		
		priorScene = GetNode<sceneChanger>("/root/SceneChanger"); // gets the sceneChanger singleton
		thisButton = this.GetNode<TextureButton>("animatedButton"); // gets the texturebutton

		//connects to the pressed signal
		thisButton.Pressed += () => onPressed();
	}

	void onPressed()
	{
		priorScene.backScene(); // calls a method which disposes the current scene, and opens up the next scene in the stack
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

using Godot;
using System;
using System.Collections.Generic;

public partial class sceneChanger : Node
{
	/// <summary>
	/// Scene Changer singleton taken from the Godot docs.
	/// Link: https://docs.godotengine.org/en/stable/tutorials/scripting/singletons_autoload.html
	/// </summary>
	private Node currentScene;
	private Stack<PackedScene> visitedScenes = new Stack<PackedScene>();
	public override void _Ready()
	{
		Viewport sceneRoot = GetTree().Root;
		currentScene = sceneRoot.GetChild(-1); //Gets the current running scene.
	}

	public void newScene(string path)
	{
		CallDeferred(MethodName.DeferrednewScene, path); // calls the deferred method
	}

	public void DeferrednewScene(string path)
	{
		visitedScenes.Push(GD.Load<PackedScene>(GetTree().CurrentScene.SceneFilePath)); // sets the current scene as previous
		/// Clears the scene tree and adds new scene.
		currentScene.Free();
		PackedScene nextScene = GD.Load<PackedScene>(path);
		currentScene = nextScene.Instantiate();
		GetTree().Root.AddChild(currentScene);
		
		// registers the new scene as the current scene
		GetTree().CurrentScene = currentScene;
	}

	public void backScene()
	{
		CallDeferred(MethodName.DeferredbackScene); // calls the deferred method
	}

	public void DeferredbackScene()
	{
		if (visitedScenes.Count == 0) // does nothing if this is the first visited scene
		{
			return;
		}
		else
		{
			currentScene.Free(); // clears the current scene

			PackedScene newScene = visitedScenes.Pop(); // gets the last visited scene from the stack 

			// adds this as the new scene
			currentScene = newScene.Instantiate();
			GetTree().Root.AddChild(currentScene);

			GetTree().CurrentScene = currentScene; // registers the new scene as the current scene
		}
	}
}

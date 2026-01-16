using Godot;
using System;

public partial class buttonResetter : Node
{
	/*
	A signal that can be used to ensure that all other buttons in a menu are reset once one is selected.
	*/
	[Signal]
	public delegate void buttonResetEventHandler();
}

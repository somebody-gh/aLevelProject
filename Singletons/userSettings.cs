using Godot;
using System;
using System.Data;

public partial class userSettings : Node
{
	//variables
	private Color textColour;
	private int textSize;
	private int levelSize;
	private int roomComplexity;
	private ConfigFile settingsFile;

    public override void _Ready()
    {
		//initialises variables and loads them from file
		settingsFile = new ConfigFile();
		textColour = new Color();
        loadSettings();
    }

    // getter functions
    public Color getTextColour()
	{
		return textColour;
	}
	public int getTextSize()
	{
		return textSize;
	}
	public int getLevelSize()
	{
		return levelSize;
	}
	public int getRoomComplexity()
	{
		return roomComplexity;
	}

	//setter functions
	public void setTextColour(Color pColour)
	{
		textColour = pColour;

		//Saves change to file
		settingsFile.SetValue("Settings","textColour",textColour);
		settingsFile.Save("user://settings.cfg");
	}
	public void setTextSize(int pSize)
	{
		textSize = pSize;

		//Saves change to file
		settingsFile.SetValue("Settings","textSize", textSize);
		settingsFile.Save("user://settings.cfg");
	}
	public void setLevelSize(int pSize)
	{
		levelSize = pSize;

		//Saves change to file
		settingsFile.SetValue("Settings","levelSize",levelSize);
		settingsFile.Save("user://settings.cfg");
	}
	public void setRoomComplexity(int pComplexity)
	{
		roomComplexity = pComplexity;

		//Saves change to file
		settingsFile.SetValue("Settings","roomComplexity",roomComplexity);
		settingsFile.Save("user://settings.cfg");
	}

	private void loadSettings()
	{
		/* code for loading a config file taken from the godot docs.
		Link: https://docs.godotengine.org/en/stable/classes/class_configfile.html#class-configfile
		*/
		Error checkFile = settingsFile.Load("user://settings.cfg");
		if (checkFile != Error.Ok)
		{
			setTextColour(new Color(0,0,0,1));
			setTextSize(15);
			setLevelSize(10);
			setRoomComplexity(0);
			
			// saves the default configuration.
			settingsFile.Save("user://settings.cfg");
		}
		else
		{
			//sets the values to the relevant variables in the event of the file loading correctly. 
			textColour = (Color)settingsFile.GetValue("Settings","textColour");
			textSize = (int)settingsFile.GetValue("Settings","textSize");
			levelSize = (int)settingsFile.GetValue("Settings","levelSize");
			roomComplexity = (int)settingsFile.GetValue("Settings","roomComplexity");
		}
	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

public partial class Generator : Node2D
{
	userSettings settings;
	Vertex[,] vertices;
	int startPointer;
	int endPointer;
	int numVertices;
	
	public override void _Ready()
	{
		settings = GetNode<userSettings>("/root/UserSettings"); // accesses the settings singleton

		if (settings.getLevelSize() > 0 && settings.getLevelSize() < 21 && settings.getRoomComplexity() >= 0 && settings.getRoomComplexity() < 5) // validates that the level size is within acceptable parameters
		{
		vertices = new Vertex[(settings.getLevelSize() * 2),(settings.getLevelSize() * 2)]; // creates the array in which the vertices will be stored.

		vertices[settings.getLevelSize(),settings.getLevelSize()] = new Vertex(0,0,null); // creates the root node. As it is the root node, it has no parent node, and so "null" is passed as that parameter
		
		this.AddChild(vertices[settings.getLevelSize(),settings.getLevelSize()]);
		vertices[settings.getLevelSize(),settings.getLevelSize()].visualise();

		numVertices = 1; // begins counting the number of created vertices

		// creates the pointers. These refer to the depth values in the vertices array at which iteration will begin / end from, respectively.
		startPointer =  settings.getLevelSize();
		endPointer = startPointer;

		generateMap(); // calls the method which begins graph generation.

		connectGraph(); // calls the method which adds more connections between nodes.
		
		visualiseMap();
		//testRoutine();
		}
		else
		{
			RichTextLabel errorLabel = new RichTextLabel(); // initialises the label to be displayed.

			errorLabel.Text = "Size variable invalid."; // adds the error message as its text property.

			// gets the user's settings from the singleton and changes the label to correlate with them.
			errorLabel.AddThemeFontSizeOverride("normal_font_size", settings.getTextSize());
			errorLabel.AddThemeColorOverride("default_color", settings.getTextColour());

			// changes the label's properties to make it display properly.
			errorLabel.FitContent = true;
			errorLabel.AutowrapMode = TextServer.AutowrapMode.Off;

			this.AddChild(errorLabel); // adds the label to the scene tree.
		}
	}

	/// <summary>
	/// Generates the initial map of the nodes. Loops infinitely while the number of vertices is less than the desired level size.
	/// Within the loop, iterates through the vertices array left - right and up - down,
	/// starting and ending at the rows indicated by startpointer and endpointer respectively.
	/// </summary>
	void generateMap()
	{
		while (numVertices < settings.getLevelSize()) // continuously loops the method until enough nodes have been created.
		{
			for (int d = startPointer; d >= endPointer; d--)
			{
				for (int w = 0; w < (settings.getLevelSize() * 2); w++)
				{
					if (vertices[w,d] != null) // ensures that the target node actually exist.
					{
						generateVertex(vertices[w,d] ,w ,d); // runs the method to expand the graph from the selected target vertex.
						if (numVertices == settings.getLevelSize()){return;} // exits if enough nodes have been created.
					}
				}
			}
		}
	}

	/// <summary>
	/// Iterates through the vertices array, left -> right and top -> bottom, and if a vertex is non-null, adds
	/// connections between it and some non-null adjacent vertices, at random. 
	/// </summary>
	void connectGraph()
	{
		for (int d = startPointer; d >= endPointer; d--)
		{
			for (int w = 0; w < (settings.getLevelSize() * 2); w++)
			{
				if (vertices[w,d] != null) // checks that the current target vertex exists.
				{
					connectVertex(vertices[w,d],w,d); // calls the connection subprogram. 
				}
			}
		}
	}

	void visualiseMap()
	{
		for (int d = startPointer; d >= endPointer; d--)
		{
			for (int w = 0; w < (settings.getLevelSize() * 2); w++)
			{
				if (vertices[w,d] != null)
				{
					vertices[w,d].visualiseConnections();
				}
			}
		}
	}

	/// <summary>
	/// Has a 50% chance to expand from the current target vertex in a random direction, given that direction
	/// does not already contain an existing vertex. Upon vertex creation, sets it as connected to the target vertex.
	/// </summary>
	/// <param name="targetVertex"> The vertex that is being expanded from. </param>
	/// <param name="pWidth"> The column of the target vertex in the vertices array. </param>
	/// <param name="pDepth"> The row of the target vertex in the vertices array. </param>
	void generateVertex(Vertex targetVertex, int pWidth, int pDepth)
	{
		if ((GD.Randi() % 2) == 0) // adds randomness through only running the rest of the method on succesful coin flip.
		{
			uint randomiser = GD.Randi(); // generates a random, large number, which will be used for selection.

			if ((randomiser % 4 == 0) && (vertices[pWidth - 1, pDepth] == null)) // validates that there is not a preexisting vertex in this direction.
			{
				vertices[pWidth -1, pDepth] = new Vertex(targetVertex.getPosition()[0] - 1, targetVertex.getPosition()[1], targetVertex); // creates a new vertex adjacent to the target vertex, 
				// and passes the target vertex as the "parentVertex" parameter in its constructor.

				targetVertex.addConnection(vertices[pWidth - 1, pDepth]); // adds the newly created vertex to the target vertex's connected vertices.

				this.AddChild(vertices[pWidth - 1, pDepth]);

				vertices[pWidth - 1, pDepth].visualise(); // places the node in its correct location in the scene, and generates the associated room.

				numVertices++; // increments the number of vertices.
			}
			else if ((randomiser % 4 == 1) && (vertices[pWidth + 1,pDepth] == null)) // validates that there is not a preexisting vertex in this direction.
			{
				vertices[pWidth + 1, pDepth] = new Vertex(targetVertex.getPosition()[0] + 1, targetVertex.getPosition()[1], targetVertex); // creates a new vertex adjacent to the target vertex, 
				// and passes the target vertex as the "parentVertex" parameter in its constructor.

				targetVertex.addConnection(vertices[pWidth + 1, pDepth]); // adds the newly created vertex to the target vertex's connected vertices.

				this.AddChild(vertices[pWidth + 1, pDepth]);

				vertices[pWidth + 1, pDepth].visualise(); // places the node in its correct location in the scene, and generates the associated room.

				numVertices++; // increments the number of vertices.
			}
			else if ((randomiser % 4 == 2) && (vertices[pWidth, pDepth - 1] == null)) // validates that there is not a preexisting vertex in this direction.
			{
				if (checkRow(pDepth - 1)) // validates that the row immediately below the target vertex is empty.
				{
					endPointer = pDepth - 1;
				}
				vertices[pWidth, pDepth - 1] = new Vertex(targetVertex.getPosition()[0], targetVertex.getPosition()[1] - 1, targetVertex); // creates a new vertex adjacent to the target vertex, 
				// and passes the target vertex as the "parentVertex" parameter in its constructor.

				targetVertex.addConnection(vertices[pWidth, pDepth - 1]); // adds the newly created vertex to the target vertex's connected vertices.

				this.AddChild(vertices[pWidth, pDepth - 1]);

				vertices[pWidth, pDepth - 1].visualise(); // places the node in its correct location in the scene, and generates the associated room.

				numVertices++; // increments the number of vertices.
			}
			else if ((randomiser % 4 == 3) && (vertices[pWidth, pDepth + 1] == null)) // validates that there is not a preexisting vertex in this direction.
			{
				if (checkRow(pDepth + 1)) // validates that the row above the target vertex is empty.
				{
					startPointer = pDepth + 1; // sets the start pointer to the new "highest" row.
				}
				vertices[pWidth, pDepth + 1] = new Vertex(targetVertex.getPosition()[0], targetVertex.getPosition()[1] + 1, targetVertex); // creates a new vertex adjacent to the target vertex, 
				// and passes the target vertex as the "parentVertex" parameter in its constructor.

				targetVertex.addConnection(vertices[pWidth, pDepth + 1]); // adds the newly created vertex to the target vertex's connected vertices.

				this.AddChild(vertices[pWidth, pDepth + 1]);

				vertices[pWidth, pDepth + 1].visualise(); // places the node in its correct location in the scene, and generates the associated room.

				numVertices++; // increments the number of vertices.
			}
		}
		return;
	}

	/// <summary>
	/// For each vertex adjacent to the target vertex in the vertices array, has a 20% chance to connect the two vertices,
	/// given a connection does not already exist.
	/// </summary>
	/// <param name="targetVertex"> The vertex which is being connected to it's neighbours </param>
	/// <param name="pWidth"> The column of the target vertex in the vertices array. </param>
	/// <param name="pDepth"> The row of the target vertex in the vertices array. </param>
	void connectVertex(Vertex targetVertex, int pWidth, int pDepth)
	{
		if ((pWidth != 0) && (vertices[pWidth - 1, pDepth] != null) && !vertices[pWidth - 1, pDepth].getConnections().Contains(targetVertex) && (GD.Randi() % 5) == 0) // validates that there exists a vertex adjacent to the target in this direction, 
		// and that it does not already have the target vertex as a connection. 
		{
			// adds the adjacent vertex as a connection to the target vertex, and vice versa.
			targetVertex.addConnection(vertices[pWidth - 1, pDepth]);
			vertices[pWidth - 1, pDepth].addConnection(targetVertex);
		}

		if ((pWidth < vertices.GetLength(0) - 1) && (vertices[pWidth + 1, pDepth] != null) && !vertices[pWidth + 1, pDepth].getConnections().Contains(targetVertex) && (GD.Randi() % 5) == 0) // validates that there exists a vertex adjacent to the target in this direction, 
		// and that it does not already have the target vertex as a connection. 
		{
			// adds the adjacent vertex as a connection to the target vertex, and vice versa.
			targetVertex.addConnection(vertices[pWidth + 1, pDepth]);
			vertices[pWidth + 1, pDepth].addConnection(targetVertex);
		}

		if ((pDepth != 0) && (vertices[pWidth, pDepth - 1] != null) && !vertices[pWidth, pDepth - 1].getConnections().Contains(targetVertex) && (GD.Randi() % 5) == 0) // validates that there exists a vertex adjacent to the target in this direction, 
		// and that it does not already have the target vertex as a connection. 
		{
			// adds the adjacent vertex as a connection to the target vertex, and vice versa.
			targetVertex.addConnection(vertices[pWidth, pDepth - 1]);
			vertices[pWidth, pDepth - 1].addConnection(targetVertex);
		}

		if ((pDepth < vertices.GetLength(1) - 1) && (vertices[pWidth, pDepth + 1] != null) && !vertices[pWidth, pDepth + 1].getConnections().Contains(targetVertex) && (GD.Randi() % 5) == 0) // validates that there exists a vertex adjacent to the target in this direction, 
		// and that it does not already have the target vertex as a connection. 
		{
			// adds the adjacent vertex as a connection to the target vertex, and vice versa.
			targetVertex.addConnection(vertices[pWidth, pDepth + 1]);
			vertices[pWidth, pDepth + 1].addConnection(targetVertex);
		}
	}

	/// <summary>
	/// Iterates through a given row of the vertices array, and returns true only if that row contains only null values.
	/// </summary>
	/// <param name="pDepth"> The row to be iterated through. </param>
	/// <returns></returns>
	bool checkRow(int pDepth)
	{
		for (int w = 0; w < (settings.getLevelSize() * 2); w++)
		{
			if (vertices[w, pDepth] != null) // validates that the selected array location does not contain a vertex.
			{
				return false; // otherwise returns false.
			}
		}
		return true;
	}

	void testRoutine()
	{
		for (int d = 0; d < (settings.getLevelSize() * 2); d++)
		{
			string line = "";
			for (int w = 0; w < (settings.getLevelSize() * 2); w++)
			{
				if (vertices[w,d] == null)
				{
					line += "0";
				}
			
				else
				{
					line += 1;
				}
			}
			GD.Print(line);
		}

		for (int d = 0; d < (settings.getLevelSize() * 2); d++)
		{
			for (int w = 0; w < (settings.getLevelSize() * 2); w++)
			{
				if (vertices[w,d] != null)
				{
					GD.Print($"{vertices[w,d].getPosition()[0]},{vertices[w,d].getPosition()[1]} is connected to");
					foreach(Vertex v in vertices[w,d].getConnections())
					{
						if(v != null)
						{
							GD.Print($"{Convert.ToString(v.getPosition()[0])},{Convert.ToString(v.getPosition()[1])}");
						}
					}
				}
			}
		}
	}

	public override void _Process(double delta)
	{
	}
}

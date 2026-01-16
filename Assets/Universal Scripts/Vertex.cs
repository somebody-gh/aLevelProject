using Godot;
using System;
using System.Linq;

[GlobalClass]
public partial class Vertex : Node2D
{
	int[] position;
	Vertex[] connectedVertices;

	public Vertex(int pWidth, int pDepth, Vertex parentVertex)
	{
		position = new int[2]{pWidth, pDepth};
		connectedVertices = new Vertex[4];
		if (parentVertex != null)
		{
			this.addConnection(parentVertex);
		}
	}

	public void visualise()
	{
		this.GlobalPosition = new Vector2(position[0] * 200, position[1] * 200); // places the vertex in the correct location.

		this.AddChild(makeRoom());
	}

	public void visualiseConnections()
	{
		foreach(Vertex v in connectedVertices)
		{
			if (v != null)
			{
				Line2D newLine = new Line2D(); // instantiates a new Line2D node.

				//adds the current and connected vertices locations as points on this line.
				newLine.AddPoint(v.GlobalPosition);
				newLine.AddPoint(this.GlobalPosition);

				AddChild(newLine); // adds the line to the scene tree.
			}
		}
	}

	private Polygon2D makeRoom()
	{
		Polygon2D returnShape = new Polygon2D();

		// randomly generates the height and width of the room, between 25 and 124.
		uint height = (GD.Randi() % 100) + 25; 
		uint width = (GD.Randi() % 100) + 25; 

		// creates a vector array of length 4 from the height and width values, and sets it as the array of points on the polygon.
		returnShape.Polygon = new Vector2[]{new Vector2(this.GlobalPosition[0] - width, (this.GlobalPosition[1]) + (height)), new Vector2(this.GlobalPosition[0] + width, (this.GlobalPosition[1]) + height), new Vector2(this.GlobalPosition[0] + width, (this.GlobalPosition[1]) - height), new Vector2(this.GlobalPosition[0] - width, (this.GlobalPosition[1]) - height)};

		return returnShape; // returns the newly created polygon. 
	}

	public int[] getPosition()
	{
		return position;
	}

	public Vertex[] getConnections()
	{
		return connectedVertices;
	}

	public void setPosition(int pWidth, int pDepth)
	{
		position[0] = pWidth;
		position[1] = pDepth;
	}

	public void addConnection(Vertex newConnection)
	{
		int i = 0;
		foreach(Vertex v in connectedVertices)
		{
			if (v == null)
			{
				connectedVertices[i] = newConnection;
				return;
			}
			i++;
		}
	}
}

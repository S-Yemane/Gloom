using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
	public enum MazeDirection
	{
		NORTH, EAST, SOUTH, WEST
	};

	public Vector2Int size;

	public MazeCell cellPrefab;
	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	private int cellSize = 5;

	public Vector2Int RandomCoordinates
	{
		get
		{
			return new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
		}
	}

	public bool ContainsCoordinates(Vector2Int coordinate)
	{
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
	}

	private MazeCell[,] cells;

	public MazeCell GetCell(Vector2Int coordinates)
	{
		return cells[coordinates.x, coordinates.y];
	}

	public void Generate()
	{
		cells = new MazeCell[size.x, size.y];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		
		while (activeCells.Count > 0)
		{
			DoNextGenerationStep(activeCells);
		}
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells)
	{
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells)
	{
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized)
		{
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		Vector2Int coordinates = currentCell.coords + direction.ToVector2Int();
		if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
		{
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null)
			{
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else
			{
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else
		{
			CreateWall(currentCell, null, direction);
		}
	}

	private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazeWall wall = Instantiate(wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null)
		{
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	private MazeCell CreateCell(Vector2Int coords)
	{
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coords.x, coords.y] = newCell;
		newCell.coords = coords;
		newCell.name = "Maze Cell " + coords.x + ", " + coords.y;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3((coords.x - size.x * 0.5f + 0.5f) * cellSize, 0f, (coords.y - size.y * 0.5f + 0.5f) * cellSize);
		return newCell;
	}
}

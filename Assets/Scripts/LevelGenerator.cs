using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeDirections
{

	public const int Count = 4;

	public static LevelGenerator.MazeDirection RandomValue
	{
		get
		{
			return (LevelGenerator.MazeDirection)Random.Range(0, Count);
		}
	}
	private static Vector2Int[] vectors = {
		new Vector2Int(0, 1),
		new Vector2Int(1, 0),
		new Vector2Int(0, -1),
		new Vector2Int(-1, 0)
	};

	private static LevelGenerator.MazeDirection[] opposites = {
		LevelGenerator.MazeDirection.SOUTH,
		LevelGenerator.MazeDirection.WEST,
		LevelGenerator.MazeDirection.NORTH,
		LevelGenerator.MazeDirection.EAST
	};

	public static LevelGenerator.MazeDirection GetOpposite(this LevelGenerator.MazeDirection direction)
	{
		return opposites[(int)direction];
	}

	public static Vector2Int ToVector2Int(this LevelGenerator.MazeDirection direction)
	{
		return vectors[(int)direction];
	}

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};

	public static Quaternion ToRotation(this LevelGenerator.MazeDirection direction)
	{
		return rotations[(int)direction];
	}
}

public class LevelGenerator : MonoBehaviour
{
	public enum MazeDirection
	{
		NORTH, EAST, SOUTH, WEST
	};
	public float generationStepDelay;

	public Vector2Int size;

	public MazeCell cellPrefab;
	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

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
	private void Start()
	{
		StartCoroutine(Generate());
	}

	public MazeCell GetCell(Vector2Int coordinates)
	{
		return cells[coordinates.x, coordinates.y];
	}

	public IEnumerator Generate()
	{
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.y];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		
		while (activeCells.Count > 0)
		{
			yield return delay;
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
		newCell.transform.localPosition = new Vector3(coords.x - size.x * 0.5f + 0.5f, 0f, coords.y - size.y * 0.5f + 0.5f);
		return newCell;
	}
}

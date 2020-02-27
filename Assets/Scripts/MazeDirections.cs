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


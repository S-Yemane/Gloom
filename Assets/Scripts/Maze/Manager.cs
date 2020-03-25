using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
	public LevelGenerator mazePrefab;

	public Vector2Int size;

	public bool RandomSeed;

	[Range(1, 999)]
	public int seed = 1;

	public float delay;

	private LevelGenerator mazeInstance;

    public NavMeshSurface surface;

	private void Start()
	{
		BeginGame();
        //UPDATES NAVMESH
        surface.BuildNavMesh();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			RestartGame();
            //UPDATES NAVMESH
            surface.BuildNavMesh();
		}
	}

	private void BeginGame()
	{
		Random.InitState(RandomSeed ? Random.Range(1, 999) : seed);
		mazeInstance = Instantiate(mazePrefab, transform.position, transform.rotation);
		mazeInstance.size = this.size;
		mazeInstance.Generate();
	}

	private void RestartGame()
	{
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}

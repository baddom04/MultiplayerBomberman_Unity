using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapCreator
{
    private readonly GameObject _boulderPrefab;
    private readonly GameObject _cratePrefab;
    private readonly GameObject[] _pickups;
    private readonly PlayerLogic _player1;
    private readonly PlayerLogic _player2;
    private readonly Transform _parentEnvironment;
    private readonly float _boulderSpawnY;
    private readonly float _crateChance;
    private readonly float _pickupChance;
    private int _maxGridIndex;
    private GameObject[,] _level;

    public MapCreator(
        [Inject(Id = "boulder")] GameObject boulderPrefab,
        [Inject(Id = "crate")] GameObject cratePrefab,
        GameObject[] pickups,
        [Inject(Id = "environment")] Transform parentEnvironment,
        [Inject(Id = "player1")] PlayerLogic player1,
        [Inject(Id = "player2")] PlayerLogic player2
        )
    {
        _boulderPrefab = boulderPrefab;
        _cratePrefab = cratePrefab;
        _parentEnvironment = parentEnvironment;
        _pickups = pickups;
        _player1 = player1;
        _player2 = player2;
        _boulderSpawnY = -1;
        _crateChance = 0.2f;
        _pickupChance = 1f;
    }
    public void InitiateMap(int gridSize)
    {
        _level = new GameObject[gridSize, gridSize];
        _maxGridIndex = gridSize - 1;
        InnerWallsAndCrates(gridSize);
        PlayerPositions(gridSize);
        OuterWalls(gridSize);
    }
    private void PlayerPositions(int gridSize)
    {
        int x = gridSize / 2 * 10;
        _player1.transform.position = new Vector3(x, _player1.transform.position.y, _player1.transform.position.z);
        _player2.transform.position = new Vector3(-x, _player2.transform.position.y, _player2.transform.position.z);
    }
    private void OuterWalls(int gridSize)
    {
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = 0; j < (gridSize + 2); j++)
            {
                int x = (j - gridSize / 2 - 1) * 10;
                int z = i * (gridSize / 2 + 1) * 10;
                GameObject.Instantiate(_boulderPrefab, new Vector3(x, _boulderSpawnY, z), Quaternion.identity, _parentEnvironment);
            }
        }
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = 0; j < gridSize; j++)
            {
                int x = i * (gridSize / 2 + 1) * 10;
                int z = (j - gridSize / 2) * 10;
                GameObject.Instantiate(_boulderPrefab, new Vector3(x, _boulderSpawnY, z), Quaternion.identity, _parentEnvironment);
            }
        }
    }
    private void InnerWallsAndCrates(int gridSize)
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                int x = (j - gridSize / 2) * 10;
                int z = (_maxGridIndex - i - gridSize / 2) * 10;
                if (j % 2 == 1 && i % 2 == 1)
                {
                    _level[i, j] = GameObject.Instantiate(_boulderPrefab, new Vector3(x, _boulderSpawnY, z), Quaternion.identity, _parentEnvironment);
                }
                else if (CalculateChance(_crateChance) && NotInPlayerColumn(i, j))
                {
                    _level[i, j] = GameObject.Instantiate(_cratePrefab, new Vector3(x, 0, z), Quaternion.identity, _parentEnvironment);
                    if (CalculateChance(_pickupChance))
                        GameObject.Instantiate(_pickups[Random.Range(0, _pickups.Length)], new Vector3(x, 3.5f, z), Quaternion.identity, _parentEnvironment);
                }
            }
        }
    }
    private bool CalculateChance(float chance)
    {
        return Random.Range(0, (int)(1 / chance)) == 0;
    }
    private bool NotInPlayerColumn(int i, int j)
    {
        return j != 0 && j != _maxGridIndex;
    }
    public GameObject[,] GetLevel()
    {
        return _level;
    }
}

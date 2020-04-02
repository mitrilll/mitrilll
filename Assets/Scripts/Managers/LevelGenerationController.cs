using System;
using System.Collections;
using System.Collections.Generic;
using PolygonTopDown;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerationController : MonoBehaviour
{
    private void Start()
    {
        var initialChunk = GenerateRandomChunk();
        initialChunk.transform.position = Vector3.zero;
        GenerateNeighbourChunks(initialChunk);
        
    }

    private Chunk GenerateRandomChunk()
    {
        var chunks = SettingsManager.Instance.Chunks;
        var prefab = chunks[Random.Range(0, chunks.Count)];

        var spawnedChunk = Instantiate(prefab);
        spawnedChunk.EventPlayerEntered += OnPlayerEnteredChunk;
        return spawnedChunk;
    }

    private void GenerateNeighbourChunks(Chunk center)
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (center.NeighbourChunks.ContainsKey(direction))
                continue;
            var neighbour = GenerateRandomChunk();
            center.NeighbourChunks[direction] = neighbour;
            neighbour.NeighbourChunks[direction.GetOpposite()] = center;

            Transform centerChunkDockPoint = center.DockPoints[direction];
            Transform neighbourChunkDockPoint = neighbour.DockPoints[direction.GetOpposite()];
            Vector3 offset = centerChunkDockPoint.position - neighbourChunkDockPoint.position;
            neighbour.transform.position += offset;
            //Transform centerChunkDockPoint = center._dockPointsSettings.Find(kvp => kvp, Direction == direction)?.Point;
        }
    }

    private void OnPlayerEnteredChunk(Chunk chunk)
    {
        GenerateNeighbourChunks(chunk);
    }
}

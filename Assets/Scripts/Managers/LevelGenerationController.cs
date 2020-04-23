using System;
using System.Collections;
using System.Collections.Generic;
using PolygonTopDown;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolygonTopDown
{
    public class LevelGenerationController : MonoBehaviour
    {
        private void Start()
        {
            var initialChunk = GenerateRandomChunk
                (ChunkDockPointType.Undefined, false);
            initialChunk.transform.position = Vector3.zero;
            GenerateNeighbourChunks(initialChunk);

        }

        private Chunk GenerateRandomChunk(ChunkDockPointType type, bool generateAdditionalElements = true)
        {
            var suitableChunks = type != ChunkDockPointType.Undefined
                ? SettingsManager.Instance.Chunks.FindAll
                    (c => c.DockPoints.Exists(p => p.Type == type))
                : SettingsManager.Instance.Chunks;
            var prefab = suitableChunks[Random.Range(0, suitableChunks.Count)];

            var spawnedChunk = Instantiate(prefab);
            
            if (generateAdditionalElements)
            {
                SpawnEnemies(spawnedChunk);
            }

            spawnedChunk.EventPlayerEntered += OnPlayerEnteredChunk;
            return spawnedChunk;
        }

        private void GenerateNeighbourChunks(Chunk center)
        {
            foreach (var centerDockPoint in center.DockPoints)
            {
                if (center.NeighbourChunks.ContainsKey(centerDockPoint))
                    continue;
                
                var suitableChunks = centerDockPoint.Type != ChunkDockPointType.Undefined
                    ? SettingsManager.Instance.Chunks.FindAll
                        (c => c.DockPoints.Exists(p => p.Type == centerDockPoint.Type))
                    : SettingsManager.Instance.Chunks;


                Chunk spawnedChunk = null;
                
                    while (suitableChunks.Count > 0)
                    {
                        int randomizedIndex = Random.Range(0, suitableChunks.Count);
                        var prefab = suitableChunks[randomizedIndex];
                        suitableChunks.RemoveAt(randomizedIndex);

                        var neighbour = Instantiate(prefab);
                        
                        var neighbourDockPoint = neighbour.DockPoints.Find
                            (p => p.Type == centerDockPoint.Type);
                        
                        var targetDockPointRotation = Quaternion.LookRotation(-centerDockPoint.transform.forward,
                            centerDockPoint.transform.up);

                        var rotationOffset =
                            targetDockPointRotation * Quaternion.Inverse(neighbourDockPoint.transform.rotation);
                        neighbour.transform.rotation *= rotationOffset;

                        Vector3 offset = centerDockPoint.transform.position - neighbourDockPoint.transform.position;
                        neighbour.transform.position += offset;
                        
                        if (IsEnoughSpaceForChunk(neighbour))
                        {
                            SpawnEnemies(neighbour);
                            neighbour.EventPlayerEntered += OnPlayerEnteredChunk;
                            
                            center.NeighbourChunks[centerDockPoint] = neighbour;
                            neighbour.NeighbourChunks[neighbourDockPoint] = center;
                            break;
                        }
                        else
                        {
                            {
                                Destroy(neighbour.gameObject);
                            }
                        }
                    }
            }
        }

        private bool IsEnoughSpaceForChunk(Chunk chunk)
        {
            foreach (var boundingCollider in chunk.BoundingColliders)
            {
                var colliderPosition = boundingCollider.transform.TransformPoint(boundingCollider.center);
                var intersectedColliders =  Physics.OverlapBox
                    (colliderPosition, 
                    boundingCollider.size / 2f, 
                    boundingCollider.transform.rotation, 
                    LayerMask.GetMask("Chunk"),
                    QueryTriggerInteraction.Collide);
                
                

                if (intersectedColliders.Length != 0)
                    return false;
            }

            return true;
        }

        private void SpawnEnemies(Chunk chunk)
        {
            foreach (var spawnPointSettings in chunk.EnemiesSpawnPoints)
            {
                if (Random.Range(0, 2) == 1)
                {
                    var settingsManager = SettingsManager.Instance;

                    //var lambda = 
                    
                    var suitableEnemies =
                        settingsManager.Enemies.FindAll
                            (el => (el.Type & spawnPointSettings.EnemyType) != 0);
                    var enemyFab = suitableEnemies[Random.Range(0, suitableEnemies.Count)];

                    var enemyObject = Instantiate(enemyFab, 
                        spawnPointSettings.transform.position + Vector3.up * 0.01f,
                        spawnPointSettings.transform.rotation);
                        enemyObject.transform.SetParent(spawnPointSettings.transform);
                }
            }
        }

        private void OnPlayerEnteredChunk(Chunk chunk)
        {
            GenerateNeighbourChunks(chunk);
        }
    }
}

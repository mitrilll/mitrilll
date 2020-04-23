using System;
using System.Collections;
using System.Collections.Generic;
using PolygonTopDown;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolygonTopDown
{
    public class Chunk : MonoBehaviour
    {
        public event Action<Chunk> EventPlayerEntered;

        public readonly Dictionary<ChunkDockPoint, Chunk> NeighbourChunks = new Dictionary<ChunkDockPoint, Chunk>();
        
        public List<EnemySpawnPoint> EnemiesSpawnPoints
        {
            get { return _enemiesSpawnPoints; }
        }
        
        public List<BoxCollider> BoundingColliders
        {
            get { return _boundingColliders; }
        }
        
        public List<ChunkDockPoint> DockPoints
        {
            get { return _dockPoints; }
            
        }
        
        [SerializeField] private List<ChunkDockPoint> _dockPoints = null;
        [SerializeField] private List<EnemySpawnPoint> _enemiesSpawnPoints = null;

        [SerializeField] private List<BoxCollider> _boundingColliders = null;
        

        
        //public Dictionary<Direction, Transform> DockPoints = new Dictionary<Direction, Transform>();


        private void OnTriggerEnter(Collider other)
        {
            EventPlayerEntered?.Invoke(this);
        }
    }
}

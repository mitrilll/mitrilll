using System;
using System.Collections;
using System.Collections.Generic;
using PolygonTopDown;
using UnityEngine;
using UnityEngine.Serialization;

public class Chunk : MonoBehaviour
{
    public event Action<Chunk> EventPlayerEntered;
    [System.Serializable]
    public class DockPointSettings
    {
        [SerializeField] public Direction Direction = PolygonTopDown.Direction.North;
        [SerializeField] public Transform Point = null;

    }
    
    public readonly  Dictionary<Direction, Chunk> NeighbourChunks = new Dictionary<Direction, Chunk>();
    
    [SerializeField] private List<DockPointSettings> _dockPointsSettings = null;

   public Dictionary<Direction, Transform> DockPoints = new Dictionary<Direction, Transform>();

    private void Awake()
    {
        foreach (var dockPointsSettings in _dockPointsSettings)
        {
            DockPoints[dockPointsSettings.Direction] = dockPointsSettings.Point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EventPlayerEntered?.Invoke(this);
    }
}

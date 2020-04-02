using System;
using System.Collections;
using System.Collections.Generic;
using PolygonTopDown;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
   public static SettingsManager Instance;
   [SerializeField] public List<Chunk> Chunks = null;

   private void Awake()
   {
      Instance = this;
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using PolygonTopDown;
using UnityEngine;

namespace PolygonTopDown
{
   public class SettingsManager : SingletonGameObject<SettingsManager>
   {

      [SerializeField] public List<Chunk> Chunks = null;
      [SerializeField] public List<Enemy> Enemies = null;

      protected override void Awake()
      {
         base.Awake();
         
      }
   }
}

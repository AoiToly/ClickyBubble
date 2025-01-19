using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Serializable]
    public class Item
    {
        public Bubble Config;
        public int Weight;
    }

    public List<Item> Bubbles = new();
    public float Time;
    public int TargetScore;
    public int ItemCount;
}

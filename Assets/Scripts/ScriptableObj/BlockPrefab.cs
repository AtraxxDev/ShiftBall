using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "Scriptable Objects/BlockPrefabs", order =1)]
public class BlockPrefab : ScriptableObject
{
    public GameObject[] blockPrefabs;
    public GameObject bonusBlockPrefab;
    
}

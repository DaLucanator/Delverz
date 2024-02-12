using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataScriptableObject : ScriptableObject
{
    [CreateAssetMenu(fileName = "NewRoomData", menuName = "ScriptableObjects/LevelDataScriptableObject", order = 1)]
    public class SpawnManagerScriptableObject : ScriptableObject
    {
        [field: SerializeField] public string dateLastSaved { get; private set; }

    }
}

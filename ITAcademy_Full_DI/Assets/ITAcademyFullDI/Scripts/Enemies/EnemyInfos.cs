using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ITAcademy.FullDI
{
    public enum EnemyType
    {
        Ogr,
        Troll
    }

    [CreateAssetMenu(menuName = "Game/Create Enemy Infos", fileName = "EnemyInfos")]
    public class EnemyInfos : ScriptableObject
    {
        [field: SerializeField] public List<EnemyInfo> Enemies { get; private set; }

        public EnemyInfo GetEnemy(EnemyType type) => Enemies.First(enemy => enemy.Type == type);
    }

    [Serializable]
    public class EnemyInfo
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
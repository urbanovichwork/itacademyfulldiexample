using UnityEngine;

namespace ITAcademy.FullDI
{
    [CreateAssetMenu(fileName = "WeaponAutomated", menuName = "Game/Create WeaponAutomated Info")]
    public class WeaponAutomatedInfo : WeaponBaseInfo
    {
        [field: SerializeField] public int BurstCount { get; private set; }
        [field: SerializeField] public float BurstDelayInMilliseconds { get; private set; }
    }
}
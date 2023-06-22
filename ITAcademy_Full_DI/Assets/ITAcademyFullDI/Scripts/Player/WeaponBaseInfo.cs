using UnityEngine;

namespace ITAcademy.FullDI
{
    [CreateAssetMenu(fileName = "WeaponBase", menuName = "Game/Create WeaponBase Info")]
    public class WeaponBaseInfo : ScriptableObject
    {
        [field: SerializeField] public float FireDelayInSeconds { get; private set; }
        [field: SerializeField] public float FireDistance { get; private set; }
    }
}
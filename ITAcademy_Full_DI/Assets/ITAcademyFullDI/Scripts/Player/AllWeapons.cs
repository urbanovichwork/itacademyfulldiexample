using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ITAcademy.FullDI
{
    public enum WeaponType
    {
        Pistol,
        SMG,
        Automat,
        AutoPistol
    }

    [CreateAssetMenu(fileName = "Weapons", menuName = "Game/Create Weapons Info")]
    public class AllWeapons : ScriptableObject
    {
        [field: SerializeField] public List<WeaponItem> Weapons { get; private set; }

        public WeaponBaseInfo GetWeaponInfo(WeaponType type) =>
            Weapons.First(weapon => weapon.WeaponType == type).WeaponInfo;
    }

    [Serializable]
    public class WeaponItem
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public WeaponBaseInfo WeaponInfo { get; private set; }
    }
}
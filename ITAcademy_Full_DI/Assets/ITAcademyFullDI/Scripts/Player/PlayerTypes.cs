using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ITAcademy.FullDI
{
    [CreateAssetMenu(menuName = "Game/Create Player Types", fileName = "playerTypes")]
    public class PlayerTypes : ScriptableObject
    {
        [field: SerializeField] public List<PlayerTypeItem> Types { get; private set; }

        public PlayerTypeItem GetPlayerTypeItem(PlayerType type) => Types.First(playerType => playerType.Type == type);
    }

    [Serializable]
    public class PlayerTypeItem
    {
        [field: SerializeField] public PlayerType Type { get; private set; }
        [field: SerializeField] public PlayerInfo Info { get; private set; }
    }
}
using UnityEngine;

namespace ITAcademy.FullDI
{
    [CreateAssetMenu(menuName = "Game/Create Player Info", fileName = "playerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject ViewPrefab { get; private set; }
    }
}
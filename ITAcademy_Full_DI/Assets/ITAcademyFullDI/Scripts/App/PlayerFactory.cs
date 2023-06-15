using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ITAcademy.FullDI
{
    public class PlayerFactory
    {
        private readonly PlayerInput _playerInput;
        private readonly GameObject _playerPrefab;
        private readonly PlayerTypes _playerTypes;
        private readonly Transform _playerSpawnPoint;

        public PlayerController PlayerController { get; private set; }

        public PlayerFactory(PlayerInput playerInput, [Inject(Id = GameIds.PlayerId)] GameObject playerPrefab,
            [Inject(Id = GameIds.PlayerSpawnId)] Transform playerSpawnPoint, PlayerTypes playerTypes)
        {
            _playerInput = playerInput;
            _playerSpawnPoint = playerSpawnPoint;
            _playerPrefab = playerPrefab;
            _playerTypes = playerTypes;
        }

        public void Create(PlayerType type)
        {
            PlayerInfo playerType = _playerTypes.GetPlayerTypeItem(type).Info;
            var player = Object.Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            Object.Instantiate(playerType.ViewPrefab, player.transform);
            PlayerController = player.GetComponent<PlayerController>();
            PlayerController.Initialize(_playerInput, type, playerType.Speed);
        }
    }
}
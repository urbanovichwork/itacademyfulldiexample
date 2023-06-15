using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ITAcademy.FullDI
{
    public class RootInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerTypes _playerTypes;
        [SerializeField] private EnemyInfos _enemyInfos;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerInput);
            Container.BindInstance(_playerPrefab).WithId(GameIds.PlayerId);
            Container.BindInstance(_playerSpawnPoint).WithId(GameIds.PlayerSpawnId);
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();

            BindScriptableObjects();
        }

        private void BindScriptableObjects()
        {
            Container.BindInstance(_playerTypes);
            Container.BindInstance(_enemyInfos);
        }
    }
}
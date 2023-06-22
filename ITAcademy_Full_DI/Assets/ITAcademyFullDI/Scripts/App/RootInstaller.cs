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
        [SerializeField] private AllWeapons _allWeapons;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _playerSpawnPoint;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerInput);
            Container.BindInstance(_playerPrefab).WithId(GameIds.PlayerId);
            Container.BindInstance(_playerSpawnPoint).WithId(GameIds.PlayerSpawnId);
            Container.BindInstance(_bullet).WithId("bullet");
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle().NonLazy();

            BindScriptableObjects();
        }

        private void BindScriptableObjects()
        {
            Container.BindInstance(_playerTypes);
            Container.BindInstance(_enemyInfos);
            Container.BindInstance(_allWeapons);
        }
    }
}
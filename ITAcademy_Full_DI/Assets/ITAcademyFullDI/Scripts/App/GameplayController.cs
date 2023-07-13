using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace ITAcademy.FullDI
{
    [UsedImplicitly]
    public class GameplayController : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemyFactory _enemyFactory;
        private readonly GameObject _bullet;
        private readonly AllWeapons _allWeapons;

        public GameplayController(PlayerFactory playerFactory, EnemyFactory enemyFactory, AllWeapons allWeapons,
            [Inject(Id = "bullet")] GameObject bullet)
        {
            _allWeapons = allWeapons;
            _bullet = bullet;
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
        }

        async void IInitializable.Initialize()
        {
            _playerFactory.Create(PlayerType.Paladin);
            _playerFactory.PlayerController
                .AddWeapon(null, new Pistol(_allWeapons.GetWeaponInfo(WeaponType.Pistol), _bullet)).Forget();
            //_playerFactory.PlayerController.AddWeapon(null,
            //    new AutomatedPistol((WeaponAutomatedInfo) _allWeapons.GetWeaponInfo(WeaponType.Automat), _bullet));
            CreateEnemies().Forget();
            Debug.Log("HELLO");

            UniTask<GameObject> obj1Task = PlayerCreation();
            UniTask<GameObject> obj2Task = Player2Creation();
            var result = await UniTask.WhenAll(obj1Task, obj2Task);
            Debug.Log($"CONTINUE {result.Item1} and {result.Item2}");
            //await PlayerAnimationWelcome();
            //await EnemyCreation();
            //await EnemyAnimationWelcome();
            //await CameraLookThroughLevel();
            //StartGameplay();
        }

        private async UniTask<GameObject> PlayerCreation()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            Debug.Log("PLAYER 1 CREATED");
            return new GameObject();
        }

        private async UniTask<GameObject> Player2Creation()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            Debug.Log("PLAYER 2 CREATED");
            return new GameObject();
        }

        private async UniTaskVoid CreateEnemies()
        {
            await _enemyFactory.Create(EnemyType.Troll);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Debug.Log("ENEMY 1");
            await _enemyFactory.Create(EnemyType.Ogr);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Debug.Log("ENEMY 2");
            await _enemyFactory.Create(EnemyType.Ogr);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Debug.Log("ENEMY 3");
            await _enemyFactory.Create(EnemyType.Troll);
        }
    }
}
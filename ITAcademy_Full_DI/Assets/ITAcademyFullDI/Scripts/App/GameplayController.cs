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

        void IInitializable.Initialize()
        {
            _playerFactory.Create(PlayerType.Paladin);
            _playerFactory.PlayerController.AddWeapon(null,
                new Pistol(_allWeapons.GetWeaponInfo(WeaponType.Pistol), _bullet));
            //_playerFactory.PlayerController.AddWeapon(null,
            //    new AutomatedPistol((WeaponAutomatedInfo) _allWeapons.GetWeaponInfo(WeaponType.Automat), _bullet));
            _enemyFactory.Create(EnemyType.Troll);
            _enemyFactory.Create(EnemyType.Ogr);
            _enemyFactory.Create(EnemyType.Ogr);
            _enemyFactory.Create(EnemyType.Troll);
        }
    }
}
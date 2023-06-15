using JetBrains.Annotations;
using Zenject;

namespace ITAcademy.FullDI
{
    [UsedImplicitly]
    public class GameplayController : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemyFactory _enemyFactory;

        public GameplayController(PlayerFactory playerFactory, EnemyFactory enemyFactory)
        {
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
        }

        void IInitializable.Initialize()
        {
            _playerFactory.Create(PlayerType.Paladin);
            _enemyFactory.Create(EnemyType.Troll);
        }
    }
}
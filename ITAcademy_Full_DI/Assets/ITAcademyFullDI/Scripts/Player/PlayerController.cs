using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ITAcademy.FullDI
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _input;

        private PlayerType _type;
        private float _speed;
        private List<WeaponBase> _weapons;
        private EnemyService _enemyService;

        public void Initialize(PlayerInput input, EnemyService enemyService, PlayerType type, float speed)
        {
            _input = input;
            _enemyService = enemyService;
            _speed = speed;
            _type = type;

            _weapons = new List<WeaponBase>();
            AddListeners();
        }

        public Vector3 GetPosition() => transform.position;

        private void Update()
        {
            WeaponProcessing();
        }

        private void WeaponProcessing()
        {
            _weapons.ForEach(weapon =>
            {
                var weaponPos = weapon.GetPosition();
                var paramsToEnemy = _enemyService.GetEnemyClosestTo(weaponPos);
                if (paramsToEnemy.HasValue)
                {
                    weapon.UpdateTarget(paramsToEnemy.Value.Position, paramsToEnemy.Value.Distance);
                }
                else
                {
                    weapon.ResetTarget();
                }
                
                //weapon.Tick();
            });
        }

        private void AddListeners()
        {
            _input.actions[PlayerInputIds.MoveActionId].performed += OnMove;
        }

        private void RemoveListeners()
        {
            _input.actions[PlayerInputIds.MoveActionId].performed -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context) => OnMove(context.ReadValue<Vector2>());

        private void OnMove(Vector2 step)
        {
            transform.position += new Vector3(step.x, 0, step.y) * _speed;
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        public async UniTaskVoid AddWeapon(GameObject prefab, WeaponBase weapon)
        {
            weapon.AddOriginElement(transform);
            weapon.StartShootingProcess().Forget();
            _weapons.Add(weapon);
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            weapon.CancelShootingProcess();
        }
    }
}
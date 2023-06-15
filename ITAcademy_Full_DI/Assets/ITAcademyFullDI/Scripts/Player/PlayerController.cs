using UnityEngine;
using UnityEngine.InputSystem;

namespace ITAcademy.FullDI
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _input;

        private PlayerType _type;
        private float _speed;

        public void Initialize(PlayerInput input, PlayerType type, float speed)
        {
            _input = input;
            _speed = speed;
            _type = type;

            AddListeners();
        }

        public Vector3 GetPosition() => transform.position;

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
    }
}
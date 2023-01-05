using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class MoveCharacter
    {
        [Header("Movable")]
        [SerializeField] private float _speed = 3;

        [Header("Jump")]
        [SerializeField] private float _gravitation = 650;
        [SerializeField] private float _velocityJump = 2800;
        [SerializeField] private float _hightJump = 0.4f;
        [SerializeField] private float _checkPlanceRadius = 0.1f;
        [SerializeField] private float _checkPlanceRadiusJump = 0.1f;
        [SerializeField] private Transform _checkGroundPoint;
        [SerializeField] private LayerMask _layersGrounds;

        private float _startHight = 0;

        public Vector2 PlayerPosition(float MoveInputHorizontal, bool isJump) =>
            new Vector2(MoveRun(MoveInputHorizontal), MoveJump(isJump) * Time.fixedDeltaTime);

        private float MoveRun(float MoveInputHorizontal) => MoveInputHorizontal != 0 ? _speed * MoveInputHorizontal : 0;

        private float gravitySpeedUp = 1;
        private float MoveJump(bool isJump)
        {
            float resolt = 0f;

            if (isJump && IsGrounds(1))
                _startHight = _hightJump;
            if (IsGrounds(1))
                gravitySpeedUp = 1;

            if (_startHight > 0)
            {
                _startHight -= Time.deltaTime;
                resolt = _velocityJump * _startHight - (_gravitation * _startHight * _startHight) / 2;

            }
            else if (_startHight <= 0 && !IsGrounds())
            {
                gravitySpeedUp += Time.deltaTime;
                resolt = -_gravitation * gravitySpeedUp * gravitySpeedUp;
            }

            return resolt;
        }

        private bool IsGrounds(int i = 0) =>
            Physics2D.OverlapCircle(_checkGroundPoint.position, _checkPlanceRadius + _checkPlanceRadiusJump * i, _layersGrounds);
    }
}
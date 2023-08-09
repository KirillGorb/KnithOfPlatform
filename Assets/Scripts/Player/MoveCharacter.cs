using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class MoveCharacter
    {
        [Header("Movable")]
        [SerializeField] private float _startTimeSpeedVelocity = 0.1f;
        [SerializeField] private float _startSpeed = 0;
        [SerializeField] private float _endSpeed = 3;

        [Header("Jump")]
        [SerializeField] private float _gravitation = 650;
        [SerializeField] private float _velocityJump = 2800;
        [SerializeField] private float _hightJump = 0.4f;
        [SerializeField] private float _startTimeStan = 1;
        [SerializeField] private float _stanValue = -5;
        [SerializeField] private float _checkPlanceRadius = 0.1f;
        [SerializeField] private float _checkPlanceRadiusJump = 0.1f;
        [SerializeField] private Transform _checkGroundPoint;
        [SerializeField] private LayerMask _layersGrounds;

        private float _speed = 0;
        private float _timeStan;
        private float _startHight = 0;
        private float _timeVelocitySpeed = 0;
        private float moveInput = -2;
        private float _spetionSpeed = -2;

        private bool _isStart = true;
        private bool _isStan = false;

        public Vector2 PlayerPosition(float MoveInputHorizontal, bool isJump)
        {
            float j = MoveJump(isJump);
            if (j <= _stanValue)
                _isStan = true;
            if (_isStan)
                if (_timeStan >= 0)
                {
                    _timeStan -= Time.deltaTime;
                    return new Vector2(0, j * Time.fixedDeltaTime);
                }
                else
                {
                    _timeStan = _startTimeStan;
                    j = -1;
                    _isStan = false;
                }

            return new Vector2(MoveRun(MoveInputHorizontal), j * Time.fixedDeltaTime);
        }
        private float MoveRun(float MoveInputHorizontal)
        {
            if ((moveInput != MoveInputHorizontal && MoveInputHorizontal != 0) || _isStart)
            {
                _isStart = false;
                _speed = _startSpeed;
                _timeVelocitySpeed = _startTimeSpeedVelocity;
                moveInput = MoveInputHorizontal;
                _spetionSpeed = (_endSpeed - _startSpeed) / _startTimeSpeedVelocity;
            }

            if (MoveInputHorizontal != 0)
            {
                RunDeltaUp();
            }
            else
            {
                RunDeltaDown();
            }
            return _speed * moveInput;
        }
        private void RunDeltaUp()
        {
            if (_timeVelocitySpeed >= 0)
            {
                _timeVelocitySpeed -= Time.fixedDeltaTime;
                _speed += _spetionSpeed;
            }
        }
        private void RunDeltaDown()
        {
            if (_timeVelocitySpeed < _startTimeSpeedVelocity)
            {
                _timeVelocitySpeed += Time.fixedDeltaTime;
                _speed -= _spetionSpeed;
                if (_speed <= _startSpeed)
                    _speed = _startSpeed;
            }
            else
                _timeVelocitySpeed = _startTimeSpeedVelocity;
        }

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
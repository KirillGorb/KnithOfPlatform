using UnityEngine;

namespace Player
{
    public class AnimationCharacter
    {
        private bool _facingRight = false;

        private float _moveInputHorizontal = 0;

        public void Animation(Transform transform, Animator animator, float MoveInputHorizontal)
        {
            _moveInputHorizontal = MoveInputHorizontal;
            PlayAnimation(animator);
            Flip(transform);
        }

        private void PlayAnimation(Animator animator)
        {
            if (_moveInputHorizontal != 0)
                animator.SetBool("IsRun", true);
            else
                animator.SetBool("IsRun", false);

            if (Input.GetKeyDown(KeyCode.Space))
                animator.SetTrigger("Jump");
            if (Input.GetKeyDown(KeyCode.LeftControl))
                animator.SetBool("IsRoll", true);
            else
                animator.SetBool("IsRoll", false);

        }

        private void Flip(Transform transform)
        {
            if ((_facingRight && _moveInputHorizontal > 0) || (!_facingRight && _moveInputHorizontal < 0))
                ChangeDirection(transform);
        }

        private void ChangeDirection(Transform transform)
        {
            transform.Rotate(0, 180, 0);
            _facingRight = !_facingRight;
        }
    }
}
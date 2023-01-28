using UnityEngine;

public class AnimationCharacter
{
    private Transform _transform;
    private Animator _animator;

    private bool _facingRight = false;

    private float _moveInputHorizontal = 0;

    public AnimationCharacter(Transform transform, Animator animator)
    {
        _transform = transform;
        _animator = animator;
    }

    public void Animation(float moveInputHorizontal, bool isGrounded)
    {
        _moveInputHorizontal = moveInputHorizontal;
        PlayAnimation(_animator, isGrounded);
        Flip(_transform);
    }

    private void PlayAnimation(Animator animator, bool isGrounded)
    {
        if (_moveInputHorizontal != 0)
            animator.SetBool("IsRun", true);
        else
            animator.SetBool("IsRun", false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
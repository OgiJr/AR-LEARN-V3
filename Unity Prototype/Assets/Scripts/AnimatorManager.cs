using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private Animator defaultAnimator;
    private bool singleton = false;

    private AnimationClip[] animations;
    private int animationLength;
    private int currentAnimation = 0;

    private SwipeControls swipeControls;

    private void Update()
    {
        #region Singleton

        if (singleton == false)
        {
            if (this.gameObject.GetComponent<Animator>() != null)
            {
                animator = this.gameObject.GetComponent<Animator>();
                CreateAnimator();
                singleton = true;
            }
        }

        if (swipeControls == null)
        {
            swipeControls = this.gameObject.GetComponent<SwipeControls>();
        }
        #endregion

        swipeControls.SwipeDetect();

<<<<<<< HEAD
=======
        #region HaltAnimations
        if (Input.touchCount > 0 || Input.GetKey(KeyCode.A))
        {
            animator.enabled = false;
        }

        else
        {
            animator.enabled = true;
        }
        #endregion

>>>>>>> f658a2ea6b44743dbdcb32d1334c0903b6b8351f
        if (animationLength > .1f)
        {
            AnimationChange();
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(animations[currentAnimation].name))
            {
                animator.Play(animations[currentAnimation].name);
            }
        }

    }

    private void CreateAnimator()
    {
        animations = animator.runtimeAnimatorController.animationClips;
        animationLength = animations.Length;

        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].wrapMode = WrapMode.Loop;
        }
    }

    void AnimationChange()
    {
        if (swipeControls.controls[1] == true)
        {
            currentAnimation -= 1;

            if (currentAnimation >= animations.Length)
            {
                currentAnimation = 0;
            }

            else if (currentAnimation < 0)
            {
                currentAnimation = animations.Length - 1;
            }

            animator.Play(animations[currentAnimation].name);
        }

        else if (swipeControls.controls[2] == true || Input.GetKeyUp(KeyCode.S))
        {
            currentAnimation += 1;

            if (currentAnimation >= animations.Length)
            {
                currentAnimation = 0;
            }

            else if (currentAnimation < 0)
            {
                currentAnimation = animations.Length - 1;
            }

            animator.Play(animations[currentAnimation].name);
        }
    }
}
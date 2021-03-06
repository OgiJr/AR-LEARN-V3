﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the animation for AR Models through Unity Swipe Controls.
/// </summary>
public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private Animator defaultAnimator;
    private bool singleton = false;

    private AnimationClip[] animations;
    private int animationLength;
    private int currentAnimation = 0;

    private SwipeControls swipeControls;

    /// <summary>
    /// Update the animation by checking for swipes and checking the animation state.
    /// </summary>
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

        #region HaltAnimations
        if (Input.touchCount > 0)
        {
            if (animator != null)
            {
                animator.enabled = false;
            }
        }

        else
        {
            if (animator != null)
            {
                animator.enabled = true;
            }
        }
        #endregion

        if (animationLength > .1f)
        {
            AnimationChange();
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(animations[currentAnimation].name))
            {
                animator.Play(animations[currentAnimation].name);
            }
        }

    }

    /// <summary>
    /// This method creates gets the animations from the animator and structures them so that they can be modified and changed.
    /// </summary>
    private void CreateAnimator()
    {
        animations = animator.runtimeAnimatorController.animationClips;
        animationLength = animations.Length;

        for (int i = 0; i < animations.Length; i++)
        {
            animations[i].wrapMode = WrapMode.Loop;
        }
    }

    /// <summary>
    /// Here you change the current animation via swiping.
    /// <param name="Current Animation">The current animation variable</param>
    /// <return>The current animation is changed to plus one when swiped to right and minus one when swiped left.</return>
    /// </summary>
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
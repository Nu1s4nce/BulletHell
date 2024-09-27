using System;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animName)
    {
        _animator.Play(animName);
    }
}
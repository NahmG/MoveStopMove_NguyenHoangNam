using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Action<Character> _OnTriggerDoor;
    [SerializeField]
    Animator animator;
    bool isOpen = false;

    void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            _OnTriggerDoor?.Invoke(character);
            if (!isOpen)
            {
                isOpen = true;
                AnimationOpen();
            }
        }
    }

    void AnimationOpen()
    {
        animator.Play("Open");
    }
}
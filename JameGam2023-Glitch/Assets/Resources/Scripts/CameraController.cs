using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Animator animator;

    private void Awake()
    {
        instance = this;
    }

    public void jump()
    {
        animator.SetTrigger("jump");
    }

}

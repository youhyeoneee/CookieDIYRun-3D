using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrSlide : Gimmick
{

    [SerializeField] bool isJump = false;
    [SerializeField] bool isSlide = false;

    protected override void Rotate()
    {
        
    }
    protected override void ActivateGimmick(GameObject hitObject)
    {
        Cookie cookie = hitObject.GetComponent<Cookie>();
        cookie.isJump = isJump;
        cookie.isSlide = isSlide;
    }
}

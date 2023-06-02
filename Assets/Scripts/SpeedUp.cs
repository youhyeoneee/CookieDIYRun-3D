using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Gimmick
{
    protected override void Rotate()
    {
    }
   
    
    protected override void ActivateGimmick(GameObject hitObject)
    {
        Debug.Log("SpeedUp");
        Cookie cookie = hitObject.GetComponent<Cookie>();
        cookie.moveZSpeed = 3f;
        
    }

}

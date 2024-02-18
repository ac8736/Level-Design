using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public CharacterController2D characterController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        characterController.m_Grounded = true;
        characterController.OnLandEvent.Invoke();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        characterController.m_Grounded = false;
    }


}

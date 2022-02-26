///-----------------------------
///     Author          : Hardik Shah
///     Last Modified   : 2022/02/26
///     Description     : Script for checking the player is dead
///     Revision History: Checking wheather player is out of the level bounds
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with Death Plane");
        }
    }
}

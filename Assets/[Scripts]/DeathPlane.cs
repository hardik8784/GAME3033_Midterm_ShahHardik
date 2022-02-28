///-----------------------------
///     Author          : Hardik Shah
///     Last Modified   : 2022/02/26
///     Description     : Script for checking the player is dead
///     Revision History: Checking wheather player is out of the level bounds
/// ----------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with Death Plane");
            // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}

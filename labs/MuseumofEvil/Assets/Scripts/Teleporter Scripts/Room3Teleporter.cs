using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Make sure to add this, or you can't use SceneManager
using UnityEngine.SceneManagement;


public class Room3Teleporter : MonoBehaviour
{

     void OnTriggerEnter ( Collider other)
     {
          //other.name should equal the root of your Player object
          //if (other.name == "AdvancedPlayer") {
          //The scene number to load (in File->Build Settings)
          Debug.Log ( "touched box" );
          SceneManager.LoadScene ( "Room3" );
          //}
     }
}

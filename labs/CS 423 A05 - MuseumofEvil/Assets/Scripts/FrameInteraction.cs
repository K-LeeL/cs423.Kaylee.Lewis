using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingFrameInteraction : MonoBehaviour
{
     public GameObject existingPainting;
     public GameObject hiddenPainting;
     public Transform teleportLocation;
     public float chanceToReveal = 0.3f;
     public float chanceToDoNothing = 0.3f;

     private void OnTriggerEnter ( Collider other )
     {
          Debug.Log ( "Trigger entered" ); // Debugging

          if (other.CompareTag ( "Player" ))
          {
               Debug.Log ( "Player detected" ); // Debugging

               float rand = Random.Range ( 0f , 1f );

               if (rand < chanceToReveal)
               {
                    Debug.Log ( "Reveal painting" ); // Debugging
                    RevealPainting ( );
               }
               else if (rand < chanceToReveal + chanceToDoNothing)
               {
                    Debug.Log ( "Do nothing" ); // Debugging
               }
               else
               {
                    Debug.Log ( "Teleport player" ); // Debugging
                    TeleportPlayer ( other.transform );
               }
          }
     }

     private void RevealPainting ( )
     {
          if (existingPainting != null && hiddenPainting != null)
          {
               existingPainting.SetActive ( false );
               hiddenPainting.SetActive ( true );
          }
          else
          {
               Debug.Log ( "Paintings not set" ); // Debugging
          }
     }

     private void TeleportPlayer ( Transform playerTransform )
     {
          if (teleportLocation != null)
          {
               Debug.Log ( "Teleporting player" ); // Debugging
               Debug.Log ( "Teleport Location: " + teleportLocation.position );  // Debugging
               Debug.Log ( "Player Current Location: " + playerTransform.position );  // Debugging

               playerTransform.position = teleportLocation.position;  // Teleport the player

               Debug.Log ( "Player New Location: " + playerTransform.position );  // Debugging
          }
          else
          {
               Debug.Log ( "Teleport location not set" ); // Debugging
          }
     }
}
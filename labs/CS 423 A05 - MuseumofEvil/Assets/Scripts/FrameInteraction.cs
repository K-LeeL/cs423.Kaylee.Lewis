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

     private bool canTeleport = true;  // A flag to check if teleportation is allowed
     private float teleportCooldown = 2f;  // Duration after which teleportation is allowed again
     private bool isHiddenPaintingRevealed = false;

     private void OnTriggerEnter ( Collider other )
     {
          Debug.Log ( "Trigger entered" );

          if (other.CompareTag ( "Player" ) && canTeleport)
          {
               Debug.Log ( "Player detected" );

               float rand = Random.Range ( 0f , 1f );

               if (rand < chanceToReveal * 0.5f) // Half the chance to either reveal the hidden painting or revert to the original
               {
                    Debug.Log ( "Reveal hidden painting or revert to original" );
                    RevealPainting ( );
               }
               else if (rand < chanceToReveal) // The other half of the chance to do nothing
               {
                    Debug.Log ( "Do nothing" );
               }
               else
               {
                    Debug.Log ( "Teleport player" );
                    TeleportPlayer ( other.transform );
                    StartCoroutine ( TeleportCooldown ( ) );
               }
          }
     }

     private IEnumerator TeleportCooldown ( )
     {
          canTeleport = false;
          yield return new WaitForSeconds ( teleportCooldown );
          canTeleport = true;
     }

     private void RevealPainting ( )
     {
          if (existingPainting != null && hiddenPainting != null)
          {
               if (isHiddenPaintingRevealed)
               {
                    // If hidden painting is currently revealed, switch back to the existing painting
                    existingPainting.SetActive ( true );
                    hiddenPainting.SetActive ( false );
                    isHiddenPaintingRevealed = false;
               }
               else
               {
                    // Otherwise, reveal the hidden painting
                    existingPainting.SetActive ( false );
                    hiddenPainting.SetActive ( true );
                    isHiddenPaintingRevealed = true;
               }
          }
          else
          {
               Debug.Log ( "Paintings not set" );
          }
     }

     private void TeleportPlayer ( Transform playerTransform )
     {
          if (teleportLocation != null)
          {
               Debug.Log ( "Intended Teleport Location: " + teleportLocation.position ); // Debug this first

               PlayerController playerController = playerTransform.GetComponent<PlayerController> ( );
               if (playerController)
               {
                    playerController.TeleportTo ( teleportLocation.position );
               }

               Debug.Log ( "Player's Actual Position After Teleport: " + playerTransform.position ); // Then debug this
          }
          else
          {
               Debug.Log ( "Teleport location not set" );
          }
     }


}
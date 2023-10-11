using System.Collections;
using UnityEngine;

public class NPCWander : MonoBehaviour
{
     public float speed = 1.0f;
     public float minWaitTime = .25f;
     public float maxWaitTime = 1.0f;
     public LayerMask floorMask = 1 << 8; // Targeting "Flooring" layer
     public LayerMask obstacleMask = (1 << 9) | (1 << 10); // Targeting "Obstacles" and "Player" layers
     public Transform floorTransform; // Reference to the flooring object

     private Vector3 targetPosition;
     private bool isWandering = true;
     private Animator animator; // Reference to the Animator component

     private void Start ( )
     {
          animator = GetComponent<Animator> ( );
          StartCoroutine ( Wander ( ) );
     }

     IEnumerator Wander ( )
     {
          while (isWandering)
          {
               animator.speed = .25f; // Pause walking animation
               yield return new WaitForSeconds ( Random.Range ( minWaitTime , maxWaitTime ) );

               targetPosition = GetRandomPositionOnFloor ( );

               while (Vector3.Distance ( transform.position , targetPosition ) > 0.5f)
               {
                    animator.speed = 1; // Resume walking animation

                    // Raycast for obstacle detection
                    if (Physics.Raycast ( transform.position , (targetPosition - transform.position).normalized , Vector3.Distance ( transform.position , targetPosition ) , obstacleMask ))
                    {
                         animator.speed = .25f; // Pause walking animation when changing direction
                         yield return new WaitForSeconds ( Random.Range ( minWaitTime , maxWaitTime ) );

                         targetPosition = GetRandomPositionOnFloor ( );
                         continue;
                    }

                    // Move NPC towards the target position
                    Vector3 moveDirection = (targetPosition - transform.position).normalized;
                    transform.position += moveDirection * speed * Time.deltaTime;
                    transform.LookAt ( targetPosition ); // Make the NPC face the target position

                    yield return null;
               }
          }
     }
     Vector3 GetRandomPositionOnFloor ( )
     {
          Vector3 position = Vector3.zero;

          // Get the bounds of the floor's collider
          Bounds floorBounds = floorTransform.GetComponent<MeshCollider> ( ).bounds;

          // Repeat until we find a position on the floor
          while (true)
          {
               // Generate a random position within the bounds of the floor
               position = new Vector3 (
                   Random.Range ( floorBounds.min.x , floorBounds.max.x ) ,
                   10f ,
                   Random.Range ( floorBounds.min.z , floorBounds.max.z )
               );

               RaycastHit hit;
               // Cast ray downwards from the potential position to see if it hits the floor
               if (Physics.Raycast ( position , Vector3.down , out hit , 20f , floorMask ))
               {
                    position.y = hit.point.y; // Set the y position to the floor's position
                    break; // Exit loop once we find a valid position
               }
          }

          return position;
     }
}

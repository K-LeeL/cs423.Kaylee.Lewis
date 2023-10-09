using System.Collections;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
     public Material dissolveMaterial; // The material with the dissolve shader
     public float dissolveSpeed = 0.1f; // Speed of dissolving
     public string playerTag = "Player"; // Tag of the player object

     private Renderer objectRenderer; // Reference to the object's renderer
     private int dissolvePropertyID; // ID of the dissolve property for efficient access
     private float dissolveAmount = 0; // Current dissolve amount
     private bool isDissolving = false; // Whether the object is currently dissolving

     void Start ( )
     {
          objectRenderer = GetComponent<Renderer> ( );
          dissolvePropertyID = Shader.PropertyToID ( "DissolveAmount" );
     }

     void OnEnable ( )
     {
          dissolveAmount = 0;
          isDissolving = false;
          if (objectRenderer)
          {
               objectRenderer.material.SetFloat ( dissolvePropertyID , dissolveAmount );
          }
     }

     void Update ( )
     {
          if (isDissolving && objectRenderer)
          {
               dissolveAmount += Time.deltaTime * dissolveSpeed;
               objectRenderer.material.SetFloat ( dissolvePropertyID , dissolveAmount );

               if (dissolveAmount >= 1)
               {
                    gameObject.SetActive ( false );
               }
          }
     }

     private void OnTriggerEnter ( Collider other )
     {
          // Start dissolving if the player touches the object
          if (other.CompareTag ( playerTag ))
          {
               isDissolving = true;
          }
     }
}
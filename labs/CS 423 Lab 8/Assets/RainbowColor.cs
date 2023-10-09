using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColour : MonoBehaviour
{

	Renderer rend;
	Material material;

	// Start is called before the first frame update
	void Start ( )
	{
		rend = GetComponent<Renderer> ( );
		material = rend.material;
		material.SetColor ( "_Color" , Color.magenta );
	}

	void Update ( )
	{

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchControl : MonoBehaviour
{
    public float rotationSpeed = 5f;
 
	void OnMouseDrag()
	{
		float XaxisRotation = Input.GetAxis("Mouse X")*rotationSpeed;
		
		// select the axis by which you want to rotate the GameObject
		transform.Rotate (Vector3.down, XaxisRotation);
		
	}
}


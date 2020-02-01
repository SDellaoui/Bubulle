using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
	public BackgroundPlane Plane;
	public Vector3 ObjectInitialPosition;

	// Start is called before the first frame update
	void Start()
	{
		Plane = FindObjectOfType<BackgroundPlane>();
		ObjectInitialPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		float DistanceToBackgroundPlane = Mathf.Abs(Plane.Camera.transform.position.z - Plane.transform.position.z);
		float DistanceToObjectPlane = Mathf.Abs(Plane.Camera.transform.position.z - ObjectInitialPosition.z);
		Vector3 ObjectInitialProjected = new Vector3(
			ObjectInitialPosition.x,
			ObjectInitialPosition.y,
			Plane.transform.position.z
		);
		transform.position = new Vector3(
			(ObjectInitialProjected.x - Plane.Camera.transform.position.x) * DistanceToObjectPlane / DistanceToBackgroundPlane + Plane.Camera.transform.position.x,
			transform.position.y,
			transform.position.z
		);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStringColliderBehaviour : MonoBehaviour
{
	public float Radius = 1.0f;
	public float RepulsionStrength = 10.0f;

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(transform.position, Radius);
	}

	public bool IsInside(Vector3 Position)
	{
		Vector3 ColliderPosition = transform.position;
		Vector3 PositionToColliderPosition = ColliderPosition - Position;
		return PositionToColliderPosition.sqrMagnitude < Radius * Radius;
	}

	public Vector3 GetRepulsionForce(Vector3 Position)
	{
		Vector3 ColliderPosition = transform.position;
		return (Position - ColliderPosition).normalized * RepulsionStrength;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStringColliderBehaviour : MonoBehaviour
{
	public Vector3 Offset = Vector3.zero;
	public float Radius = 1.0f;
	public float RepulsionStrength = 10.0f;

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(transform.position + Offset, Radius);
	}

	public bool IsInside(Vector3 Position)
	{
		Vector3 ColliderPosition = transform.position + Offset;
		Vector3 PositionToColliderPosition = ColliderPosition - Position;
		return PositionToColliderPosition.sqrMagnitude < Radius * Radius;
	}

	public Vector3 GetRepulsionForce(Vector3 Position)
	{
		Vector3 ColliderPosition = transform.position + Offset;
		return (Position - ColliderPosition).normalized * RepulsionStrength;
	}
}

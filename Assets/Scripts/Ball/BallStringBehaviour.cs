using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStringBehaviour : MonoBehaviour
{
	public struct Simulation
	{
		public Simulation(int PointsCount)
		{
			Points = new Vector3[PointsCount];
			for (int PointIndex = 0; PointIndex < PointsCount; ++PointIndex)
			{
				Points[PointIndex] = Vector3.zero;
			}
		}

		public Vector3[] Points;
	}

	public int PointsCount = 10;
	public int CurrentState = 0;
	public float MaxSpringLength = 1.0f;
	public float SpringDampening = 10.0f;
	public float MicroFrictionStrength = 1.0f;
	public float FrictionStrength = 0.01f;
	public Simulation[] SimulationState;
	public Transform RootTransform;

	public Vector3[] PointsDebug;

	// Start is called before the first frame update
	void Start()
	{
		RootTransform = transform;
		SimulationState = new Simulation[2];
		for (int SimulationIndex = 0; SimulationIndex < SimulationState.Length; ++SimulationIndex)
		{
			SimulationState[SimulationIndex] = new Simulation(PointsCount);
		}
	}

	Vector3 GetFriction(Vector3 CurrentForce)
	{
		float CurrentForceStrength = CurrentForce.magnitude * FrictionStrength;

		return (-CurrentForce.normalized + new Vector3(Random.Range(-MicroFrictionStrength, MicroFrictionStrength), 0.0f, 0.0f)) * CurrentForceStrength;
	}

	// Update is called once per frame
	void Update()
	{
		float DeltaSeconds = Time.deltaTime;

		Simulation PreviousFrame = SimulationState[1 - CurrentState];
		Simulation CurrentFrame = SimulationState[CurrentState];

		CurrentFrame.Points[0] = transform.position;

		for (int PointIndex = 1; PointIndex < PointsCount; ++PointIndex)
		{
			CurrentFrame.Points[PointIndex] = PreviousFrame.Points[PointIndex];

			Vector3 CurrentForce = Vector3.zero;

			CurrentForce += Physics.gravity;
			Vector3 SpringForce = CurrentFrame.Points[PointIndex - 1] - CurrentFrame.Points[PointIndex];
			if (SpringForce != Vector3.zero)
			{
				SpringForce = SpringForce.normalized * Mathf.Max(SpringForce.magnitude - MaxSpringLength, 0.0f) * SpringDampening;
			}
			CurrentForce += SpringForce;
			CurrentForce += GetFriction(CurrentForce);

			CurrentFrame.Points[PointIndex] += CurrentForce * DeltaSeconds;
		}

		PointsDebug = CurrentFrame.Points;

		CurrentState = 1 - CurrentState;
	}

	void OnDrawGizmosSelected()
	{
		// Draw a yellow sphere at the transform's position

		Simulation CurrentFrame = SimulationState[CurrentState];
		for (int PointIndex = 0; PointIndex < PointsCount; ++PointIndex)
		{
			Gizmos.DrawSphere(CurrentFrame.Points[PointIndex], 1.0f);
		}
	}
}

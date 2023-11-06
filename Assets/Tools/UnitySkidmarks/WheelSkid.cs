using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Example skid script. Put this on a WheelCollider.
// Copyright 2017 Nition, BSD licence (see LICENCE file). http://nition.co
[RequireComponent(typeof(WheelCollider))]
public class WheelSkid : MonoBehaviour {

	[Header("Skid")]
	public AudioSource SkidSound;
	public float SkidSoundMultiplyer;

	[SerializeField]
	Rigidbody rb;
	[SerializeField]
	Skidmarks skidmarksController;
	
	WheelCollider wheelCollider;
	WheelHit wheelHitInfo;

	private const float SkidFxSpeed = 0.5f; // Min side slip speed in m/s to start showing a skid
	private const float MaxSkidIntensity = 20.0f; // m/s where skid opacity is at full intensity
	private const float WheelSlipMultiplier = 10.0f; // For wheelspin. Adjust how much skids show
	private const float MinSkidAmount = 10.0f;
	private const float MaxSkidAmount = 50.0f;
	private const float MinVelocityValue = 15.0f;
	int lastSkid = -1; // Array index for the skidmarks controller. Index of last skidmark piece this wheel used
	float lastFixedUpdateTime;

	[Header("Drift Points")] 
	public TextMeshProUGUI pointsText;
	
	public int pointsAmount = 0;
	private const int PointsForDrift = 10;

	private void Start()
	{
		if (pointsText != null)
		{
			pointsText.text = pointsAmount.ToString();
		}
	}

	protected void Awake() 
	{
		wheelCollider = GetComponent<WheelCollider>();
		lastFixedUpdateTime = Time.time;
	}

	protected void FixedUpdate() 
	{
		lastFixedUpdateTime = Time.time;
	}

	protected void LateUpdate() 
	{
		if (wheelCollider.GetGroundHit(out wheelHitInfo))
		{
			// Check sideways speed

			// Gives velocity with +z being the car's forward axis
			Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
			float skidTotal = Mathf.Abs(localVelocity.x);

			// Check wheel spin as well

			float wheelAngularVelocity = wheelCollider.radius * ((2 * Mathf.PI * wheelCollider.rpm) / 60);
			float carForwardVel = Vector3.Dot(rb.velocity, transform.forward);
			float wheelSpin = Mathf.Abs(carForwardVel - wheelAngularVelocity) * WheelSlipMultiplier;

			Debug.Log(carForwardVel);
			// NOTE: This extra line should not be needed and you can take it out if you have decent wheel physics
			// The built-in Unity demo car is actually skidding its wheels the ENTIRE time you're accelerating,
			// so this fades out the wheelspin-based skid as speed increases to make it look almost OK
			wheelSpin = Mathf.Max(0, wheelSpin * (10 - Mathf.Abs(carForwardVel)));

			skidTotal += wheelSpin;

			// Skid if we should
			if (skidTotal >= SkidFxSpeed) {

				float intensity = Mathf.Clamp01(skidTotal / MaxSkidIntensity);
				// Account for further movement since the last FixedUpdate
				Vector3 skidPoint = wheelHitInfo.point + (rb.velocity * (Time.time - lastFixedUpdateTime));
				lastSkid = skidmarksController.AddSkidMark(skidPoint, wheelHitInfo.normal, intensity, lastSkid);
				SkidSound.volume = intensity / SkidSoundMultiplyer;
				
				if (skidTotal is >= MinSkidAmount and < MaxSkidAmount 
				    && pointsText != null && Mathf.Abs(carForwardVel) >= MinVelocityValue)
				{
					pointsAmount += PointsForDrift;
					pointsText.text = pointsAmount.ToString();
				}
			}
			else 
			{
				lastSkid = -1;
			}
		}
		else 
		{
			
			lastSkid = -1;
		}
	}
}

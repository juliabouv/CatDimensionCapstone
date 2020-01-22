using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// Transform of the GameObject you want to shake
	public Transform shakeObject;

	// Desired duration of the shake effect
	private float shakeDuration = 0f;

	// A measure of magnitude for the shake. Tweak based on your preference
	private float shakeMagnitude = 0.1f;

	// A measure of how quickly the shake effect should evaporate
	private float dampingSpeed = 1.0f;

	// The initial position of the GameObject
	Vector3 initialPosition;

	void Awake()
	{
		if (shakeObject == null)
		{
			shakeObject = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		initialPosition = shakeObject.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			shakeObject.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

			shakeDuration -= Time.deltaTime * dampingSpeed;
		}
		else
		{
			shakeDuration = 0f;
			shakeObject.localPosition = initialPosition;
		}
	}

	public void TriggerShake()
	{
		shakeDuration = 0.3f;
	}
}

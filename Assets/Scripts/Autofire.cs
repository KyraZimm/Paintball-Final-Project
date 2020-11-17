using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autofire : MonoBehaviour
{
	public float radius;
	public string enemyOrFriend;
	GameObject target;
	[SerializeField] FieldOfView fov;
	void CheckTargetInRange()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyOrFriend);
		foreach(GameObject target in targets)
		{
			float distToTarget = Vector2.Distance(transform.position, target.transform.position);
			if (distToTarget < radius)
			{
				//if ()
			}
		}
		
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		fov.SetOrigin(transform.position);
		fov.SetAimDirection(transform.eulerAngles);
	}
}

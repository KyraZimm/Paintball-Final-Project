using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autofire : MonoBehaviour
{
	[SerializeField] GameObject fov;
	[SerializeField] LayerMask layerMask;
	[SerializeField] float hp;
	[SerializeField] float damage;
	[SerializeField] float accuracy;
	[SerializeField] float cover;
	Autofire damageSource;
	bool isDead;
	public bool isFiring;

	public void Fire(Vector2 point, GameObject target)
	{
		isFiring = true;
		var dir = new Vector3(point.x, point.y, 0) - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		// target receives damage
		DealDamage(target.GetComponent<Autofire>());
	}

	public void DealDamage(Autofire target)
	{
		target.ReceiveDamage(damage * accuracy *Time.deltaTime, this);

	}
	public void ReceiveDamage(float damage, Autofire source)
	{
		hp -= damage * (1 - cover);
		damageSource = source;
		if (hp <= 0)
		{
			isDead = true;
			Destroy(fov);
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		fov = Instantiate(fov);
		fov.GetComponent<FieldOfView>().layerMask = layerMask;
		fov.GetComponent<FieldOfView>().autofire = this;


	}

	// Update is called once per frame
	void Update()
	{
		fov.GetComponent<FieldOfView>().SetOrigin(transform.position);
		fov.GetComponent<FieldOfView>().SetAimDirection(transform.eulerAngles);
		Debug.Log(isFiring);
	}
}

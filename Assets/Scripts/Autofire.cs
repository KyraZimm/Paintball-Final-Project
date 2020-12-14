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

	public float visibleTime;
	
	//paintball asset - Kyra
	public GameObject paintball;
	
	public void SetVisible()
	{
		visibleTime = 3f;
	}
	public void Fire(Vector2 point, GameObject target)
	{
		isFiring = true;
		var dir = new Vector3(point.x, point.y, 0) - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		// target receives damage
		DealDamage(target.GetComponent<Autofire>());
	}
	
	//Kyra:
	public void PaintballTrail(GameObject target, bool isPlayerFiring)
	{
		GameObject newPaintball = Instantiate(paintball, gameObject.transform.position,
			Quaternion.identity);
		newPaintball.GetComponent<PaintballBehaviour>().target = target;
		newPaintball.GetComponent<PaintballBehaviour>().isPlayerFiring = isPlayerFiring;
	}

	public void DealDamage(Autofire target)
	{
		target.ReceiveDamage(damage * accuracy * Time.deltaTime, this);

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
		if (tag == "Enemy")
		{

			fov.GetComponent<FieldOfView>().fov = 70f;
			fov.GetComponent<MeshRenderer>().enabled = false;
			fov.layer = LayerMask.NameToLayer("BehindMask");
		}
	}

	// Update is called once per frame
	void Update()
	{
		fov.GetComponent<FieldOfView>().SetOrigin(transform.position);
		fov.GetComponent<FieldOfView>().SetAimDirection(transform.eulerAngles);
		//Debug.Log(isFiring);

		// masking
		if (gameObject.tag == "Enemy")
		{
			if (visibleTime > 0)
			{
				gameObject.layer = LayerMask.NameToLayer("Enemy"); // set to visible
				visibleTime -= Time.deltaTime;
			}
			else
			{
				gameObject.layer = LayerMask.NameToLayer("BehindMask"); // set to invisible
			}
		}
	}
}

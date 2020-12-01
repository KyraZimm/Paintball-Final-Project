using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public LayerMask layerMask;
	public Autofire autofire;
	Mesh mesh;
	float startingAngle;
	float fov;
	Vector3 origin;
	Vector3 GetVectorFromAngle(float angle)
	{
		// angle = 0 -> 360
		float angleRad = angle * (Mathf.PI / 180f);
		return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
	}
	float GetAngleFromVectorFloat(Vector3 dir)
	{
		dir = dir.normalized;
		float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		if (n < 0)
		{
			n += 360;
		}
		return n;
	}
	void CreateFieldOfView()
	{
		
		float viewDistance = 5f;
		int rayCount = 10;
		float angle = startingAngle;
		
		float angleIncrease = fov / rayCount;

		Vector3[] vertices = new Vector3[rayCount + 1 + 1];
		Vector2[] uv = new Vector2[vertices.Length];
		int[] triangles = new int[rayCount * 3];

		vertices[0] = origin;

		int vertexIndex = 1;
		int triangleIndex = 0;

		bool hit = false;
		for (int i = 0; i <= rayCount; i++)
		{
			Vector3 vertex;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
			
			if (raycastHit2D.collider == null)
			{
				// no hit
				vertex = origin + GetVectorFromAngle(angle) * viewDistance;
				Debug.DrawLine(origin, raycastHit2D.point);
			}
			else
			{
				// hit object
				Debug.DrawLine(origin, raycastHit2D.point);
				vertex = raycastHit2D.point;
				if (autofire.gameObject.tag.Equals("Player"))
				{
					if (raycastHit2D.collider.gameObject.tag.Equals("Enemy"))
					{
						if (!hit)
						{
							hit = true;
						}
						autofire.Fire(vertex, raycastHit2D.collider.gameObject);
					}
				}
				if (autofire.gameObject.tag.Equals("Enemy"))
				{
					if (raycastHit2D.collider.gameObject.tag.Equals("Player"))
					{
						if (!hit)
						{
							hit = true;
						}
						autofire.Fire(vertex, raycastHit2D.collider.gameObject);
					}
				}

			}

			vertices[vertexIndex] = vertex;

			if (i > 0)
			{
				triangles[triangleIndex + 0] = 0;
				triangles[triangleIndex + 1] = vertexIndex - 1;
				triangles[triangleIndex + 2] = vertexIndex;

				triangleIndex += 3;

			}
			vertexIndex++;
			angle -= angleIncrease;
		}

		autofire.isFiring = hit;

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}
	public void SetOrigin(Vector3 origin)
	{
		this.origin = origin;

	}
	public void SetAimDirection(Vector3 aimDirection)
	{
		startingAngle = aimDirection.z - fov / 2f + 180;
		//Debug.Log(startingAngle);

	}
	// Start is called before the first frame update
	void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		fov = 90f;
		Vector3 origin = transform.position;
	}

	// Update is called once per frame
	void LateUpdate()
	{

		CreateFieldOfView();
	}
}

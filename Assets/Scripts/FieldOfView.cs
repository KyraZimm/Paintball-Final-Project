using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

	Mesh mesh;
	Vector3 GetVectorFromAngle(float angle)
	{
		// angle = 0 -> 360
		float angleRad = angle * (Mathf.PI / 180f);
		return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
	}
	void CreateFieldOfView()
	{
		float fov = 90f;
		float viewDistance = 50f;
		int rayCount = 20;
		float angle = 0f;
		Vector3 origin = transform.position;
		float angleIncrease = fov / rayCount;

		Vector3[] vertices = new Vector3[rayCount + 1 + 1];
		Vector2[] uv = new Vector2[vertices.Length];
		int[] triangles = new int[rayCount * 3];

		vertices[0] = origin;

		int vertexIndex = 1;
		int triangleIndex = 0;


		for (int i = 0; i <= rayCount; i++)
		{
			Vector3 vertex;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);
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

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}
	// Start is called before the first frame update
	void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	// Update is called once per frame
	void Update()
	{

		CreateFieldOfView();
	}
}

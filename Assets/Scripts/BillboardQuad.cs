using UnityEngine;

public class BillboardQuad : MonoBehaviour
{
	private void Update()
	{
		transform.LookAt(Camera.main.transform);
	}
}
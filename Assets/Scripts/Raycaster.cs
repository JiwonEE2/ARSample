using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Raycaster : MonoBehaviour
{
	public ARRaycastManager raycastManager;
	public GameObject spherePrefab;

	public void Raycast()
	{
		Transform camTrans = Camera.main.transform;
		Ray ray = new Ray(camTrans.position, camTrans.forward);

		List<ARRaycastHit> hits = new List<ARRaycastHit>();

		// Ray에 부딪힌 객체가 있을 때 true
		if (raycastManager.Raycast(ray, hits))
		{
			Pose pose = hits[0].pose;

			Instantiate(spherePrefab, pose.position, pose.rotation);
		}
	}
}
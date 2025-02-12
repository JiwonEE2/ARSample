using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
	public ARTrackedImageManager imageManager;

	// Image library에 있는 이미지의 name과 동일한 이름을 가진 프리팹을 생성
	public List<GameObject> prefabs;

	private void OnEnable()
	{
		imageManager.trackedImagesChanged += OnImageChange;
	}

	private void OnDisable()
	{
		imageManager.trackedImagesChanged -= OnImageChange;
	}

	private void OnImageChange(ARTrackedImagesChangedEventArgs args)
	{
		// 새로 발견된 이미지
		foreach (ARTrackedImage img in args.added)
		{
			string name = img.referenceImage.name;
			GameObject targetPrefab = prefabs.Find((x) => x.name == name);
			Instantiate(targetPrefab, img.transform, false);
		}

		// 카메라 내에서 움직이는 등 변경사항이 있는 이미지
		foreach (ARTrackedImage img in args.updated)
		{
			img.transform.GetChild(0)
				.SetPositionAndRotation(img.transform.position, img.transform.rotation);
			;
		}

		// 카메라에서 사라진 이미지
		foreach (ARTrackedImage img in args.removed)
		{
			Destroy(img.transform.GetChild(0));
		}
	}
}
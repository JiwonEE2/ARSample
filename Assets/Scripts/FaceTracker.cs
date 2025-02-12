using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceTracker : MonoBehaviour
{
	public ARFaceManager faceManager;
	public GameObject pointPrefab;
	public TextMeshProUGUI text;

	private Transform[] facePoints = new Transform[468];

	private void Awake()
	{
		for (int i = 0; i < facePoints.Length; i++)
		{
			facePoints[i] = Instantiate(pointPrefab).transform;
		}
	}

	private void OnEnable()
	{
		faceManager.facesChanged += OnFacesChanged;
	}

	private void OnDisable()
	{
		faceManager.facesChanged -= OnFacesChanged;
	}

	private void OnFacesChanged(ARFacesChangedEventArgs args)
	{
		// 새로운 얼굴 등장
		if (args.added.Count > 0)
		{
			text.text = "얼굴 등장!!";
		}
		else if (args.removed.Count > 0)
		{
			text.text = "얼굴 퇴장..";
		}

		// 얼굴에 움직임이 있을 경우
		if (args.updated.Count > 0)
		{
			ARFace face = args.updated[0];

			// 증강 얼굴의 정점들(Vertices)를 차례로 돌며 참조
			// 총 468개의 정점이 생성됨
			for (int i = 0; i < face.vertices.Length /*468*/; i++)
			{
				// vertices의 좌표는 face의 중앙에 대한 상대좌표이므로, 얼굴 위치 기준을 월드 좌표로 환산
				Vector3 vertPos = face.transform.TransformPoint(face.vertices[i]);
				facePoints[i].position = vertPos;
			}
		}
	}
}
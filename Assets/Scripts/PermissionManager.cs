using UnityEngine;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
	public GPSManager gps;

	private void Start()
	{
		// 요청할 권한
		// string permissionName = "ACCESS_FINE_LOCATION";
		string permissionName = Permission.FineLocation;

		// 이 앱이 권한이 승인된 상태인 지 확인
		if (Permission.HasUserAuthorizedPermission(permissionName))
		{
			// 이미 권한을 가지고 있는 상태
		}
		else
		{
			// 권한이 없으므로 요청해야 함

			// 권한 요청 후 콜백을 어떻게 받을 것인 지 콜백 객체를 생성
			PermissionCallbacks callbacks = new PermissionCallbacks();

			// 접근이 거부되었을 때 호출될 함수
			callbacks.PermissionDenied += OnDenied;
			// 접근이 허용되었을 때 호출될 함수
			callbacks.PermissionGranted += OnGranted;

			// 권한 요청
			Permission.RequestUserPermission(permissionName, callbacks);
		}
	}

	// 권한 요청이 승인되었을 때 호출될 함수
	private void OnGranted(string msg)
	{
		print($"요청 승인됨: {msg}");
		gps.GPSOn();
	}

	// 권한 요청이 거부되었을 때 호출될 함수
	private void OnDenied(string msg)
	{
		print($"요청 거부됨: {msg}");
		gps.GPSOff();
	}
}
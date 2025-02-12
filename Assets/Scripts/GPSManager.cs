using System.Collections;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
	// GPS 재요청 간격
	public float delayTime = 1f;
	private Coroutine gpsCoroutine;

	public void GPSOn()
	{
		if (gpsCoroutine != null) return;
		gpsCoroutine = StartCoroutine("GPSCoroutine");
	}

	public void GPSOff()
	{
		if (gpsCoroutine == null) return;
		StopCoroutine(gpsCoroutine);
		gpsCoroutine = null;
	}

	IEnumerator GPSCoroutine()
	{
		// 만약 동적으로 간격이 변해야 할 경우에는
		// yield return 시 마다 yieldInstruction 객체를 생성할 수 밖에 없다.
		// 그러나 만약 동적으로 간격이 변해야 할 필요는 없고
		// 코루틴 시작 시부터 고정된 간격으로만 대기할 경우
		// 객체를 재활용할 수 있다.
		// 객체 생성 시점의 delayTime만큼 대기하는 WaitForSeconds 객체
		WaitForSeconds delay = new WaitForSeconds(delayTime);
		// while (true)
		// {
		// 	yield return delay;
		// }

		// 유니티에서 GPS를 사용하려면 레거시 입력인 Input 클래스를 활용해야 함
		// 사용자가 GPS를 꺼 둔 상태
		if (false == Input.location.isEnabledByUser)
		{
			gpsCoroutine = null;
			yield break;
		}

		// GPS 사용 시작
		Input.location.Start();
		print("GPS Start");

		int waitCount = 20;
		bool gpsRunning = false;
		while (false == gpsRunning)
		{
			switch (Input.location.status)
			{
				// GPS 초기화 중
				case LocationServiceStatus.Initializing:
					print($"Wait for GPS Initialize. Countdown: {waitCount}");
					waitCount--;
					yield return delay;

					if (waitCount <= 0)
					{
						print("GPS가 응답하지 않음.");
						gpsCoroutine = null;
						yield break;
					}

					break;

				// GPS가 동작함
				case LocationServiceStatus.Running:
					print("GPS is Running.");
					gpsRunning = true;
					break;

				// (이유는 모르겠으나) GPS 초기화 실패
				case LocationServiceStatus.Failed:
					print("GPS Failed.");
					Input.location.Stop();
					gpsCoroutine = null;
					yield break;
			}
		}

// GPS 실행되는 동안 반복 수행할 루프
		while (Input.location.status == LocationServiceStatus.Running)
		{
			LocationInfo info = Input.location.lastData;
			print($"위도: {info.latitude}, 경도: {info.longitude}");
			yield return delay;
		}

		gpsCoroutine = null;
	}
}
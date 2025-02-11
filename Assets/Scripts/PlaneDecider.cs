using UnityEngine;

public class PlaneDecider : MonoBehaviour
{
	public Material outlineMat;
	public Material planeMat;
	private bool isOn = true;

	// isOn을 반대로 전환하면서, true/false 여부에 따라 머티리얼을 투명하거나 불투명하게 변경.
	public void Toggle()
	{
		isOn = !isOn;
		outlineMat.color = isOn ? Color.black : Color.clear;
		planeMat.color = isOn ? new Color(1, 1, 0, 0.25f) : Color.clear;
	}
}
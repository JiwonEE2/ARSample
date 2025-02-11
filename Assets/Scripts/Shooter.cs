using UnityEngine;

public class Shooter : MonoBehaviour
{
	public GameObject projectilePrefab;
	public float shootPower = 20;

	public void Shoot()
	{
		Transform camTransform = Camera.main.transform;
		GameObject projectile = Instantiate(projectilePrefab, camTransform.position,
			camTransform.rotation);
		projectile.GetComponent<Rigidbody>()
			.AddForce(camTransform.forward * shootPower, ForceMode.Impulse);
	}
}
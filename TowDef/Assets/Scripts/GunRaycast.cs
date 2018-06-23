using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : MonoBehaviour {

	[Header("Gun Stats")]

	[SerializeField]
	private int gunDamage = 1;
	[SerializeField]
	private float fireRate = 0.25f;
	[SerializeField]
	private float weaponRange = 50f;
	[SerializeField]
	private float hitForce = 100f;
	[SerializeField]
	private Transform gunEnd;

	public LayerMask ignoreLayer;

	private Camera fpsCam;
	private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
	private AudioSource gunAudio;
	private LineRenderer laserLine;
	private float nextFire;

	void Start () {
		laserLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		fpsCam = GetComponentInParent<Camera>();
	}
	
	void Update () {
		
		if(Input.GetButtonDown("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			StartCoroutine(ShotEffect());

			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;

			laserLine.SetPosition(0, gunEnd.position);

			if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, ~ignoreLayer)) {
				laserLine.SetPosition(1, hit.point);

				// detect if we can damage the object
				Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

				if(enemy != null) {
					enemy.TakeDamage(gunDamage);
				}

				/* if(hit.rigidbody != null) {
					hit.rigidbody.AddForce(-hit.normal * hitForce);
				} */

			} else {
				laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
			}
		}

		// little extra so it looks a little bit nicer (my opinion)
		if(laserLine.enabled) {
			laserLine.SetPosition(0, gunEnd.position);
		}

	}

	private IEnumerator ShotEffect() {
		gunAudio.Play();

		laserLine.enabled = true;
		yield return shotDuration;
		laserLine.enabled = false;
	}

}

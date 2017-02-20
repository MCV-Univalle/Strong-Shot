using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RayCastShoot : MonoBehaviour {

	public int gunDamage = 1;
	public float fireRate = 0.25f;
	public float weaponRange = 50f;
	public float hitForce = 100;
	public Transform gunEnd;
	public Color color = Color.white;

	List<Color> ColorList = new List<Color> ();

	public int CurrentColor = 0;


	private Camera fpsCam;
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
	private AudioSource gunAudio;
	private LineRenderer laserLine;
	private Image colorSelector;
	private float nextFire;




	void Start () 
	{
		laserLine = GetComponent<LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		fpsCam = GetComponentInParent<Camera> ();
		colorSelector = GameObject.Find ("ColorSelector").GetComponent<Image> ();


		ColorList.Add (Color.red);
		ColorList.Add (Color.green);
		ColorList.Add (Color.blue);
		//ColorList.Add (Color.black);
		//ColorList.Add (Color.white);
	}

	void Update () 
	{
		colorSelector.color = color; 
		color = ColorList [CurrentColor];

		if(Input.GetButton("Fire1") && Time.time > nextFire)
		{
			
			nextFire = Time.time + fireRate;

			StartCoroutine (ShotEffect ());

			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0));
			RaycastHit hit;

			laserLine.SetPosition (0, gunEnd.position);





			if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) {
				laserLine.SetPosition (1, hit.point);
				ShootableBox shootableBox = hit.collider.GetComponent<ShootableBox> ();

				if (shootableBox != null) {
					shootableBox.Damage (gunDamage);
					shootableBox.Colored (color);
				}

				if (hit.rigidbody != null) {
					//hit.rigidbody.AddForce(-hit.normal * hitForce);
				}

			} 
			else 
			{
				laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
			}
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			CurrentColor = CurrentColor + 1;
			if(CurrentColor >= 3){
				CurrentColor = 0;
			}
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			CurrentColor = CurrentColor - 1;
			if(CurrentColor <= -1){
				CurrentColor = 2;
			}
		}
	}

	private IEnumerator ShotEffect(){
		gunAudio.Play ();

		laserLine.enabled = true;
		yield return shotDuration;
		laserLine.enabled = false;
	}
}

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

	public int Current = 0;
    public int Next = 1;
    public int Previous = 2;


    private Camera fpsCam;
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
	private AudioSource gunAudio;
	private LineRenderer laserLine;
	private Image colorSelector;
    private Image NextColor;
    private Image PreviousColor;
    private float nextFire;




	void Start () 
	{
		laserLine = GetComponent<LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		fpsCam = GetComponentInParent<Camera> ();
		colorSelector = GameObject.Find ("ColorSelector").GetComponent<Image> ();
        NextColor = GameObject.Find("NextColor").GetComponent<Image>();
        PreviousColor = GameObject.Find("PreviousColor").GetComponent<Image>();


        ColorList.Add (Color.red);
		//ColorList.Add (Color.green);
		ColorList.Add(Color.yellow);
		ColorList.Add (Color.blue);
		//ColorList.Add (Color.black);
		//ColorList.Add (Color.white);
	}

	void Update () 
	{
		colorSelector.color = ColorList[Current];
        NextColor.color = ColorList[Next];
        PreviousColor.color = ColorList[Previous];

        if (Input.GetButton("Fire1") && Time.time > nextFire)
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
                    if (shootableBox.isColor(ColorList[Current])) {
                        shootableBox.Damage(gunDamage);
                        //shootableBox.Colored(color);
                    }
                    shootableBox.Colored(ColorList[Current]);
                }

				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce(-hit.normal * hitForce);
				}

			} 
			else 
			{
				laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
			}
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Current = Current + 1;
            Previous = Previous + 1;
            Next = Next + 1;

            if (Current >= 3){
				Current = 0;
			}
            if (Previous >= 3)
            {
                Previous = 0;
            }
            if (Next >= 3)
            {
                Next = 0;
            }
        }

		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
            Current = Current - 1;
            Previous = Previous - 1;
            Next = Next - 1;

            if (Current <= -1){
				Current = 2;
			}
            if (Previous <= -1)
            {
                Previous = 2;
            }
            if (Next <= -1)
            {
                Next = 2;
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

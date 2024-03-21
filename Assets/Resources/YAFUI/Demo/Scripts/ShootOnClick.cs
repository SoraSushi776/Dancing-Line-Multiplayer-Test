/*
This script is will shoot a Ray on click.
If they Ray hits the shield it will trigger an effect to occur.
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootOnClick : MonoBehaviour 
{

	//text for the buttons
	public Text modeText;
	public Text PresetId;

	//shooting modes...
	public enum modes
	{
		ray,projectile
	}
	//the current mode
	public modes mode = modes.ray;

	//the projectile that will be used
	public GameObject projectile;

	//the force of the projectile
	public float force = 500f;


	//method used to change the shotting mode
	public void ChangeMode()
	{

        if (modeText == null)
        {
            return;
        }

        if (mode == modes.ray)
		{
			mode = modes.projectile;
			modeText.text = "Mode: Projectile";
		}
		else 
		{
			mode = modes.ray;
			modeText.text = "Mode: Ray";
		}
	}

	//set correct text on Awake
	void Awake()
	{
        if (modeText == null)
        {
            return;
        }

		if (mode == modes.ray)
		{
			modeText.text = "Mode: Ray";
		}
		else 
		{
			modeText.text = "Mode: Projectile";
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            mode = modes.projectile;
        }
        else
        {
            mode = modes.ray;
        }

		//On Click
		if (Input.GetMouseButtonDown(0))
		{

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//if ray mode
		if (mode == modes.ray)
		{
			
			RaycastHit RCH;

			if (Physics.Raycast (ray,out RCH, 1000)) 
			{
                
                /*
				EnergyShieldManager ESM = RCH.collider.gameObject.GetComponent<EnergyShieldManager>();

				//if the collider has an EnergyShieldManager.cs on it.
				if (ESM != null)
				{
					ESM.Hit(RCH.point); //hit the shield
				}
                */
			}
		}
		else //if projectile mode
		{
				
			Transform t = Camera.main.transform;
			GameObject p =  Instantiate(projectile,t.position,t.rotation) as GameObject;
			p.GetComponent<Rigidbody>().AddForce(ray.direction * force);
		}


		}
	
	}
}

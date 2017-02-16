using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 3;
	public int id = 0;

	List<Color> ColorList = new List<Color> ();

	void start(){
		ColorList.Add(Color.red);
		//ColorList.Add (Color.green);
		ColorList.Add(Color.yellow);
		ColorList.Add(Color.blue);
	}
	public void Damage(int damageAmount)
	{
		//subtract damage amount when Damage function is called

		currentHealth -= damageAmount;


		//Check if health has fallen below zero
		if (currentHealth <= 0) 
		{
			//if health has fallen below zero, deactivate it 
			gameObject.SetActive (false);
		}
	}

	public void Colored(Color color){
		Renderer rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Specular");
		rend.material.SetColor("_SpecColor", color);
	}
}

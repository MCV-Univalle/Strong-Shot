using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootableBox : MonoBehaviour {

	//The box's current health point total
	public int currentHealth = 3;
    Color color = Color.red;

    void start(){
        
    }

	public void Damage(int gunDamage)
	{
		//subtract damage amount when Damage function is called

		currentHealth -= 1;


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

    public bool isColor(Color colorAux){
        if (colorAux == color)
        {
            return true;
        }else {
            return false;
        }
    }
}

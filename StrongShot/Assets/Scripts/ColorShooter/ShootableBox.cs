using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootableBox : MonoBehaviour {
    public int id = 0;
	//The box's current health point total
	public int currentHealth = 1;
    List<Color> ColorList = new List<Color>();

    void start(){
        ColorList.Add(Color.red);
        ColorList.Add(Color.yellow);
        ColorList.Add(Color.blue);
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
        switch (id){
            case 0:
                if (colorAux.Equals(Color.red)){
                    return true;
                }else{
                    return false;
                }
            case 1:
                if (colorAux.Equals(Color.blue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (colorAux.Equals(Color.yellow))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
}

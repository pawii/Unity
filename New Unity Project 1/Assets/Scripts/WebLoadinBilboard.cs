using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadinBilboard : MonoBehaviour {

	private void OnWebImage(Texture2D image)
	{
		GetComponent<Renderer>().material.mainTexture = image;
	}

	public void Operate()
	{
		Managers.Images.GetWebImage(OnWebImage);
	}

	void Start()
	{
		//OnWebImage(Resources.Load<Texture2D>("321"));
		//Operate();
	}
}

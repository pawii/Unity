using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkService : MonoBehaviour {

	private const string xmlApi = 
		"http://api.openweathermap.org/data/2.5/" +
		"find?q=Minsk&units=imperial&type=accurate&mode=xml&APPID=c73c0c8366f8c9ea5f9f6f3672538c25";

	private const string webImage = "http://gyanendushekhar.com/wp-content/uploads/2017/07/SampleImage.png";

	private bool IsResponseValid(WWW www)
	{
		if (www.error != null)
		{
			Debug.Log("Bad connection");
			return false;
		}
		if (string.IsNullOrEmpty(www.text))
		{
			Debug.Log("Bad data");
			return false;
		}
		return true;
	}

	private IEnumerator CallAPI(string url, Action<string> callback)
	{
		WWW www = new WWW(url);
		yield return www;

		if (!IsResponseValid(www))
			yield break;

		callback(www.text);
	}

	public IEnumerator GetWeatherXML(Action<string> callback)
	{
		return CallAPI(xmlApi, callback);
	}

	public IEnumerator DownloadImage(Action<Texture2D> callback)
	{
		WWW www = new WWW(webImage);
		yield return www;
		callback(www.texture);
	}
}

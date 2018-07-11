using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class WeatherManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }

	private NetworkService _network;

	public float cloudValue { get; private set; }

	public void StartUp(NetworkService service)
	{

		Debug.Log("Weather manager starting...");
		_network = service;
		StartCoroutine(service.GetWeatherXML(OnXmlDataLoad));

		status = ManagerStatus.Initializing;
	}

	public void OnXmlDataLoad(string data)
	{
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(data);
		XmlNode root = doc.DocumentElement;

		XmlNode node = root.SelectSingleNode("list").
		                   SelectSingleNode("item").SelectSingleNode("clouds");
		string value = node.Attributes["value"].Value;
		cloudValue = Int32.Parse(value) / 100f;
		Debug.Log("Weather: " + value);

		Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

		status = ManagerStatus.Started;
	}
}

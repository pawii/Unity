using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SttingsPopup : MonoBehaviour {

	[SerializeField]
	private Slider slider;
	[SerializeField]
	private AudioClip sound;

	public void OnSubmitName(string name) 
	{
		Debug.Log(name);
	}

	public void OnSpeedValue(float speed)
	{
		Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
	}

	public void OnSoundToggle()
	{
		Managers.Audio.soundMute = !Managers.Audio.soundMute;
		Managers.Audio.PlaySound(sound);
	}

	public void OnSoundValue(float volume)
	{
		Managers.Audio.soundVolume = volume;
	}

	public void OnPlayMusic(int selector)
	{
		Managers.Audio.PlaySound(sound);

		switch (selector)
		{
			case 1:
				Managers.Audio.PlayIntroMusic();
				break;
			case 2:
				Managers.Audio.PlayLevelMusic();
				break;
			default:
				Managers.Audio.StopMusic();
				break;
		}
	}

	public void OnMusicToggle()
	{
		Managers.Audio.musicMute = !Managers.Audio.musicMute;
		Managers.Audio.PlaySound(sound);
	}

	public void OnMusicValue(float value)
	{
		Managers.Audio.musicVolume = value;
	}
}
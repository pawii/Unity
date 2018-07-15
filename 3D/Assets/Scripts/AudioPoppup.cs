using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoppup : MonoBehaviour 
{
	[SerializeField]
	AudioClip buttonClickClip;

	bool muteSound = false;
	bool muteMusic = false;

	public void OnMuteSound()
	{
		muteSound = !muteSound;
		Managers.Audio.soundMute = muteSound;
		Managers.Audio.PlaySound(buttonClickClip);
	}

	public void OnSoundVolume(float volume)
	{
		Managers.Audio.soundVolume = volume;
	}

	public void OnMuteMusic()
	{
		muteMusic = !muteMusic;
		Managers.Audio.musicMute = muteMusic;
		Managers.Audio.PlaySound(buttonClickClip);
	}

	public void OnMusicVolume(float volume)
	{
		Managers.Audio.musicVolume = volume;	}

	public void OnPlayLoop()
	{
		Managers.Audio.PlayLoop();
		Managers.Audio.PlaySound(buttonClickClip);
	}

	public void OnPlayIntro()
	{
		Managers.Audio.PlayIntro();
		Managers.Audio.PlaySound(buttonClickClip);
	}
}

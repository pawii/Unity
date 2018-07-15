using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager 
{
	public ManagerStatus status { get; private set; }
	[SerializeField]
	AudioSource soundSource;
	[SerializeField]
	AudioSource musicSource1;
	[SerializeField]
	AudioSource musicSource2;

	AudioSource activeMusic;
	AudioSource inactiveMusic;

	public float crossFadeRate = 1.5f;
	private bool _crossFading;

	NetworkService _network;

	private float _musicVolume;
	private bool _musicMute = false;

	public float soundVolume
	{
		get { return AudioListener.volume; }
		set { AudioListener.volume = value; }
	}

	public bool soundMute
	{
		get { return AudioListener.pause; }
		set { AudioListener.pause = value; }
	}

	public float musicVolume
	{
		get { return _musicVolume; }
		set
		{
			_musicVolume = value;
			musicSource1.volume = _musicVolume;
			musicSource2.volume = _musicVolume;
		}
	}

	public bool musicMute
	{
		get { return _musicMute; }
		set
		{
			_musicMute = value;
			musicSource1.mute = _musicMute;
			musicSource2.mute = _musicMute;
		}
	}

	public void StartUp(NetworkService network)
	{
		status = ManagerStatus.Initializing;

		_network = network;

		activeMusic = musicSource1;
		inactiveMusic = musicSource2;
		activeMusic.ignoreListenerPause = true;
		activeMusic.ignoreListenerVolume = true;
		inactiveMusic.ignoreListenerPause = true;
		inactiveMusic.ignoreListenerVolume = true;

		_crossFading = false;

		_musicVolume = 1f;
		soundVolume = 1f;

		status = ManagerStatus.Started;
	}

	public void PlaySound(AudioClip sound)
	{
		soundSource.PlayOneShot(sound);
	}

	private void PlayMusic(AudioClip music)
	{
		if (_crossFading)
			return;
		StartCoroutine(CrossFadeMusic(music));
	}

	private IEnumerator CrossFadeMusic(AudioClip music)
	{
		_crossFading = true;

		inactiveMusic.clip = music;
		inactiveMusic.volume = 0;
		inactiveMusic.Play();

		float scaladRate = crossFadeRate * _musicVolume;

		while (activeMusic.volume > 0)
		{
			activeMusic.volume -= scaladRate * Time.deltaTime;
			inactiveMusic.volume += scaladRate * Time.deltaTime;
			yield return null;
		}

		AudioSource temp = activeMusic;
		activeMusic = inactiveMusic;
		inactiveMusic = temp;

		inactiveMusic.Stop();
		activeMusic.volume = _musicVolume;

		_crossFading = false;
	}

	public void PlayLoop()
	{
		PlayMusic(Resources.Load<AudioClip>("music/loop"));
	}

	public void PlayIntro()
	{
		PlayMusic(Resources.Load<AudioClip>("music/intro"));	}
}

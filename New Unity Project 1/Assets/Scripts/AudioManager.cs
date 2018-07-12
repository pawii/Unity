using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
	[SerializeField]
	private AudioSource soundSource;

	[SerializeField]
	private AudioSource music1Source;
	[SerializeField]
	private AudioSource music2Source;
	[SerializeField]
	private string introBGMusic;
	[SerializeField]
	private string levelBGMusic;

	private AudioSource _activeMusic;
	private AudioSource _inactiveMusic;

	public float crossFadeRate = 1.5f;
	private bool _crossFading;

	private float _musicVolume;

	public ManagerStatus status {get; private set;}

	private NetworkService _network;

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

			if (music1Source != null)
			{ 
				music1Source.volume = _musicVolume;
				music2Source.volume = _musicVolume;
			}
		}
	}

	public bool musicMute
	{
		get
		{
			if (music1Source != null)
				return music1Source.mute;
			return false;
		}
		set
		{			if (music1Source != null)
			{
				music1Source.mute = value;
				music2Source.mute = value;
			}
		}
	}

	public void StartUp(NetworkService network)
	{
		status = ManagerStatus.Initializing;
		Debug.Log("Audio manager started...");

		_network = network;

		music1Source.ignoreListenerPause = true;
		music1Source.ignoreListenerVolume = true;

		soundVolume = 1f;
		_musicVolume = 1f;

		_activeMusic = music1Source;
		_inactiveMusic = music2Source;

		status = ManagerStatus.Started;
	}

	public void PlaySound(AudioClip clip)
	{
		soundSource.PlayOneShot(clip);
	}

	private void PlayMusic(AudioClip clip)
	{
		if (_crossFading)
			return;
		StartCoroutine(CrossFadeMusic(clip));
	}

	private IEnumerator CrossFadeMusic(AudioClip clip)
	{
		_crossFading = true;

		_inactiveMusic.clip = clip;
		_inactiveMusic.volume = 0;
		_inactiveMusic.Play();

		float scaledRate = crossFadeRate * _musicVolume;

		while (_activeMusic.volume > 0)
		{
			_activeMusic.volume -= scaledRate * Time.deltaTime;
			_inactiveMusic.volume += scaledRate * Time.deltaTime;

			yield return null;
		}

		AudioSource temp = _activeMusic;
		_activeMusic = _inactiveMusic;
		_activeMusic.volume = _musicVolume;

		_inactiveMusic = temp;
		_inactiveMusic.Stop();

		_crossFading = false;
	}

	public void PlayIntroMusic()
	{
		PlayMusic(Resources.Load<AudioClip>("Music/" + introBGMusic));
	}

	public void PlayLevelMusic()
	{
		PlayMusic(Resources.Load<AudioClip>("Music/" + levelBGMusic));
	}

	public void StopMusic()
	{
		_activeMusic.Stop();
		_inactiveMusic.Stop();
	}
}

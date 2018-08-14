using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
	bool fastSpeed { get; set; }
	bool isRun { get; set; }
	void OnKeyDown(KeyCode key);
	void Create();
}
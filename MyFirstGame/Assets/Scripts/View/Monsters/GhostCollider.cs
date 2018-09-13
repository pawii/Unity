using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour 
{
	int damage = -1;
	bool hitRequire;
	float delay = 0.5f;
	[SerializeField] Animator anim;

	private void Awake()
	{
		hitRequire = true;
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "shield")
		{
			anim.SetTrigger("GetBack");
		}
		
		if (collision.gameObject.tag == "character" && hitRequire)
		{
			hitRequire = false;
			MessageParameters parameters = new MessageParameters(Methods.GetDirection(gameObject), damage);
			GameController.character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
		}
	}

	private IEnumerable Delay()
	{
		yield return new WaitForSeconds(delay);
		hitRequire = true;
	}
}
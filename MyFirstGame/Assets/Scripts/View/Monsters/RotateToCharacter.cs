using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCharacter : MonoBehaviour
{
	private Transform triggerTarget;
	private float velocity;
	[SerializeField]
	private float damageArea = 8f;

	float offset;
	Vector3 newAngles;

	void Awake()
	{
		triggerTarget = GameController.character;
		velocity = (float)Mathf.Sqrt((float)damageArea * (float)Physics2D.gravity.magnitude);

		offset = transform.localEulerAngles.z;
		Debug.Log(velocity);
	}

	void LateUpdate()
	{
		float S = (float)triggerTarget.position.x - (float)transform.position.x;
		Debug.Log("S: " + S.ToString());
		if (Mathf.Abs(S) <= damageArea)
		{
			float angle = (float)((float)Mathf.Asin((float)S * (float)Physics2D.gravity.magnitude / (float)Mathf.Pow((float)velocity, 2f))) / 2f;
			if (angle < 0f)
				angle += (float)Mathf.PI;
			Debug.Log("angle: " + angle);

			float offsetAngle;
			Vector2 monsterToCharacter = triggerTarget.position - transform.position;
			//Debug.Log("monsterToCharacter: " + monsterToCharacter);
			if (monsterToCharacter.x >= 0 && monsterToCharacter.y >= 0)
			{
				offsetAngle = Methods.Angle(monsterToCharacter, new Vector2(1, 0));
				angle += offsetAngle;
			}
			if (monsterToCharacter.x >= 0 && monsterToCharacter.y <= 0)
			{
				offsetAngle = Methods.Angle(new Vector2(1, 0), monsterToCharacter);
				angle -= offsetAngle;
			}
			if (monsterToCharacter.x <= 0 && monsterToCharacter.y >= 0)
			{
				offsetAngle = Methods.Angle(monsterToCharacter, new Vector2(-1, 0));
				angle -= offsetAngle;
			}
			if (monsterToCharacter.x <= 0 && monsterToCharacter.y <= 0)
			{
				offsetAngle = Methods.Angle(monsterToCharacter, new Vector2(-1, 0));
				angle += offsetAngle;
			}

			//Debug.Log("angle + offset: " + angle);

			Vector2 directionPoint = new Vector2((float)Mathf.Cos((float)angle), (float)Mathf.Sin((float)angle));


			// ПОВОРОТ

			if(directionPoint.x < 0)
				directionPoint.x = directionPoint.x * -1;
			
			//Debug.Log("directionPoint: " + directionPoint);

			Vector3 newAngel = new Vector3(0, 0, 0);

			if(directionPoint.y >= 0)
				newAngel.z = (float)Vector2.Angle(new Vector2(1, 0), directionPoint);
			else
				newAngel.z = (float)-Vector2.Angle(new Vector2(1, 0), directionPoint);


			//Debug.Log("newAngel: " + newAngel.z);

			newAngel.z += offset;
			transform.localEulerAngles = newAngel;
		}
		else
		{
			//Vector3 newAngles = new Vector3();
			//newAngles.z = offset;
			//transform.localEulerAngles = newAngles; 
		}
	}
}

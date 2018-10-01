using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCharacter : MonoBehaviour
{
	private Transform triggerTarget;
	private float velocity;
	[SerializeField]
	private float damageArea = 8f;
    [SerializeField]
    private Transform arrowPos;

	float startOffset;

	void Awake()
	{
		triggerTarget = GameController.character;
		velocity = (float)Mathf.Sqrt((float)damageArea * (float)Physics2D.gravity.magnitude);

		startOffset = transform.localEulerAngles.z;
		Debug.Log(velocity);
	}

	void LateUpdate()
	{
		float S = triggerTarget.position.x - arrowPos.position.x;
        Vector3 toTarget = new Vector3(triggerTarget.position.x - arrowPos.position.x, 
            triggerTarget.position.y - arrowPos.position.y, 0);
		if (Mathf.Abs(toTarget.x) <= damageArea)
		{
            /*float angle = (float)((float)Mathf.Asin((float)S * (float)Physics2D.gravity.magnitude / (float)Mathf.Pow((float)velocity, 2f))) / 2f;
			if (angle < 0f)
				angle += (float)Mathf.PI;

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

            //Vector3 newAngel = Vector3.zero;
            //newAngel.z = Methods.RadToGrad(angle);
			newAngel.z += offset;
			transform.localEulerAngles = newAngel;*/

            Vector3 newAngel = Vector3.zero;

            // НАПРАВЛЯЕМ УГОЛ ПРЯМО НА ЦЕЛЬ
            newAngel.z = Vector3.Angle(new Vector3(1,0,0), toTarget);

            // РАССТАВЛЯЕМ ЗНАКИ
            if (toTarget.y < 0)
                newAngel.z = newAngel.z * -1;

            // ВЫЧИСЛЯЕМ ОФФСЕТ
            // ЕСЛИ toTarget.x = damageArea, ТО ОФФСЕТ = 45
            // ЕСЛИ toTarget.x = 0, ТО ОФФСЕТ = 0
            float offset = Mathf.Abs(toTarget.x) / damageArea * 45;

            // ОГРАНИЧИВАЕМ ОФФСЕТ
            // ЕСЛИ |УГОЛ|=90, ОФФСЕТ *= 0
            // ЕСЛИ |УГОЛ|=0, ОФФСЕТ *= 1
            offset = offset * (90 - Mathf.Abs(newAngel.z)) / 90;

            newAngel.z += offset;

            newAngel.z += startOffset;

            // ЕСЛИ toTarget.x < 0, ТО ЗЕРКАЛЬНО ОТРАЖАЕМ УГОЛ
            if (toTarget.x < 0)
                newAngel.z = newAngel.z * -1;

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

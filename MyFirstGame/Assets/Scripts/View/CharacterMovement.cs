using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class CharacterMovement : MonoBehaviour 
{
	[SerializeField]
	private float runSpeed = 5f;
	[SerializeField]
	private float walkSpeed = 2.5f;
	[SerializeField]
	private float jumpPower = 10f;
	[SerializeField]
	int getDamagePower = 5;

	[SerializeField]
	private Rigidbody2D rb;

    #region Unity lifecycle
    private void Awake()
	{
		CharacterController.Run += Run;
		CharacterController.Jump += Jump;
		CharacterController.GetDamage += GetDamage;
	}

    private void OnDestroy()
	{
		CharacterController.Run -= Run;
		CharacterController.Jump -= Jump;
		CharacterController.GetDamage -= GetDamage;
	}
    #endregion

    public void Run(bool isRun)
	{
        Vector3 pos = transform.position;
		Vector3 direction = transform.right * Input.GetAxis("Horizontal");

		float speed = isRun ? runSpeed : walkSpeed;
		transform.position = Vector2.MoveTowards(pos, pos + direction, 
		                                         speed * Time.deltaTime);
	}

	public void Jump()
	{
		Vector3 force = transform.up * jumpPower;
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	public void GetDamage(int direction)
	{
		Vector3 getDamageForce = new Vector3(0.1f, 1, 0);
		getDamageForce.x *= direction;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower* getDamageForce, ForceMode2D.Impulse);
		}
	}
}
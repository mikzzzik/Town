using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class InputSystem : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;

	public bool sprint;

	[Header("Movement Settings")]
	public bool analogMovement;

	public void MoveInput(InputAction.CallbackContext context)
	{
		

		move = context.ReadValue<Vector2>();
	}

	public void SprintInput(InputAction.CallbackContext context)
	{
		//Debug.Log(context.ReadValue<float>() == 1 ? true : false);
		sprint = context.ReadValue<float>() == 1 ? true : false;
	}

	public void LookInput(InputAction.CallbackContext context)
	{
		look = context.ReadValue<Vector2>();
	}
}

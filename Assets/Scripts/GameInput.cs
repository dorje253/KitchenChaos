using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    private PlayerInputActions playerInputActions;
    public void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
       OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
       inputVector = inputVector.normalized;

    //    Debug.Log(inputVector);
       return inputVector;
    }
}

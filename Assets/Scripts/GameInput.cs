using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInpurActions playerInpurActions;
    public void Awake(){
        playerInpurActions = new PlayerInpurActions();
        playerInpurActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInpurActions.Player.Move.ReadValue<Vector2>();
        
       inputVector = inputVector.normalized;

       Debug.Log(inputVector);
       return inputVector;
    }
}

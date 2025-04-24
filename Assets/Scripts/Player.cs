using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private void Update(){
       Vector2  inputVector = gameInput.GetMovementVectorNormalized();

       Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

       float moveDistance = moveSpeed * Time.deltaTime;
       float playerRadius = 0.7f;
       float playerHeight = 2f;
       bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerRadius, moveDir, moveDistance);
       
       if(!canMove){
        // attempt only x  movement
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerRadius, moveDirX, moveDistance);

        if(canMove){
            // Can move only on the X
            moveDir = moveDirX;
        }else{
            // can move only on the x
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,  playerRadius, moveDirZ, moveDistance);

            if(canMove){
                //can move only on the z
                moveDir = moveDirZ;
            }else{
                // cannot move in any direction
            }
        }
       }
       if(canMove){
        transform.position += moveDir * moveDistance;
       }
       

       isWalking = moveDir != Vector3.zero;
       float rotateSpeed = 10f;
       transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);

    }

    public bool IsWalking(){
        return isWalking;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnAnyPickedSomething;


    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }

    public static Player LocalInstance {get; private set;}

    public event EventHandler OnPickSomething;
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;
   
    [SerializeField] private LayerMask collisionsLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private List<Vector3> spawnPositionList;
    [SerializeField] private PlayerVisual playerVisual;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    
    

    private void Start(){
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(KitchenGameMultiplayer.Instance.GetPlayerColor(playerData.colorId));
    }
     private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e){
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }
        transform.position = spawnPositionList[KitchenGameMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];
        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        }
       
    }


    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == OwnerClientId && HasKitchenObject())
        {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }
    
    private void Update(){
        if (!IsOwner)
        {
            return;
        } 
       HandleMovement();
       HandleInteraction();

    }

    public bool IsWalking(){
        return isWalking;
    }

    private void HandleInteraction()
{
    Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    if (moveDir != Vector3.zero)
    {
        lastInteractDir = moveDir;
    }

    float interactDistance = 2.0f;

    if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // Has ClearCounter
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);

            }
        } else {
            SetSelectedCounter(null);
        }
}


    private void HandleMovement(){
        Vector2  inputVector = GameInput.Instance.GetMovementVectorNormalized();

       Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

       float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .6f;
       
       bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDir, Quaternion.identity, moveDistance, collisionsLayerMask);
       
       if(!canMove){
        // attempt only x  movement
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        canMove = (moveDir.x <-.5f || moveDir.x> +.5f) && !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirX, Quaternion.identity, moveDistance, collisionsLayerMask);

        if(canMove){
            // Can move only on the X
            moveDir = moveDirX;
        }else{
            // can move only on the x
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.BoxCast(transform.position, Vector3.one * playerRadius, moveDirZ, Quaternion.identity, moveDistance, collisionsLayerMask);

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

    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs{
                    selectedCounter = selectedCounter
                });
    }

    public Transform GetKitchenObjectFollowTransform(){
    return kitchenObjectHoldPoint;
    }
    
    
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
            OnAnyPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    
    public void ClearKitchenObject(){
        kitchenObject = null;
    }
    
    public bool HasKitchenObject(){
        return kitchenObject != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}

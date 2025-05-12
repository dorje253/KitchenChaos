using UnityEngine;
using Unity.Netcode;


public class KitchenObject : NetworkBehaviour
{
    [SerializeField] private KitchenObjectSO  kitchenObjectSO;
    private  IKitchenObjectParent KitchenObjectParent;

    private FollowTransform followTransform;


    protected virtual void Awake()
    {
        followTransform = GetComponent<FollowTransform>();
    }

    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObjectParent){

        SetKitchenObjectParentServerRpc(KitchenObjectParent.GetNetworkObject());
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetKitchenObjectParentServerRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    {
        SetKitchenObjectParentClientRpc(kitchenObjectParentNetworkObjectReference);
    }

    [ClientRpc]
    private void SetKitchenObjectParentClientRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    {
 

        kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
        IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

        this.KitchenObjectParent = kitchenObjectParent;


        if (KitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }

        KitchenObjectParent.SetKitchenObject(this);

        followTransform.SetTargetTransform(KitchenObjectParent.GetKitchenObjectFollowTransform());


    }

    public IKitchenObjectParent GetKitchenObjectParent(){
        return KitchenObjectParent;
    }

    public void DestroySelf(){
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject =null;
            return false;
        }
    }

    

    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent){
        KitchenGameMultiplayer.Instance.SpawnKitchenObject(kitchenObjectSO,  kitchenObjectParent);
    }
}

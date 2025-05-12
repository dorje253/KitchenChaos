using UnityEngine;
using Unity.Netcode;


public class KitchenObject : NetworkBehaviour
{
    [SerializeField] private KitchenObjectSO  kitchenObjectSO;
    private  IKitchenObjectParent KitchenObjectParent;


    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObjectParent){
        if(this.KitchenObjectParent != null){
            this.KitchenObjectParent.ClearKitchenObject();
        }
        this.KitchenObjectParent = KitchenObjectParent;

        if(KitchenObjectParent.HasKitchenObject()){
            Debug.LogError("IKitchenObjectParent alrady has a kitchenObject !");
        }
        
        KitchenObjectParent.SetKitchenObject(this);

        //transform.parent = KitchenObjectParent.GetKitchenObjectFollowTransform();
        //transform.localPosition = Vector3.zero;
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

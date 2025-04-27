using UnityEngine;

public class KitchenObject : MonoBehaviour
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

        transform.parent = KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent(){
        return KitchenObjectParent;
    }
}

using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{


   [SerializeField] private KitchenObjectSO kitchenObjectSo;
   [SerializeField] private Transform counterTopPoint;
  
   private KitchenObject kitchenObject;



public void Interact(Player player){
    if(kitchenObject == null){
        Transform kitchenObjectTransfrom = Instantiate(kitchenObjectSo.prefab, counterTopPoint);
        kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        kitchenObjectTransfrom.localPosition = Vector3.zero;
    }else{
        // kitchenObject.SetclearCounter(player);
        kitchenObject.SetKitchenObjectParent(player);
    }
    
}

public Transform GetKitchenObjectFollowTransform(){
    return counterTopPoint;
}

public void SetKitchenObject(KitchenObject kitchenObject){
    this.kitchenObject = kitchenObject;
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

}

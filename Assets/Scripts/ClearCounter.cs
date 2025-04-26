using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSo;
   [SerializeField] private Transform counterTopPoint;
   [SerializeField] private ClearCounter secondClearCounter;
   [SerializeField] private bool testing;

   private KitchenObject kitchenObject;

   private void Update(){
    if(testing && Input.GetKeyDown(KeyCode.T)){
        if(kitchenObject != null){
            kitchenObject.SetclearCounter(secondClearCounter);
        }
    }
   }

public void Interact(){
    if(kitchenObject == null){
        Transform kitchenObjectTransfrom = Instantiate(kitchenObjectSo.prefab, counterTopPoint);
        kitchenObjectTransfrom.GetComponent<KitchenObject>().SetclearCounter(this);
        kitchenObjectTransfrom.localPosition = Vector3.zero;
    }else{
        Debug.Log(kitchenObject.GetClearCounter());
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

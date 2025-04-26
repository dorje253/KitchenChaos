using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO  kitchenObjectSO;
    private  ClearCounter clearCounter;


    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }

    public void SetclearCounter(ClearCounter clearCounter){
        if(this.clearCounter != null){
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject()){
            Debug.LogError("Counter alrady has a kitchenObject !");
        }
        
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter(){
        return clearCounter;
    }
}

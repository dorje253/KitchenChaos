using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSo;
   [SerializeField] private Transform counterTopPoint;

public void Interact(){
    Debug.Log("Interact");
    Transform kitchenObjectTransfrom = Instantiate(kitchenObjectSo.prefab, counterTopPoint);
    kitchenObjectTransfrom.localPosition = Vector3.zero;

    Debug.Log(kitchenObjectTransfrom.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
}
}

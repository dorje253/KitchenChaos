using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
   [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player){
        Transform kitchenObjectTransfrom = Instantiate(kitchenObjectSo.prefab);
        kitchenObjectTransfrom.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    
    
}



}

using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
   [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            // Player is not carrying anythoing
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
            
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        
    }



}

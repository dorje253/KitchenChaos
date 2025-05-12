using UnityEngine;
using System;
using Unity.Netcode;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
   [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            // Player is not carrying anythoing
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);

            InteractLogicServerRpc();
        }
        
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc()
    {
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }



}

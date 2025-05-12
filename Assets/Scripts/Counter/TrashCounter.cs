using UnityEngine;
using System;
using Unity.Netcode;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player){
        if(player.HasKitchenObject()){
            KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
     
            InteractingLogicServerRpc();
            
        }
    }


    [ServerRpc(RequireOwnership = false)]
    private void InteractingLogicServerRpc()
    {
        InteractingLogicClientRpc();
    }

    [ClientRpc]
    private void InteractingLogicClientRpc()
    {
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
}

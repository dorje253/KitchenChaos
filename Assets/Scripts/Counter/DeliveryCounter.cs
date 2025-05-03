using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.GetKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // Only accpts Plates
                player.GetKitchenObject().DestroySelf();
            }
           
        }
    }
    
}

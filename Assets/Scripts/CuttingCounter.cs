using UnityEngine;

public class CuttingCounter : BaseCounter
{
     [SerializeField] private KitchenObjectSO  cutkitchenObjectSO;

   public override void Interact(Player player){
   if(!HasKitchenObject()){
      // There is no KitchenObject here
      if(player.HasKitchenObject()){
         // Player is carrying something
          player.GetKitchenObject().SetKitchenObjectParent(this);
      }else{
         // Player not carrying anything

      }
   }else{
      // There is a KitchenObject here
      if(player.HasKitchenObject()){
         // player is carrying something
      }else{
         // player is not carrying anything
         GetKitchenObject().SetKitchenObjectParent(player);
      }
   }
}

public override void InteractAlternate(Player player){
        if(HasKitchenObject()){
            // There is a KitchenObject here
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutkitchenObjectSO, this);
        }
    }

}

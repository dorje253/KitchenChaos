using UnityEngine;
using Unity.Netcode;
using System;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    public event EventHandler OnCut;

   

     [SerializeField] private CuttingRecipeSO[]  cuttingRecipeSOArray;

     private int cuttingProgress;

   public override void Interact(Player player){
   if(!HasKitchenObject()){
      // There is no KitchenObject here
      if(player.HasKitchenObject()){
         // Player is carrying something
         if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    //Player is carrying something that can be cut
                    KitchenObject kitchenObject = player.GetKitchenObject();
                    kitchenObject.SetKitchenObjectParent(this);
                    InteractLogicPlateObjectOnCounterServerRpc();
                }
         
      }else{
         // Player not carrying anything

      }
   }else{
      // There is a KitchenObject here
      if(player.HasKitchenObject()){
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else{
         // player is not carrying anything
         GetKitchenObject().SetKitchenObjectParent(player);
      }
   }
}

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicPlateObjectOnCounterServerRpc()
    {
        InteractLogicPlateObjectOnCounterClientRpc();
    }

    [ClientRpc]
    private void InteractLogicPlateObjectOnCounterClientRpc()
    {
        cuttingProgress = 0;

        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
        {
            progressNormalized = 0f
        });
    }

    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
            // There is a KitchenObject here and it can be cut

            CutObjectServerRpc();
            TestCuttingProgressDoneServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void CutObjectServerRpc()
    {
        CutObjectClientRpc();
    }

    [ClientRpc]
    private void CutObjectClientRpc()
    {
        cuttingProgress++;

        OnCut?.Invoke(this, EventArgs.Empty);
        //Debug.Log(OnAnyCut.GetInvocationList().Length);
        OnAnyCut?.Invoke(this, EventArgs.Empty);

        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
        {
            progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax

        });


        
    }

    [ServerRpc(RequireOwnership = false)]
    private void TestCuttingProgressDoneServerRpc()
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

        if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
        {
            KitchenObjectSO OutputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
            KitchenObject.SpawnKitchenObject(OutputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if(cuttingRecipeSO != null){
            return cuttingRecipeSO.output;
        }else{
            return null;
        }
        
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
            if(cuttingRecipeSO.input == inputKitchenObjectSO){
                return cuttingRecipeSO;
            }

        }
        return null;

    }

}

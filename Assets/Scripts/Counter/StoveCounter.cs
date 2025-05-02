using UnityEngine;
using System;
public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    public class OnStateChangedEventArgs : EventArgs{
        public State state;
    }
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;

    private float fryingTimer;
     private float burningTimer;
    private void Start(){
        state = State.Idle;
    }
    private void Update() {
    if (HasKitchenObject()) {
        switch (state) {
            case State.Idle:
                // Optional: do nothing
                break;

            case State.Frying:
                fryingTimer += Time.deltaTime;
                OnProgressChange?.Invoke(this, new  IHasProgress.OnProgressChangeEventArgs{
                 progressNormalized = fryingTimer/ fryingRecipeSO.fryingTimerMax
              });

                if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                    // Fried
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                   
                    
                    state = State.Fried;
                    burningTimer = 0f;
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });
                }
                break;

            case State.Fried:
                
                burningTimer += Time.deltaTime;
                OnProgressChange?.Invoke(this, new  IHasProgress.OnProgressChangeEventArgs{
                 progressNormalized = burningTimer/ burningRecipeSO.burningTimerMax
              });
                if (burningTimer > burningRecipeSO.burningTimerMax) {
                    // Fried
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                   
                    state = State.Burned;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });

                    OnProgressChange?.Invoke(this, new  IHasProgress.OnProgressChangeEventArgs{
                        progressNormalized = 0f
                    });
                    
                }
                break;

            case State.Burned:
                
                break;
        }
        
    }
}


    public override void Interact(Player player){
        if(!HasKitchenObject()){
      // There is no KitchenObject here
      if(player.HasKitchenObject()){
         // Player is carrying something
         if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
             //Player is carrying something that can be Fried
              player.GetKitchenObject().SetKitchenObjectParent(this);
              fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
              state = State.Frying;
              fryingTimer = 0f;

              OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });

              OnProgressChange?.Invoke(this, new  IHasProgress.OnProgressChangeEventArgs{
                 progressNormalized = fryingTimer/ fryingRecipeSO.fryingTimerMax
              });
            }
        }else{
             // Player not carrying anything
            }
        }else{
            // There is a KitchenObject here
            if(player.HasKitchenObject()){
                // player is carrying Something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else{
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });
                    
                OnProgressChange?.Invoke(this, new  IHasProgress.OnProgressChangeEventArgs{
                        progressNormalized = 0f
                    });

                }
            }
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if(fryingRecipeSO != null){
            return fryingRecipeSO.output;
        }else{
            return null;
        }
        
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
            if(fryingRecipeSO.input == inputKitchenObjectSO){
                return fryingRecipeSO;
            }

        }
        return null;

    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach(BurningRecipeSO burningRecipeSO in burningRecipeSOArray){
            if(burningRecipeSO.input == inputKitchenObjectSO){
                return burningRecipeSO;
            }

        }
        return null;

    }


    

}

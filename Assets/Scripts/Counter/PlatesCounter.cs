using UnityEngine;
using System;
public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
   private float spawnPlatesTimer;
   private float spawnPlatesTimerMax = 4.0f;
   private int platesSpawnAmount;
   private int platesSpawnAmountMax = 4;


   private void Update(){
    spawnPlatesTimer += Time.deltaTime;
    if(spawnPlatesTimer > spawnPlatesTimerMax){
        spawnPlatesTimer = 0f;
        if(KitchenGameManager.Instance.IsGamePlaying() && platesSpawnAmount < platesSpawnAmountMax){
            platesSpawnAmount++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
        // KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
    }
   }

   
    public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            // Player is Empty Handed
            if(platesSpawnAmount>0){
                // there is at least one plate here
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

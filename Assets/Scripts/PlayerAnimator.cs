using UnityEngine;
using Unity.Netcode;

public class PlayerAnimator : NetworkBehaviour
{
    [SerializeField] private Player player;
    private const string Is_Walking = "IsWalking";
    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        if (!IsOwner)
        {
            return;
        }
         animator.SetBool(Is_Walking, player.IsWalking());
    }
}

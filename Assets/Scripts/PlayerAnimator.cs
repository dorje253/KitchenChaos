using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private const string Is_Walking = "IsWalking";
    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
         animator.SetBool(Is_Walking, player.IsWalking());
    }
}

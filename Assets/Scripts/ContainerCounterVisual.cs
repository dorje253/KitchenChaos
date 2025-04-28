using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSe = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;

   private Animator animator;

   private void Awake(){
    animator = GetComponent<Animator>();
   } 

   private void Start(){
    containerCounter.OnPlayerGrabbedObject += containerCounter_OnPlayerGrabbedObject;
   }

   private void containerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e){
    animator.SetTrigger(OPEN_CLOSe);
   }
}


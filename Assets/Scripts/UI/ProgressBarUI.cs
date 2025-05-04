using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barimage;
    private IHasProgress hasProgess;

    private void Start(){
        hasProgess = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgess == null){
            Debug.Log("GameObject "+ hasProgressGameObject+ "doesn't have a component that implements IHasProgress!");
        }
        hasProgess.OnProgressChange += HasProgess_OnProgressChanged;
        barimage.fillAmount = 0f;
        Hide();
    }

    private void HasProgess_OnProgressChanged(object sender, IHasProgress.OnProgressChangeEventArgs e){
        barimage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f ||  e.progressNormalized == 1f){
            Hide();
        }else{
            Show();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

}

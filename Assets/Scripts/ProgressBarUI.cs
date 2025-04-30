using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barimage;

    private void Start(){
        cuttingCounter.OnProgressChange += CuttingCounter_OnProgressChanged;
        barimage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangeEventArgs e){
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

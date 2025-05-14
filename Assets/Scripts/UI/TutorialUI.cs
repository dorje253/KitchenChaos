using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeyMoveUpText;
    [SerializeField] private TextMeshProUGUI KeyMoveDownText;
    [SerializeField] private TextMeshProUGUI KeyMoveLeftText;
    [SerializeField] private TextMeshProUGUI KeyMoveRightText;
    [SerializeField] private TextMeshProUGUI KeyMoveInteractText;
    [SerializeField] private TextMeshProUGUI KeyMoveInteractAlternateText;
    [SerializeField] private TextMeshProUGUI KeyMovePauseText;
    [SerializeField] private TextMeshProUGUI KeyMoveGamepadInteractText;
    [SerializeField] private TextMeshProUGUI KeyMoveGamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI KeyMoveGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnLocalPlayerReadyChanged += KitchenGameManager_OnLocalPlayerReadyChanged;
        UpdateVisual();
        Show();
    }

    private void KitchenGameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsLocalPlayerReady())
        {
            Hide();
        }
    }

   

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        KeyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        KeyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        KeyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        KeyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        KeyMoveInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        KeyMoveInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        KeyMovePauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        KeyMoveGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        KeyMoveGamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        KeyMoveGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}


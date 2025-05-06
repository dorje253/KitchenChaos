using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {  get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button CloseButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;


    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform presstoRebindKeyTransform;
   

    private void Awake()
    {
        Instance = this;    
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        MusicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        CloseButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveUpButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_Up); });

        moveDownButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_Down); });

        moveLeftButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_Left); });

        moveRightButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Move_Right); });

        interactButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Interact); });

        interactAltButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.InteractAlternate); });

        pauseButton.onClick.AddListener(() =>{RebindBinding(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGameUnPaused (object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);

        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        presstoRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        presstoRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
       
    }
}

    


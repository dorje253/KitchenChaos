using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TestingNetworkUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    private void Awake()
    {
        startHostButton.onClick.AddListener(() =>
        {
            Debug.Log("HOST");
            KitchenGameMultiplayer.Instance.StartHost();
            Hide();
        });

        startClientButton.onClick.AddListener(() =>
        {
            Debug.Log("Client");
            KitchenGameMultiplayer.Instance.StartClient();
            Hide();
        });

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}

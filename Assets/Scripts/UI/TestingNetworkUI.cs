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
            NetworkManager.Singleton.StartHost();
            Hide();
        });

        startClientButton.onClick.AddListener(() =>
        {
            Debug.Log("Client");
            NetworkManager.Singleton.StartClient();
            Hide();
        });

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}

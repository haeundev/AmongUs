using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject onlineCanvas;
    [SerializeField] private Button onlineBackButton;
    [SerializeField] private Button mainMenuOnlineButton;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private TMP_InputField nicknameInputField;
    [SerializeField] private GameObject createRoomUI;
    private static readonly int Shake = Animator.StringToHash("Shake");

    private void Awake()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        mainMenuOnlineButton.onClick.AddListener(OnClickMainMenuOnlineButton);
        onlineBackButton.onClick.AddListener(OnClickOnlineBackButton);
        createRoomButton.onClick.AddListener(OnClickCreateRoomButton);
    }

    private void OnClickMainMenuOnlineButton()
    {
        mainMenuCanvas.SetActive(false);
        onlineCanvas.SetActive(true);
    }
    
    private void OnClickOnlineBackButton()
    {
        mainMenuCanvas.SetActive(true);
        onlineCanvas.SetActive(false);
    }
    
    private void OnClickCreateRoomButton()
    {
        if (nicknameInputField.text != "")
        {
            PlayerSettings.Nickname = nicknameInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            // todo: create animation
            nicknameInputField.GetComponent<Animator>()?.SetTrigger(Shake);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button onlineButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject onlineUI;

    private void Awake()
    {
        onlineButton.onClick.AddListener(OnClickOnlineButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }
    
    private void OnClickOnlineButton()
    {
        onlineUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button onlineButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        onlineButton.onClick.AddListener(OnClickOnlineButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    public void OnClickOnlineButton()
    {
        
    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button mouseControlButton;
    [SerializeField] private Button keyboardMouseControlButton;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private Button closeButton;
    private Animator _animator;
    private static readonly int CloseAnim = Animator.StringToHash("Close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        mouseControlButton.onClick.AddListener(OnClickMouseControlButton);
        keyboardMouseControlButton.onClick.AddListener(OnClickKeyboardMouseControlButton);
        backgroundButton.onClick.AddListener(Close);
        closeButton.onClick.AddListener(Close);
    }

    private void OnClickMouseControlButton()
    {
        SetControlMode(ControlType.Mouse);
    }
    
    private void OnClickKeyboardMouseControlButton()
    {
        SetControlMode(ControlType.KeyboardMouse);
    }
    
    private void OnEnable()
    {
        ChangeButtonColorByControlType();
    }

    private void ChangeButtonColorByControlType()
    {
        switch (PlayerSettings.ControlType)
        {
            case ControlType.Mouse:
                mouseControlButton.image.color = Color.green;
                keyboardMouseControlButton.image.color = Color.white;
                break;
            case ControlType.KeyboardMouse:
                mouseControlButton.image.color = Color.white;
                keyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    public void SetControlMode(ControlType controlType)
    {
        PlayerSettings.ControlType = controlType;
        ChangeButtonColorByControlType();
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        _animator.SetTrigger(CloseAnim);
        yield return YieldCache.WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        _animator.ResetTrigger(CloseAnim);
    }
}
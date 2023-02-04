using System.Collections.Generic;
using System.Linq;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util.Extensions;
using Random = UnityEngine.Random;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private List<Image> crewImages;
    [SerializeField] private List<Button> imposterCountButtons;
    [SerializeField] private List<Button> maxPlayerCountButtons;
    [SerializeField] private Button backButton;
    
    private GameRoomCreationData _roomData;
    private static readonly int PlayerColor = Shader.PropertyToID("_PlayerColor");
    private const float MaxImposterCount = 4;
    private Color White { get; } = PlayerColors.GetColor(PlayerColorType.White);

    private void Awake()
    {
        RegisterEvents();
    }
    
    private void OnClickBackButton()
    {
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    private void RegisterEvents()
    {
        backButton.onClick.AddListener(OnClickBackButton);
        
        for (var i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var i1 = i;
            maxPlayerCountButtons[i].onClick.AddListener(() =>
            {
                UpdateMaxPlayerCount(i1 + 4);
            });
        }
        
        for (var i = 0; i < imposterCountButtons.Count; i++)
        {
            var i1 = i;
            imposterCountButtons[i].onClick.AddListener(() =>
            {
                UpdateImposterCount(i1 + 1);
            });
        }
    }

    private void Start()
    {
        InstantiateMaterials();
        InitRoomData();
        UpdateCrewImages();
    }

    private void InstantiateMaterials()
    {
        foreach (var image in crewImages)
        {
            var mat = Instantiate(image.material);
            image.material = mat;
        }
    }

    private void InitRoomData()
    {
        _roomData = new GameRoomCreationData(1, 10);
    }

    private void UpdateCrewImages()
    {
        InitCrewMaterialColorToDefault();
        SetImpostersColorRed();
        ApplyCrewCount();
    }

    private void UpdateImposterCount(int count)
    {
        _roomData.ImposterCount = count;
        
        for (var i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        var minimumPlayersCount = GetMinimumPlayersCount(_roomData.ImposterCount);
        
        if (_roomData.MaxPlayerCount < minimumPlayersCount)
        {
            UpdateMaxPlayerCount(minimumPlayersCount);
        }
        else
        {
            UpdateMaxPlayerCount(_roomData.MaxPlayerCount);
        }

        for (var i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (i < minimumPlayersCount - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    private int GetMinimumPlayersCount(int imposterCount)
    {
        return imposterCount switch
        {
            1 => 4,
            2 => 7,
            _ => 9
        };
    }

    private void UpdateMaxPlayerCount(int count)
    {
        _roomData.MaxPlayerCount = count;

        for (var i = 0; i < maxPlayerCountButtons.Count; i++)
            maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, i == count - 4 ? 1f : 0f);
        
        UpdateCrewImages();
    }

    private void InitCrewMaterialColorToDefault()
    {
        crewImages.ForEach(p => p.material.SetColor(PlayerColor, White));
    }

    private readonly List<int> _crewIndices = new() { 0, 1, 2, 3, 4, 5, 6 };
    private void SetImpostersColorRed()
    {
        var selectedToBeRed = _crewIndices.Shuffle().Take(_roomData.ImposterCount);
        foreach (var index in selectedToBeRed)
            crewImages[index].material.SetColor(PlayerColor, Color.red);
    }

    private void ApplyCrewCount()
    {
        for (var i = 0; i < crewImages.Count; i++)
            crewImages[i].gameObject.SetActive(i < _roomData.MaxPlayerCount);
    }
}
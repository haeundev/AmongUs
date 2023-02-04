using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using Util.Extensions;
using Random = UnityEngine.Random;

public class CrewFloater : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float timer = 0.5f;
    [SerializeField] private float initialSpawnDistance = 8f;
    [SerializeField] private float laterSpawnDistance = 11f;
    [SerializeField] private float flowDirectionMagnitude = 0.25f; // 기본적으로 별 이펙트와 함께 오른쪽으로 흘러가게끔.
    [SerializeField] private float floatSpeedMin;
    [SerializeField] private float floatSpeedMax = 1f;
    [SerializeField] private float rotateSpeedMin = -0.15f;
    [SerializeField] private float rotateSpeedMax = 0.05f;
    [SerializeField] private float sizeMin = .2f;
    [SerializeField] private float sizeMax = 1.6f;

    private readonly Dictionary<PlayerColorType, bool> _crewStateByColor = new();
    private int _totalColorsCount;

    private void Awake()
    {
        foreach (var colorType in Enum.GetValues(typeof(PlayerColorType)).OfType<PlayerColorType>())
            _crewStateByColor.Add(colorType, false);
    }

    private void Start()
    {
        _totalColorsCount = PlayerColors.TotalColorCount;
        
        for (var i = 0; i < _totalColorsCount; i++)
            Spawn((PlayerColorType)i, true, Random.Range(0, initialSpawnDistance));
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (!(timer <= 0f))
            return;
        
        var availableColors = _crewStateByColor.Where(p => !p.Value).Select(q => q.Key).ToList();
        if (!availableColors.Any())
            return;
        
        Spawn(availableColors.PeekRandom(), false, laterSpawnDistance);
        timer = .5f;
    }
    
    /// <param name="colorType"></param> 크루 색상 지정.
    /// <param name="spawnInScreen"></param> 화면 안에서만 생성 T/F. 맨 처음 세팅을 위한 값.
    /// <param name="dist"></param> 카메라 중심으로부터의 거리. 그 밖에서 크루 생성.
    private void Spawn(PlayerColorType colorType, bool spawnInScreen, float dist)
    {
        var crewOfColorAlreadyExists = _crewStateByColor[colorType];
        if (crewOfColorAlreadyExists)
            return;

        var angle = Random.Range(0f, 2f * Mathf.PI);
        var spawnPosition = spawnInScreen
            ? camera.GetRandomVector3PointOnScreen()
            : new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;
        var direction = spawnInScreen
            ? new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f)
            : -spawnPosition.normalized;
        var flowDirection = Vector3.right * flowDirectionMagnitude;
        var floatingSpeed = Random.Range(floatSpeedMin, floatSpeedMax);
        var rotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);
        var size = Random.Range(sizeMin, sizeMax);
        var crew = Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<FloatingCrew>();
        crew.InitFloatingCrew(sprites.PeekRandom(), colorType, direction + flowDirection, floatingSpeed, rotateSpeed, size);

        _crewStateByColor[colorType] = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var crew = other.GetComponent<FloatingCrew>();
        if (crew == default)
            return;
        _crewStateByColor[crew.colorType] = false;
        Destroy(crew.gameObject);
    }
}

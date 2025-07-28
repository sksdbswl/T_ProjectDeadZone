using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    Player _player;
    public Player Player { get { return _player; } }

    UIManager _uiManager;
    public UIManager UIManager { get { return _uiManager; } }

    FloatingTextManager _floatingTextManager;
    public FloatingTextManager FloatingTextManager { get { return _floatingTextManager; } }

    DayNightCycle _dayNightCycle;
    public DayNightCycle DayNightCycle { get { return _dayNightCycle; } }

    private void Awake()
    {
        instance = this;

        // 참조
        _player = FindObjectOfType<Player>();
        _uiManager = FindObjectOfType<UIManager>();
        _floatingTextManager = FindObjectOfType<FloatingTextManager>();
        _dayNightCycle = FindObjectOfType<DayNightCycle>();


        // 초기화
        _dayNightCycle.Initialize();
        _uiManager.InitializeUI(this);

        _player.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

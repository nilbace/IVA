using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers instance {get{Init(); return s_instance;}}
    

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UI_Manager _ui_manager = new UI_Manager();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();
    DataManager _data = new DataManager();
    public static InputManager Input {get {return instance._input;}}
    public static ResourceManager Resource{get{return instance._resource;}}
    public static UI_Manager UI_Manager{get{return instance._ui_manager;}}
    public static SoundManager Sound{get{return instance._sound;}}
    public static PoolManager Pool{get{return instance._pool;}}
    public static DataManager Data { get { return instance._data; } }
    
    void Awake()
    {
        Init();
        StartCoroutine(s_instance._data.RequestAndSetDatas(DayDatasURL));
    }


    void Update()
    {
        _input.OnUpdate();
    }

    static void Init(){
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go==null)
            {
                go = new GameObject{name = "@Managers"};
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._pool.Init();
            s_instance._data.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        UI_Manager.Clear();
        
        Pool.Clear();
    }

    #region data
    const string DayDatasURL = "https://docs.google.com/spreadsheets/d/1WjIWPgya-w_QcNe6pWE_iug0bsF6uwTFDRY8j2MkO3o/export?format=tsv&gid=1890750354&range=D1:D";
    
    #endregion
}

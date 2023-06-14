using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.PostProcessing;

public class SceneLoader : MonoBehaviour
{
    static List<int> LevelsToPlay = new List<int> { 3, 4 };
    private Scene scene;

    public bool vrmode;
    private bool isSizerOn;
    private bool isMissingOn;
    private bool isVignetteOn;
    private bool isBlurOn;
    public Volume volume;
    private MotionBlur motionBlur = null;
    private Vignette vignette = null;
    //public GameObject player;
    private bool blurState;
    public bool overrideBlur;
    

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "main_menu")
        {
            print("main menu");
            StopXR();
        }
        else if (scene.name == "vr_maze_vr" || scene.name == "vr_adaptation" || scene.name == "vr_training" || scene.name == "vr_test")
        {
            print("vr");
            StartVR();
        }
        else if (scene.name == "vr_maze_glitch_vr" || scene.name == "vr_maze_glitch")
        {
            isSizerOn = PlayerPrefs.GetInt("sizer") == 1 ? true : false;
            GameObject.Find("wamsley_original").GetComponent<WallResizer>().enabled = isSizerOn;
            isMissingOn = PlayerPrefs.GetInt("missing") == 1 ? true : false;
            GameObject.Find("wamsley_original").GetComponent<MeshDisabler>().enabled = isMissingOn;
            print("vr");
            StartVR();
        }


        bool isOn = PlayerPrefs.GetInt("fog") == 1 ? true : false;
        RenderSettings.fog = isOn;
        
        //volume.profile.TryGet<MotionBlur>.intensity = 1;
        //volume.profile.TryGet<MotionBlur>(out motionBlur);
        //volume.profile.TryGet<Vignette>(out vignette);
         MotionBlur mbc;
         if (volume.profile.TryGet<MotionBlur>(out mbc))
         {
             motionBlur = mbc;
         }
         isBlurOn = PlayerPrefs.GetInt("blur") == 1 ? true : false;
         motionBlur.active = isBlurOn;

         Vignette vc;
         if (volume.profile.TryGet<Vignette>(out vc))
         {
             vignette = vc;
         }
         isVignetteOn = PlayerPrefs.GetInt("vignette") == 1 ? true : false;
         vignette.active = isVignetteOn;

        // VolumeProfile profile = volume.sharedProfile;
        
        // if (!profile.TryGet<MotionBlur>(out var blur))
        // {
        //     blur = profile.Add<MotionBlur>(false);
        // }
        // bool isBlurOn = PlayerPrefs.GetInt("blur") == 1 ? true : false;
        
        // blur.enabled.overrideState = overrideBlur;
        // blur.enabled.value = isBlurOn;

        // volume.profile.TryGetSettings(out motionBlur);
        // volume.profile.TryGetSettings(out vignette);
        
        // motionBlur.enabled.value = isBlurOn;
        //isVignetteOn = PlayerPrefs.GetInt("vignette") == 1 ? true : false;
        //GameObject.Find("FPSPlayer").GetComponent<Vignetter>().enabled = isVignetteOn;
        // vignette.enabled.value = isVignetteOn;

        // if (scene.name == "vr_maze_vr" || scene.name == "vr_adaptation" || scene.name == "vr_training")
        // {
        //     RenderSettings.fogEndDistance = 9;
        // }
        RenderSettings.fogEndDistance = 9;
        // if (scene.name != "main_menu")
        // {
        //     bool isPathOn = PlayerPrefs.GetInt("path") == 1 ? true : false;
        //     GameObject.Find("Capsule").GetComponent<DrawPath>().enabled = isPathOn;
        // }


        bool isVrOn = PlayerPrefs.GetInt("vr") == 1 ? true : false;
        vrmode = isVrOn;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
            StopXR();
        }
    }

    public void LoadRandomScene()
    {
        bool isVrOn = PlayerPrefs.GetInt("vr") == 1 ? true : false;
        if (isVrOn)
        {
            StopXR();
            SceneManager.LoadScene("vr_maze_vr");
        }
        else
        {
            //choose the index of a level:
            int nextLevelIndex = Random.Range(1, LevelsToPlay.Count);
            //get the actual sceneIndex by the index of our list:
            int nextLevel = LevelsToPlay[nextLevelIndex];
            // load the level:
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void LoadAdaptationScene()
    {
        if (PlayerPrefs.GetFloat("max_trials") != 0)
        {
            bool isVrOn = PlayerPrefs.GetInt("vr") == 1 ? true : false;
            if (isVrOn)
            {
                StopXR();
                SceneManager.LoadScene("vr_adaptation");
            }
            else
            {
                // load the level:
                SceneManager.LoadScene("adaptation");
            }
        }
        else
        {
            print("select number of trials");
        }
    }
    public void LoadTrainingScene()
    {
        bool isVrOn = PlayerPrefs.GetInt("vr") == 1 ? true : false;
        if (isVrOn)
        {
            StopXR();
            SceneManager.LoadScene("training_vr");
        }
        else
        {
            // load the level:
            SceneManager.LoadScene("training");
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
    public void LoadDreamScene()
    {
        bool isVrOn = PlayerPrefs.GetInt("vr") == 1 ? true : false;
        if (isVrOn)
        {
            StopXR();
            SceneManager.LoadScene("vr_maze_glitch_vr");
        }
        else
        {
            // load the level:
            SceneManager.LoadScene("vr_maze_glitch");
        }
    }

    public void StartVR()
    {
        StartCoroutine(StartXR());
    }
    public IEnumerator StartXR()
    {
        //StopXR();
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Directing to Normal Interaction Mode...!.");
            StopXR();
            //DirectToNormal();
        }
        else
        {
            Debug.Log("Initialization Finished.Starting XR Subsystems...");

            //Try to start all subsystems and check if they were all successfully started ( thus HMD prepared).
            bool loaderSuccess = XRGeneralSettings.Instance.Manager.activeLoader.Start();
            if (loaderSuccess)
            {
                vrmode = true;
                //XRGeneralSettings.Instance.Manager.StartSubsystems();
                Debug.Log("All Subsystems Started!");
                yield return null;
            }
            else
            {
                Debug.LogError("Starting Subsystems Failed. Directing to Normal Interaciton Mode...!");
                StopXR();
                //DirectToNormal();
            }
        }
    }

    void StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("All Subsystems Stopped!");
        }
    }
    public IEnumerator CheckXR()
    {
        //StopXR();
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Directing to Normal Interaction Mode...!.");
            StopXR();
            //DirectToNormal();
        }
        else
        {
            vrmode = true;

        }
    }
}

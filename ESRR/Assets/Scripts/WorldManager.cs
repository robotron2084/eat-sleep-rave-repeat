using System.Collections;
using GameJamStarterKit.Audio;
using UnityEngine;

namespace DefaultNamespace
{
  public class WorldManager : MonoBehaviour
  {

    public Scene[] scenes;
    public Scene initialScene;
    public Scene currentScene;
    public Player player;
    public Bed bed;
    public HighlightUI highlightUI;
    public SuperRaveCompletedUI completedUI;
    public DialogueController dialogue;
    public Giraffe giraffe;
    public Horse horse;
    public Portal portal; 
    public BackgroundMusic music;
    public Curtain curtain;
    public DJ dj;
    public SuperRaveQuest currentQuest;
    public GameObject horseArea;
    public GameObject deathDropArea;

    public GameObject[] hideOnStartup;
    public static WorldManager instance;
    

    void Awake()
    {
      instance = this;
    }

    void Start()
    {
      // hide all scenes.
      foreach (Scene config in scenes)
      {
        config.Hide();
      }

      foreach (GameObject go in hideOnStartup)
      {
        go.SetActive(false);
      }
      GotoScene(initialScene);
    }

    public void GotoScene(string sceneName)
    {
      Scene sceneConfig = null;
      foreach (Scene scene in scenes)
      {
        if (scene.sceneName == sceneName)
        {
          sceneConfig = scene;
          break;
        }
      }

      if (sceneConfig == null)
      {
        Debug.Log($"No scene config found for {sceneName}");
        return;
      }
      GotoScene(sceneConfig);
    }

    public void GotoScene(Scene scene)
    {
      StartCoroutine(gotoScene(scene));
    }

    IEnumerator gotoScene(Scene sceneConfig)
    {
      
      if (currentScene != null)
      {
        yield return currentScene.transitionOut();
      }
      currentScene = sceneConfig;
      
      yield return currentScene.transitionIn();
    }

  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    private static bool initialized = false;
    private static bool isLoading = false;


    public enum StageScene {
        Stage1,
        loading
    }

    public static void Init() {
        if (Loader.initialized) {
            return;
        }

        Loader.initialized = true;
        SceneManager.sceneLoaded += FinishLoading;
    }

    public static void Load(LevelObject levelData) {
        if (Loader.isLoading) {
            return;
        }

        Loader.Init();
        Loader.isLoading = true;

        LeanTween.delayedCall(
            0.01f, 
            () => {
                Debug.Log("Loading Screen...");
                SceneManager.LoadScene(StageScene.loading.ToString());

                Loader.isLoading = true;

                LeanTween.delayedCall(
                    0.01f,
                    () => {
                        Debug.Log("Loading Stage: " + levelData.stage.ToString());
                        
                        SceneManager.LoadSceneAsync(levelData.stage.ToString());   
                    }
                );
            }
        );
    }

    private static void FinishLoading(Scene scene, LoadSceneMode mode) {
        Loader.isLoading = false;
    }
}
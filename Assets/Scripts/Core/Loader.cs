using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    private static string loadingSceneName = "Loading";

    private static bool initialized = false;
    private static bool isLoading = false;

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
                SceneManager.LoadScene(Loader.loadingSceneName);

                Loader.isLoading = true;

                LeanTween.delayedCall(
                    0.01f,
                    () => {
                        Debug.Log("Loading Stage: " + levelData.scene.ToString());
                        
                        SceneManager.LoadSceneAsync(((SceneAsset)levelData.scene).name);   
                    }
                );
            }
        );
    }

    private static void FinishLoading(Scene scene, LoadSceneMode mode) {
        Loader.isLoading = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum CoreScenes {
        MainMenu,
        Loading
    };

    private static bool initialized = false;
    private static bool isLoading = false;

    public static void Init() {
        if (Loader.initialized) {
            return;
        }

        Loader.initialized = true;
        SceneManager.sceneLoaded += FinishLoading;
    }

    public static void LoadLevel(LevelObject levelData) {
        Loader.LoadScene(levelData.GetSceneName());
    }

    public static void LoadScene(string sceneName) {
        if (Loader.isLoading) {
            return;
        }

        Loader.Init();
        Loader.isLoading = true;

        LeanTween.delayedCall(
            0.01f,
            () => {
                SceneManager.LoadScene(Loader.CoreScenes.Loading.ToString());

                Loader.isLoading = true;

                LeanTween.delayedCall(
                    0.01f,
                    () => {
                        SceneManager.LoadSceneAsync(sceneName);
                    }
                ).setIgnoreTimeScale(true);
            }
        ).setIgnoreTimeScale(true);
    }

    private static void FinishLoading(Scene scene, LoadSceneMode mode) {
        Loader.isLoading = false;
    }
}
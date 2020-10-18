using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static bool isLoading = false;

    public enum Scene {
        Level_1,
        loading
    }

    public static void Load(Scene scene) {
        if (Loader.isLoading) {
            return;
        }

        Loader.isLoading = true;

        LeanTween.delayedCall(
            0.01f, 
            () => {
                SceneManager.LoadScene(Scene.loading.ToString());

                LeanTween.delayedCall(
                    1.5f,
                    () => {
                        SceneManager.LoadScene(scene.ToString());

                        Loader.isLoading = false;
                    }
                );
            }
        );
    }
}
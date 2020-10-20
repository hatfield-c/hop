using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected ReferenceManager references = null;

    public void ResetLevel() {
        this.references.player.Reset();
    }

}

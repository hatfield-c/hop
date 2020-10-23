using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    [Header("References")]
    public Hopper player;
    public GoalZone goalZone;
    public LevelUIController uiController;
    public LevelManager levelManager;
    public List<AbstractResettable> resetables = new List<AbstractResettable>();
}

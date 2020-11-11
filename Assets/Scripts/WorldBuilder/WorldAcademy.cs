using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class WorldAcademy : MonoBehaviour {

    [Header("Parameters")]
    [SerializeField] protected CellData cellData = new CellData();
    [SerializeField] protected SpaceData spaceData = new SpaceData();
    [SerializeField] protected GameObject geometryContainer = null;

    [Header("References")]
    [SerializeField] protected List<WorldAgent> agentList = new List<WorldAgent>();
    [SerializeField] protected BuilderUI builderUi = null;

    public static WorldAgent currentAgent;

    protected int typesCompleted = 0;

    public static void LearnStep() {
        WorldAcademy.currentAgent.RequestDecision();
        Academy.Instance.EnvironmentStep();
    }

    void Awake()
    {
        Academy.Instance.AutomaticSteppingEnabled = false;
        Academy.Instance.OnEnvironmentReset += this.EnvironmentReset;

        this.PreprocessAgents();
        this.builderUi.ChangeState(BuilderUI.BuildStates.generationFinished);
    }

    public void NextTerrainType() {
        if (this.IsFinishedGeneration()) {
            this.HasFinishedGeneration();
            return;
        }

        if(this.typesCompleted > 0) {
            WorldAcademy.currentAgent.gameObject.SetActive(false);
        }

        WorldAcademy.currentAgent = this.agentList[this.typesCompleted];
        WorldAcademy.currentAgent.gameObject.SetActive(true);
        
        this.typesCompleted++;
        this.builderUi.ChangeState(BuilderUI.BuildStates.typeCreate);
    }

    public void NewTerrain() {
        foreach(WorldAgent agent in this.agentList) {
            agent.DumpGeometry();
        }

        this.typesCompleted = 0;
        //this.ShuffleAgents();
        this.NextTerrainType();
    }

    protected void HasFinishedGeneration() {
        WorldAcademy.currentAgent.gameObject.SetActive(false);
        this.builderUi.ChangeState(BuilderUI.BuildStates.generationFinished);
    }

    protected bool IsFinishedGeneration() {
        if(this.typesCompleted < this.agentList.Count) {
            return false;
        }

        return true;
    }

    protected void PreprocessAgents() {
        for (int i = 0; i < this.agentList.Count; i++) {
            this.agentList[i].Init(this.cellData, this.spaceData, this.builderUi);
            this.agentList[i].gameObject.SetActive(false);
        }
    }

    protected void ShuffleAgents() {
        for (int i = 0; i < this.agentList.Count; i++) {
            int randomIndex = Random.Range(i, this.agentList.Count);

            WorldAgent temp = this.agentList[i];
            this.agentList[i] = this.agentList[randomIndex];
            this.agentList[randomIndex] = temp;
        }
    }

    void EnvironmentReset() {

    }

    [System.Serializable]
    public struct CellData {
        public int xCells;
        public int yCells;
        public int zCells;
    }

    [System.Serializable]
    public struct SpaceData {
        public float maxX;
        public float maxY;
        public float maxZ;
    }
}

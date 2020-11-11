using Google.Protobuf.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class WorldAgent : Agent
{
    [Header("References")]
    [SerializeField] protected Transform geometryContainer = null;

    [Header("Space Parameters")]
    [SerializeField] protected PermutationParameters permutationParameters = new PermutationParameters();

    [Header("Build Parameters")]
    [SerializeField] protected GameObject prefab = null;
    [SerializeField] protected ScaleType scaling = ScaleType.all_Scale;
    [SerializeField] protected int totalCount = 1;
    [SerializeField] protected int stepCount = 1;

    protected WorldAcademy.CellData cellData;
    protected WorldAcademy.SpaceData spaceData;
    protected BuilderUI builderUi;

    protected int currentCount = 0;
    protected int currentTotal = 0;
    protected bool finished = false;

    // *
    // *    Actions:
    // *        0 : xPos
    // *        1 : yPos
    // *        2 : zPos
    // *        3 : xRot
    // *        4 : yRot
    // *        5 : zRot
    // *        6 : scale1
    // *        7 : scale2 (optional)
    // *        8 : scale3 (optional)
    // *
    public override void OnActionReceived(float[] vectorAction) {
        base.OnActionReceived(vectorAction);

        GameObject newObject = Instantiate(this.prefab);
        this.ApplyPosition(newObject, vectorAction);
        this.ApplyRotation(newObject, vectorAction);
        this.ApplyScale(newObject, vectorAction);
        newObject.transform.parent = this.geometryContainer;

        this.currentCount++;
        this.currentTotal++;

        if(this.currentCount >= this.stepCount || this.currentTotal >= this.totalCount) {
            this.currentCount = 0;
            
            if (this.currentTotal >= this.totalCount) {
                this.finished = true;
            }

            this.builderUi.ChangeState(BuilderUI.BuildStates.typeReward);
        } else {
            WorldAcademy.LearnStep();
        }
    }

    public override void CollectObservations(VectorSensor sensor) {
        base.CollectObservations(sensor);

        sensor.AddObservation(0f);
    }

    public override void OnEpisodeBegin() {
        base.OnEpisodeBegin();

        this.currentCount = 0;
        this.currentTotal = 0;
        this.finished = false;
    }

    public override void Heuristic(float[] actionsOut) {
        
    }

    public void DumpGeometry() {
        foreach(Transform terrainTrans in this.geometryContainer) {
            Destroy(terrainTrans.gameObject);
        }
    }

    public void Init(WorldAcademy.CellData cellData, WorldAcademy.SpaceData spaceData, BuilderUI builderUi) {
        this.cellData = cellData;
        this.spaceData = spaceData;
        this.builderUi = builderUi;
    }

    public bool IsFinished() {
        return this.finished;
    }

    public void Finish() {
        this.EndEpisode();
    }

    protected void ApplyPosition(GameObject prefab, float[] vectorAction) {
        float x = (vectorAction[0] + 1) / 2;
        float y = (vectorAction[1] + 1) / 2;
        float z = (vectorAction[2] + 1) / 2;

        prefab.transform.position = new Vector3(
            x * this.spaceData.maxX,
            y * this.spaceData.maxY,
            z * this.spaceData.maxZ
        );
    }

    protected void ApplyRotation(GameObject prefab, float[] vectorAction) {
        float rot1 = (vectorAction[3] + 1) / 2;
        float rot2 = (vectorAction[4] + 1) / 2;
        float rot3 = (vectorAction[5] + 1) / 2;

        prefab.transform.eulerAngles = new Vector3(
            rot1 * 360,
            rot2 * 360,
            rot3 * 360
        );
    }

    protected void ApplyScale(GameObject prefab, float[] vectorAction) {
        float scale1 = (vectorAction[6] + 1) / 2;
        float scale2 = 0f;
        float scale3 = 0f;

        switch (this.scaling) {
            case ScaleType.xyz_Scale:        
                scale2 = (vectorAction[7] + 1) / 2;
                scale3 = (vectorAction[8] + 1) / 2;

                prefab.transform.localScale = new Vector3(
                    scale1 * this.permutationParameters.maxScale,
                    scale2 * this.permutationParameters.maxScale,
                    scale3 * this.permutationParameters.maxScale
                );
                break;
            case ScaleType.xz_Scale_Y_Scale:
                scale2 = (vectorAction[7] + 1) / 2;

                prefab.transform.localScale = new Vector3(
                    scale1 * this.permutationParameters.maxScale,
                    scale2 * this.permutationParameters.maxScale,
                    scale1 * this.permutationParameters.maxScale
                );
                break;
            case ScaleType.all_Scale:
                prefab.transform.localScale = new Vector3(
                    scale1 * this.permutationParameters.maxScale,
                    scale1 * this.permutationParameters.maxScale,
                    scale1 * this.permutationParameters.maxScale
                );
                break;
            default:
                break;
        }

        scale2 += 0;
        scale3 += 0;
    }

    // *
    // *    ScaleType:
    // *        xyz_Scale           : x, y, z scale axes done independently
    // *        xz_Scale_Y_Scale    : xz scaled together, y scaled independently
    // *        all_scale           : xyz all scaled together
    // *
    public enum ScaleType {
        xyz_Scale,
        xz_Scale_Y_Scale,
        all_Scale
    }

    [System.Serializable]
    public struct PermutationParameters {
        public float maxScale;
    }
}

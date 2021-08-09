using Google.Protobuf.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
    protected int voxelCount;

    protected Vector3 voxelSize = new Vector3();
    protected CellSize cellSize = new CellSize();

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

        for(int x = 0; x < this.cellData.xCells; x++) {
            for(int y = 0; y < this.cellData.yCells; y++) {
                for(int z = 0; z < this.cellData.zCells; z++) {
                    float fill = this.CalculateCellFill(x, y, z);
                    sensor.AddObservation(fill);
                }
            }
        }
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

    public void Init(WorldAcademy.CellData cellData, WorldAcademy.SpaceData spaceData, int voxelCount, BuilderUI builderUi) {
        this.cellData = cellData;
        this.spaceData = spaceData;
        this.voxelCount = voxelCount;
        this.builderUi = builderUi;

        this.cellSize.x = (2 * this.spaceData.maxX) / this.cellData.xCells;
        this.cellSize.y = (2 * this.spaceData.maxY) / this.cellData.yCells;
        this.cellSize.z = (2 * this.spaceData.maxZ) / this.cellData.zCells;

        this.voxelSize.x = this.cellSize.x / this.voxelCount;
        this.voxelSize.y = this.cellSize.y / this.voxelCount;
        this.voxelSize.z = this.cellSize.z / this.voxelCount;
    }

    public bool IsFinished() {
        return this.finished;
    }

    public void Finish() {
        this.EndEpisode();
    }

    protected float CalculateCellFill(int cellX, int cellY, int cellZ) {
        Vector3 cell = new Vector3(
            (cellX * this.cellSize.x) - this.spaceData.maxX,
            (cellY * this.cellSize.y) - this.spaceData.maxY,
            (cellZ * this.cellSize.z) - this.spaceData.maxZ
        );
        Vector3 voxel = new Vector3();

        bool collision = false;
        float collisionCount = 0;
        float maxCollisions = this.voxelCount * this.voxelCount * this.voxelCount;

        for (int xVox = 0; xVox < this.voxelCount; xVox++) {
            for (int yVox = 0; yVox < this.voxelCount; yVox++) {
                for (int zVox = 0; zVox < this.voxelCount; zVox++) {
                    voxel.x = (xVox * this.voxelSize.x) + cell.x + (this.voxelSize.x / 2);
                    voxel.y = (yVox * this.voxelSize.y) + cell.y + (this.voxelSize.y / 2);
                    voxel.z = (zVox * this.voxelSize.z) + cell.z + (this.voxelSize.z / 2);

                    collision = Physics.CheckBox(voxel, (this.voxelSize / 2));

                    if (collision) {
                        collisionCount++;
                    }
                }
            }
        }

        return collisionCount / maxCollisions;
    }

    protected void ApplyPosition(GameObject prefab, float[] vectorAction) {
        float x = vectorAction[0];
        float y = vectorAction[1];
        float z = vectorAction[2];

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

    public struct CellSize {
        public float x;
        public float y;
        public float z;
    }
}

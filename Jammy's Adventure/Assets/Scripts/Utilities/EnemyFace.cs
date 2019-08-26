using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyFaceType
{
    DEAD,
    CLOSED,
    ANGRY,
    DIZZY
}

public class EnemyFace : MonoBehaviour
{
    [SerializeField] private int faceMaterial = 1;

    private MeshRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponentInChildren<MeshRenderer>();

        myRenderer.materials[faceMaterial] = new Material(myRenderer.materials[faceMaterial]);
        ChangeFace(EnemyFaceType.ANGRY);
    }

    public void ChangeFace(EnemyFaceType face)
    {
        switch (face)
        {
            case EnemyFaceType.DEAD:
                myRenderer.materials[faceMaterial].mainTextureOffset = new Vector2(0.0f, 0.0f);
                break;

            case EnemyFaceType.CLOSED:
                myRenderer.materials[faceMaterial].mainTextureOffset = new Vector2(0.25f, 0.0f);
                break;

            case EnemyFaceType.ANGRY:
                myRenderer.materials[faceMaterial].mainTextureOffset = new Vector2(0.5f, 0.0f);
                break;

            case EnemyFaceType.DIZZY:
                myRenderer.materials[faceMaterial].mainTextureOffset = new Vector2(0.75f, 0.0f);
                break;
        }
    }
}
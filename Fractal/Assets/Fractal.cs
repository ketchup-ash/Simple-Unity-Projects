﻿using System.Collections;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh[] meshes;
    public Material material;

    public float childScale;
    public int maxDepth;
    public float possibility;
    public float maxRotationSpeed;
    public float maxTwist;

    private int depth;
    private float rotationSpeed;
    private Material[,] materials;

    private static Vector3[] childDirection = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] childOrientation = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };

    public void Start() {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
        if (materials == null) {
            InitializeMaterial();
        }
        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = material;
        GetComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];
        if (depth < maxDepth) {
            StartCoroutine(CreateChildren());
        }
    }

    private IEnumerator CreateChildren() {
        for (int i = 0; i < childOrientation.Length; i++) {
            if (Random.value < possibility) {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
            }
        }
    }

    private void InitializeMaterial() {
        materials = new Material[maxDepth + 1, 2];
        for (int i = 0; i <= maxDepth; i++) {
            float t = i / (maxDepth - 1f);
            t *= t;
            materials[i, 0] = new Material(material);
            materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
        }
        materials[maxDepth, 0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.red;
    }

    private void Initialize(Fractal parent, int childIndex) {
        meshes = parent.meshes;
        maxTwist = parent.maxTwist;
        maxRotationSpeed = parent.maxRotationSpeed;
        possibility = parent.possibility;
        materials = parent.materials;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirection[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientation[childIndex];
    }

    void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

}

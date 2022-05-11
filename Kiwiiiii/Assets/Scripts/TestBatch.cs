using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestBatch : MonoBehaviour
{
    //private void Awake()
    //{
    //    GraphicsSettings.useScriptableRenderPipelineBatching = true;
    //    StaticBatchingUtility.Combine(gameObject);
    //}



    [SerializeField] private List<MeshFilter> sourceMeshFilters;
    [SerializeField] private MeshFilter targetMeshFilter;

    private void Awake()
    {
        GetChildMeshes();
        CombineMeshesOfChildren();
    }

    private void GetChildMeshes()
    {
        for (int i = 0; i < transform.childCount; i++) {

            var child = transform.GetChild(i);

            if (child.childCount > 0) {
                foreach (MeshFilter mesh in child.GetComponentsInChildren<MeshFilter>()) {
                    sourceMeshFilters.Add(mesh);
                }
            }

            Destroy(child.gameObject);
        }
    }


    [ContextMenu("Combine Meshes")]
    private void CombineMeshesOfChildren()
    {
        var combine = new CombineInstance[sourceMeshFilters.Count];

        for (int i = 0; i < sourceMeshFilters.Count; i++) {
            combine[i].mesh = sourceMeshFilters[i].mesh;
            combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.CombineMeshes(combine);
        targetMeshFilter.mesh = mesh;
        
    }


}

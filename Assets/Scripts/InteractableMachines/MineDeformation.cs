using UnityEngine;

public class MineDeformation : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits = 0;
    
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    private float deformationAmount = 0.01f;
    private float minY = -1f;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            originalVertices = meshFilter.mesh.vertices;
            displacedVertices = meshFilter.mesh.vertices.Clone() as Vector3[];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeformFromTopToBottom();
            currentHits++;
            if (currentHits >= maxHits)
            {
                Destroy(gameObject, 2f);
            }
        }
    }

    void DeformFromTopToBottom()
    {
        if (meshFilter == null || displacedVertices == null) return;
        
        float maxY = float.MinValue;
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            if (displacedVertices[i].y > maxY)
            {
                maxY = displacedVertices[i].y;
            }
        }
        
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            if (displacedVertices[i].y >= maxY - 0.1f)
            {
                float randomDeformation = Random.Range(0.05f, deformationAmount);
                displacedVertices[i].y -= randomDeformation;
                displacedVertices[i].y = Mathf.Max(displacedVertices[i].y, minY);
            }
        }

        meshFilter.mesh.vertices = displacedVertices;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
    }
}
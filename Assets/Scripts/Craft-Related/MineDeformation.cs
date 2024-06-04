using UnityEngine;

public class MineDeformation : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits = 0;
    
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    private float deformationAmount = 0.01f; // Base amount of deformation
    private float minY = -1f; // Minimum Y value to clamp the deformation

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
        if (Input.GetKeyDown(KeyCode.Space)) // Test deformation with space key
        {
            DeformFromTopToBottom();
            currentHits++;
            if (currentHits >= maxHits)
            {
                Destroy(gameObject, 2f); // Destroys the rock after 2 seconds
            }
        }
    }

    void DeformFromTopToBottom()
    {
        if (meshFilter == null || displacedVertices == null) return;

        // Find the highest Y value in the mesh
        float maxY = float.MinValue;
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            if (displacedVertices[i].y > maxY)
            {
                maxY = displacedVertices[i].y;
            }
        }

        // Deform vertices top to bottom with randomness
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            if (displacedVertices[i].y >= maxY - 0.1f) // Adjust the range as needed for more natural effect
            {
                float randomDeformation = Random.Range(0.05f, deformationAmount); // Random deformation amount
                displacedVertices[i].y -= randomDeformation;

                // Optional: Ensure vertices don't go below a certain threshold
                displacedVertices[i].y = Mathf.Max(displacedVertices[i].y, minY);
            }
        }

        meshFilter.mesh.vertices = displacedVertices;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
    }
}
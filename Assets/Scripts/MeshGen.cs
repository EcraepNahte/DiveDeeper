using System.Collections.Generic;
using UnityEngine;

public class MeshGen : MonoBehaviour
{
    // The length of segment (world space)
    public float SegmentLength = 5;
    // the segment resolution (number of horizontal points)
    public int SegmentResolution = 32;
    // the size of meshes in the pool
    public int MeshCount = 4;
    // the maximum number of visible meshes. Should be lower or equal to MeshCount
    public int VisibleMeshes = 4;
    // indicates whether the floor should come from left or right 
    public bool IsFromLeft = true;
    // the prefab including MeshFilter and MeshRenderer
    public MeshFilter SegmentPrefab;

    // helper array to generage a new segment without further allocations
    private Vector3[] vertexArray;
    // the pool of free mesh filters
    private List<MeshFilter> freeMeshFilters = new List<MeshFilter>();
    // the list of used segments
    private List<Segment> usedSegments = new List<Segment>();
    // the edge of the screen from the direction indicated;
    private float screenEdgeX;
    // Game Manager
    private GameManager gameManager;

    private struct Segment
    {
        public int Index { get; set; }
        public MeshFilter MeshFilter { get; set; }
    }

    /////////// Unity Methods ///////////

    private void Awake()
    {
        gameManager = GameManager.Instance;

        // create vertex array helper
        vertexArray = new Vector3[SegmentResolution * 2];

        // Build triangles array. For all meshes this array always will look the same, so only generating once
        int iterations = vertexArray.Length / 2 - 1;
        var triangles = new int[(vertexArray.Length - 2) * 3];

        for (int i = 0; i < iterations; ++i)
        {
            int i2 = i * 6;
            int i3 = i * 2;

            triangles[i2] = i3 + 2;
            triangles[i2 + 1] = i3 + 1;
            triangles[i2 + 2] = i3 + 0;

            triangles[i2 + 3] = i3 + 2;
            triangles[i2 + 4] = i3 + 3;
            triangles[i2 + 5] = i3 + 1;
        }

        // Create colors array. For now make it all white
        var colors = new Color[vertexArray.Length];
        for (int i = 0; i < vertexArray.Length; ++i)
        {
            var defaultColors = gameManager.WallColors;
            int colorIndex = i % defaultColors.Count;

            colors[i] = defaultColors[colorIndex];
        }

        // Create game objects (with MeshFilter) instances.
        // Assign verticies, triangles, deactivate and add to the pool.
        for (int i = 0; i < MeshCount; ++i)
        {
            MeshFilter filter = Instantiate(SegmentPrefab);

            Mesh mesh = filter.mesh;
            mesh.Clear();

            mesh.vertices = vertexArray;
            mesh.triangles = triangles;
            mesh.colors = colors;

            filter.gameObject.SetActive(false);
            freeMeshFilters.Add(filter);
        }

        // get the edge of the screen value
        screenEdgeX = Camera.main.ViewportToWorldPoint(new Vector3(IsFromLeft ? 0 : 1, 0, 0)).x;
    }

    private void Update()
    {
        // get the index of visible segment by finding center point of world position
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        int currentSegment = (int)(worldCenter.y / SegmentLength);

        // Test visible segments for visibility and hide those that aren't visible
        for (int i = 0; i < usedSegments.Count;)
        {
            int segmentIndex = usedSegments[i].Index;
            if (!IsSegmentInSight(segmentIndex))
            {
                EnsureSegmentNotVisible(segmentIndex);
            } 
            else
            {
                // increment here to ensure that a segment not in sight is properly turned to not visible and not used
                ++i;
            }
        }

        // Test neighbor segment idexes for visibility and display those if should be visible
        for (int i = currentSegment - VisibleMeshes / 2; i < currentSegment + VisibleMeshes / 2; ++i)
        {
            if (IsSegmentInSight(i))
            {
                EnsureSegmentVisible(i);
            }
        }
    }

    /////////// Private Methods ///////////

    // This function generates a mesh segment
    // Index is a segment index (starting at 0)
    // Mesh is a mesh that this segment should be written to
    private void GenerateSegment(int index, ref Mesh mesh)
    {
        float startPosition = index * SegmentLength;
        float step = SegmentLength / (SegmentResolution - 1);

        for (int i = 0; i < SegmentResolution; ++i)
        {
            // get the relative y position
            float yPos = step * i * (IsFromLeft ? -1 : 1);

            // top vertex
            float xPosTop = screenEdgeX + GetHeight(startPosition + yPos);
            vertexArray[i * 2] = new Vector3(xPosTop, yPos, 0);

            // bottom vertex always at y = 0
            vertexArray[i * 2 + 1] = new Vector3(screenEdgeX, yPos, 0);
        }

        mesh.vertices = vertexArray;

        // need to recalculate bounds because mesh can disappear too early
        mesh.RecalculateBounds();
    }

    // Gets the height of terrain at current position
    // Modify this function to get different terrain configurations
    private float GetHeight(float position)
    {
        float relativePosition = position + (IsFromLeft ? 0f : 1f);
        float height = (Mathf.Sin(relativePosition ) + 1.5f + Mathf.Sin(relativePosition * 1.75f) + 1f) / 4f;

        // Increment the height as you move down, maxing out at a certain height
        height += gameManager.MaxWallWidth * Mathf.Clamp(position / gameManager.DepthForMaxWallWidth, 0, 1);

        if (!IsFromLeft)
        {
            // Flip the height if the number comes from the bottom of the screen
            height *= -1;
        }

        return height;
    }

    // Check to see if the segment is visible by the camera
    private bool IsSegmentInSight(int index)
    {
        Vector3 worldTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 worldBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        // check top and bottom segment side
        float y1 = (IsFromLeft ? index : index + 1) * SegmentLength;
        float y2 = y1 - SegmentLength;
        
        return (y1 >= worldBottom.y) && (y2 <= worldTop.y);
    }

    // Check if segment is visible so we don't use more than one MeshFilter for the same segment
    private bool IsSegmentVisible(int index)
    {
        return SegmentCurrentlyVisibleListIndex(index) >= 0;
    }

    // returns list index of currently visible segment
    private int SegmentCurrentlyVisibleListIndex(int index)
    {
        for (int i = 0; i < usedSegments.Count; ++i)
        {
            if (usedSegments[i].Index == index)
            {
                return i;
            }
        }

        return -1;
    }

    // makes sure the segment of the specified index is visible
    private void EnsureSegmentVisible(int index)
    {
        if (!IsSegmentVisible(index))
        {
            // get from the pool
            int meshIndex = freeMeshFilters.Count - 1;
            MeshFilter filter = freeMeshFilters[meshIndex];
            freeMeshFilters.RemoveAt(meshIndex);

            // generate
            Mesh mesh = filter.mesh;
            GenerateSegment(index, ref mesh);

            // position
            filter.transform.position = new Vector3(0, index * SegmentLength, 0);

            // make visible
            filter.gameObject.SetActive(true);

            //register as a visible segment
            var segment = new Segment();
            segment.Index = index;
            segment.MeshFilter = filter;

            usedSegments.Add(segment);
        }
    }

    // hide the segment at the specified index
    private void EnsureSegmentNotVisible(int index)
    {
        if (IsSegmentVisible(index))
        {
            int listIndex = SegmentCurrentlyVisibleListIndex(index);
            Segment segment = usedSegments[listIndex];
            usedSegments.RemoveAt(listIndex);

            MeshFilter filter = segment.MeshFilter;
            filter.gameObject.SetActive(false);

            freeMeshFilters.Add(filter);
        }
    }
}

using UnityEngine;

public class OrbitLine : MonoBehaviour {

    LineRenderer line;
    
    private int segments;
    private float radius;

    void Start () {
        line = GetComponent<LineRenderer>();
        
        radius = transform.position.magnitude;
        segments = (int)radius / 150;
        
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = true;
        line.sortingLayerName = "Radar";
        DrawOrbit();
    }
   
    void DrawOrbit () {
        float x;
        float y;
        float z = 0f;
       
        float angle = 20f;
       
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;
                   
            line.SetPosition (i, new Vector3(x,y,z));
                   
            angle += (360f / segments);
        }
    }
}

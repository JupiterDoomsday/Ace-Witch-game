using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using meshmath;

public class FieldOfView : MonoBehaviour
{
        [SerializeField] private LayerMask layer;
        private Vector3 origin;
        private Mesh mesh;
        public MeshFilter filter;
        public bool playerInRange;
        public float fov = 90f;
        public float startingAngle;
        public float viewDistance;
        private float angleIncrease;
        public int raycount = 2;
        private void Start()
        {
            mesh = new Mesh();
            filter.mesh = mesh;
            origin = Vector2.zero;
        }
        private void Update() { 
            angleIncrease = fov / raycount;
            float angle = startingAngle;
            Vector3[] vertices = new Vector3[raycount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[raycount * 3];

            vertices[0] = origin;
            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= raycount; i++)
            {
                Vector3 vertex;
                
                RaycastHit2D hit = Physics2D.Raycast(origin, MeshMath.GetVectorFromAngle(angle), viewDistance, layer);

                if (hit.collider == null)
                    vertex = origin + MeshMath.GetVectorFromAngle(angle) * viewDistance;
                else
                    vertex = hit.point;

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 1] = vertexIndex;

                    triangleIndex += 3;
                }
                
                angle -= angleIncrease;
            }
            
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            
        }
        
        public void setOrigin(Vector3 origin)
        {
            this.origin = origin;
        }
        public void SetAimDirection(Vector3 dir)
        {
            startingAngle = MeshMath.GetAngleFromVectorFloat(dir) - fov/2f;
        }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMesh {
    public Vector3[] vertices;
    int[] triangles;
    Vector3[] interpolatedVertices;
    //Vector3[] totalVertices;
    List<Vector3> totalVertices;

    Mesh mesh;
    Vector2[] uvs;
    Blob blob;
    float offsetMag;
    int detail;
    Matrix4x4 worldToLocal;

    public BlobMesh(Blob blob, Mesh mesh){
        this.mesh = mesh;
        this.blob = blob;
        this.detail = blob.meshDetail;
        
        // offset so that mesh goes all the way
        this.offsetMag = blob.joints[0].GetComponent<CircleCollider2D>().radius;

        vertices = new Vector3[blob.numJoints];
        interpolatedVertices = new Vector3[vertices.Length * (detail - 1)];
        
        // plus one for the centerJoint
        totalVertices = new List<Vector3>();
        triangles = new int[0];

        // set vertices for mesh
        for(int i = 0; i < blob.numJoints; i++){
            vertices[i] = blob.joints[i].transform.position;
            //Debug.Log("initMesh verts:" + i + " " + vertices[i]);
        }
    } 

    public void UpdateMesh(){
        triangles = mesh.triangles;
        Vector2 offset;
        Vector2 jointPos;
        Vector2 centerPos = blob.centerBody.position; 
        
        for(int i = 0; i < blob.numJoints; i++){
            jointPos = blob.joints[i].GetComponent<Rigidbody2D>().position;
            offset = (jointPos - centerPos).normalized * offsetMag;
            vertices[i] = jointPos + offset; 
            //ebug.Log("vertice:" + i + " " + vertices[i]);
        }

        totalVertices = Interpolation.QuadraticBezier(vertices, detail);
        totalVertices.Add(centerPos);
        
        // if length if off, redo the triangle array
        if(triangles.Length != (totalVertices.Count - 1)*3){
            triangles = new int[(totalVertices.Count - 1)*3];
            int triangleIndex = 0;
            for(int i = 0; i < totalVertices.Count - 1; i++){
                triangles[triangleIndex] = i;
                triangles[triangleIndex + 1] = (i + 1 > totalVertices.Count - 2 ? 0 : i + 1);;
                triangles[triangleIndex + 2] = totalVertices.Count - 1;

                triangleIndex += 3;
            }
        }

        // do the uvs
        if(uvs == null){
            Vector2 origin = new Vector2(1f, 1f);
            Vector2 point;
            float angle;
            uvs = new Vector2[totalVertices.Count];
            Debug.Log(totalVertices.Count);
            for(int i = 0; i < uvs.Length; i++){
                point = blob.gameObject.transform.InverseTransformPoint(totalVertices[i]) / (blob.radius + offsetMag);
                point.Set((point.x + 1)/2f, (point.y + 1)/2f);
                //Debug.Log("UV:" + i + " " + totalVertices[i]);
                uvs[i] = point;
            }
        }

        //reset mesh 
        mesh.Clear();
        mesh.vertices = totalVertices.ToArray();
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

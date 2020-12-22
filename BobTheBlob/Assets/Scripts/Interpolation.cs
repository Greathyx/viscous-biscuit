using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation {
    private static Matrix4x4 cubicMatrix = InitMatrix();
    private static float multiplier = 0.5f;

    public static List<Vector3> QuadraticBezier(Vector3[] vertices, int tSteps){
        /* Blob interpolation using Bezier formulation
        In: 
            Vertices: Original points to be interpolated from
            tSteps: How many new points from original points do we want

        Out:
            InterpolatedVertices: The new vertices
        */
        List<Vector3> interpolatedVertices = new List<Vector3>();
        Vector2 p1, p2, p3;
        Vector2 controllPoint;
        Vector2 norm;
        float tInterval = 1f/(tSteps);
        float t;
        
        for(int i = 0; i < vertices.Length; i++){ // the minus 1 is for neglecting the center joint 
            p1 = vertices[i - 1 < 0 ? vertices.Length - 1 : i - 1];
            p2 = (Vector2) vertices[i];
            
            p3 = (Vector2) vertices[i + 1 > vertices.Length - 1 ? 0 : i + 1];
            //p4 = vertices[i + 2 > vertices.Length - 1? i + 2 - vertices.Length: i + 2];
            
            controllPoint = GetMidPoint(p2, p3);
            norm = (p3 - p2).normalized;
            norm.Set(-norm.y, norm.x);
            controllPoint += norm * multiplier;

            for(int j = 0; j < tSteps; j++){
                t = (tInterval * (j));
                // bezier thing interpolation
                interpolatedVertices.Add(Mathf.Pow(1 - t, 2)*p2 + 2*(1 - t)*t*controllPoint + t*t*p3);
            }    
        }
        return interpolatedVertices;    
    }
    
    // pass in origin point p and direction dir of each line
    public static Vector2 FindIntersection(Vector2 p1, Vector2 dir1, Vector2 p2, Vector2 dir2){
        float t;
        t = Cross2d(p1 - p2, dir2 / Cross2d(dir2, dir1));
        return p1 + dir1*t;
    }

    private static float Cross2d(Vector2 vec1, Vector2 vec2){
        return vec1.x*vec2.y - vec1.y*vec2.x;
    }

    private static Vector2 GetMidPoint(Vector2 pos1, Vector2 pos2){
        return pos1 + (pos2 - pos1)/2f;
    }

    private static Matrix4x4 InitMatrix(){
        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetColumn(0, new Vector4(0, -0.5f, 1, -0.5f));
        matrix.SetColumn(1, new Vector4(1f, 0, -2.5f, 1.5f));
        matrix.SetColumn(2, new Vector4(0f, 0.5f, 2f, -1.5f));
        matrix.SetColumn(3, new Vector4(0, 0, -0.5f, 0.5f));
        return matrix;
    }
}


/*
 public static Vector3[] CubicHermiteInterpolation(Vector3[] vertices){
        Vector3[] interpolatedVertices = new Vector3[vertices.Length - 1];
        Vector4 exponents = new Vector4();
        Vector4 functionValues = new Vector4();
        int index;

        float x, y;
        for(int i = 0; i < vertices.Length - 1; i++){ // the minus 1 is for ignoring the center joint
            // x is halfway between two vertices
            x = vertices[i].x + ((i > vertices.Length - 2 ? vertices[0].x : vertices[i+1].x) - vertices[i].x)/2f;
            for(int j = 0; j < 4; j++){
                exponents[j] = Mathf.Pow(x, j);
                
                index = i - 1 + j;
                if(index < 0){
                    index += vertices.Length - 1;
                }
                else if(i - 1 + j > vertices.Length - 2){
                    index -= vertices.Length - 1; 
                }
             
                functionValues[j] = vertices[index].y;
            }
            y = Vector4.Dot(exponents, (cubicMatrix * functionValues));
            interpolatedVertices[i] = new Vector3(x, y, 0f);
        }
        return interpolatedVertices;
    }

*/
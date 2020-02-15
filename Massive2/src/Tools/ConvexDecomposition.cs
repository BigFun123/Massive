using BulletSharp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Tools
{
  public class ConvexDecomposition
  {
   // StreamWriter output;        
    int mBaseCount = 0;

    public List<ConvexHullShape> convexShapes = new List<ConvexHullShape>();
    public List<Vector3d> convexCentroids = new List<Vector3d>();

    public Vector3d LocalScaling { get; set; }

    public ConvexDecomposition()
    {
     // this.output = output;      

      LocalScaling = new Vector3d(1, 1, 1);
    }

    public void ConvexDecompResult(Vector3d[] hullVertices, int[] hullIndices)
    {
      //if (output == null)
      //  return;

     // output.WriteLine("## Hull Piece {0} with {1} vertices and {2} triangles.", convexShapes.Count, hullVertices.Length, hullIndices.Length / 3);

      //output.WriteLine("usemtl Material{0}", mBaseCount);
      //output.WriteLine("o Object{0}", mBaseCount);

      //foreach (Vector3d p in hullVertices)
     // {
      //  output.WriteLine(string.Format("v {0:F9} {1:F9} {2:F9}", p.X, p.Y, p.Z));
     // }


      // Calc centroid, to shift vertices around center of mass
      Vector3d centroid = Vector3d.Zero;
      foreach (Vector3d vertex in hullVertices)
      {
        centroid += vertex * LocalScaling;
      }
      centroid /= (float)hullVertices.Length;

      List<Vector3d> outVertices = new List<Vector3d>();
      foreach (Vector3d vertex in hullVertices)
      {
        outVertices.Add(vertex * LocalScaling - centroid);
      }

      /*
      // Create TriangleMesh
      var trimesh = new TriangleMesh();
      for (int i = 0; i < hullIndices.Length; i += 3)
      {
        int index0 = hullIndices[i];
        int index1 = hullIndices[i + 1];
        int index2 = hullIndices[i + 2];

        Vector3d vertex0 = hullVertices[index0] * LocalScaling - centroid;
        Vector3d vertex1 = hullVertices[index1] * LocalScaling - centroid;
        Vector3d vertex2 = hullVertices[index2] * LocalScaling - centroid;

        index0 += mBaseCount;
        index1 += mBaseCount;
        index2 += mBaseCount;

        //output.WriteLine("f {0} {1} {2}", index0 + 1, index1 + 1, index2 + 1);
      }
      */

      //this is a tools issue: due to collision margin, convex objects overlap, compensate for it here:
      //#define SHRINK_OBJECT_INWARDS 1
#if SHRINK_OBJECT_INWARDS

            float collisionMargin = 0.01f;

            AlignedVector3Array planeEquations;
            GeometryUtil.GetPlaneEquationsFromVertices(hullVertices, planeEquations);

            AlignedVector3Array shiftedPlaneEquations;
            for (int p=0;p<planeEquations.Count;p++)
            {
                Vector3 plane = planeEquations[p];
                plane[3] += collisionMargin;
                shiftedPlaneEquations.Add(plane);
            }
            AlignedVector3Array shiftedVertices;
            GeometryUtil.GetVerticesFromPlaneEquations(shiftedPlaneEquations, shiftedVertices);
            ConvexHullShape convexShape = new ConvexHullShape(shiftedVertices);
#else //SHRINK_OBJECT_INWARDS

      ConvexHullShape convexShape = new ConvexHullShape(outVertices);
#endif

      convexShape.Margin = 0.01f;
      convexShapes.Add(convexShape);
      convexCentroids.Add(centroid);
      mBaseCount += hullVertices.Length; // advance the 'base index' counter.
    }
    }
  }

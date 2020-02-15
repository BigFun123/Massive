using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MSphericalPatchMesh : MSceneObject
  {
    int m_radius;
    int m_resolution = 3;

    double m_startHorizontalAngle = 0.0;
    double m_endHorizontalAngle = 2 * Math.PI;
    double m_startVerticalAngle = -1 * Math.PI / 2;
    double m_endVerticalAngle = Math.PI / 2;

    public MSphericalPatchMesh() :
      base(EType.Other, "PatchMesh")
    {

    }

    //Point AtAngle(double horizontalAngle, double verticalAngle);



    // The horizontal angle can vary from 0 to 2*PI radians
    public double StartHorizontalAngle
    {
      get { return m_startHorizontalAngle; }
      set { m_startHorizontalAngle = value; }
    }

    public double EndHorizontalAngle
    {
      get { return m_endHorizontalAngle; }
      set { m_endHorizontalAngle = value; }
    }

    // The vertical angle can vary from -PI/2 to PI/2 radians
    public double StartVerticalAngle
    {
      get { return m_startVerticalAngle; }
      set { m_startVerticalAngle = value; }
    }

    public double EndVerticalAngle
    {
      get { return m_endVerticalAngle; }
      set { m_endVerticalAngle = value; }
    }

    public int Radius
    {
      get { return m_radius; }
      set { m_radius = value; }
    }

    protected void CreateGeometry()
    {
      int zmax = m_resolution;
      int rmax = m_resolution;
      double horizontalAngleStep = (m_endHorizontalAngle - m_startHorizontalAngle) / m_resolution;
      double verticalAngleStep = (m_endVerticalAngle - m_startVerticalAngle) / m_resolution;

      List<Vector3d> Vertices = new List<Vector3d>();
      List<Vector3d> Normals = new List<Vector3d>();
      List<int> m_triangles = new List<int>();
      List<Vector2d> TextureCoords = new List<Vector2d>();

      // vertical variation
      for (int zstep = 0; zstep <= zmax; zstep++)
      {
        // the z-coordinate is the projection of the sphere's radius along the z-axis
        double zcoord = m_radius * Math.Sin(m_startVerticalAngle + verticalAngleStep * zstep);
        // the projection on the xy-plane is the radius of the arc in the xy-plane
        double xyproj = m_radius * Math.Cos(m_startVerticalAngle + verticalAngleStep * zstep);

        // horizontal variation
        for (int rstep = 0; rstep <= rmax; rstep++)
        {
          // The resulting x and y coordinate are the projection of the arc's radius on the respective axis.
          double xcoord = xyproj * Math.Cos(m_startHorizontalAngle + horizontalAngleStep * rstep);
          double ycoord = xyproj * Math.Sin(m_startHorizontalAngle + horizontalAngleStep * rstep);

          Vertices.Add(new Vector3d(xcoord, ycoord, zcoord));
          Normals.Add(new Vector3d(xcoord, ycoord, zcoord));
        }
      }

      for (int zstep = 0; zstep < zmax; zstep++)
      {
        for (int rstep = 0; rstep < rmax; rstep++)
        {
          m_triangles.Add(rstep + zstep * (rmax + 1));
          m_triangles.Add(rstep + zstep * (rmax + 1) + 1);
          m_triangles.Add(rstep + zstep * (rmax + 1) + (rmax + 1));

          m_triangles.Add(rstep + zstep * (rmax + 1) + 1);
          m_triangles.Add(rstep + zstep * (rmax + 1) + (rmax + 1) + 1);
          m_triangles.Add(rstep + zstep * (rmax + 1) + (rmax + 1));
        }
      }

      for (int zstep = 0; zstep <= zmax; zstep++)
      {
        for (int rstep = 0; rstep <= rmax; rstep++)
        {
          TextureCoords.Add(new Vector2d(m_startHorizontalAngle + horizontalAngleStep * rstep,
                                                  m_startVerticalAngle + verticalAngleStep * zstep));
        }
      }
    }
    /*
    public Vector2d AtAngle(double horizontalAngle, double verticalAngle)
    {
      double longitude = horizontalAngle * 180 / Math.PI;
      double latitude = verticalAngle * 180 / Math.PI;
      WorldCoordinate wc = new WorldCoordinate();
      wc.Longitude = longitude;
      wc.Latitude = latitude;
      TileCoordinate tile = TileCoordinate.Create(wc, 1);

      double x = ((double)tile.XFraction) / TileCoordinate.Resolution;
      double y = ((double)tile.YFraction) / TileCoordinate.Resolution;

      Vector2d returnValue = new Vector2d();
      returnValue.X = (x == 0.0 && horizontalAngle > m_startLongitude) ? 1.0 : x;
      returnValue.Y = (y == 0.0 && verticalAngle == m_startLatitude) ? 1.0 : y;

      return returnValue;
    }
    */
  }

 
}

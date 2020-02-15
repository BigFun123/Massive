using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Massive.Primitives
{
  class MGrass : MSceneObject
  {
    MShader grassShader;

public string GeoShader = @"
#version 450
layout(points) in;
layout(triangle_strip, max_vertices=4) out;

in vec3 baseColour[];

out vec3 colour;

uniform matrix4 model;
uniform matrix4 mvp;

void main(){
 vec4 offset = vec4(-1, 1.0, 0, 0);
 vec4 vertex_pos = offset + gl_in[0].gl_Position;
 gl_Position = mvp * model * vertex_pos;
 colour = baseColour[0] * vec3(1.0, 0,0);
EmitVertex();

 vec4 offset = vec4(-1.0, -1.0, 0, 0);
 vec4 vertex_pos = offset + gl_in[0].gl_Position;
 gl_Position = mvp * model * vertex_pos;
 colour = baseColour[0] * vec3(1.0, 0,1.0);
EmitVertex();

vec4 offset = vec4(1.0, 1.0, 0, 0);
 vec4 vertex_pos = offset + gl_in[0].gl_Position;
 gl_Position = mvp * model * vertex_pos;
 colour = baseColour[0] * vec3(1.0, 1.0,0);
EmitVertex();

vec4 offset = vec4(1.0, -1.0, 0, 0);
 vec4 vertex_pos = offset + gl_in[0].gl_Position;
 gl_Position = mvp * model * vertex_pos;
 colour = baseColour[0] * vec3(0.0, 0,1.0);
EmitVertex();
 
EndPrimitive();

}

";

    public MGrass() : base(EType.Grass, "Grass")
    {
      grassShader = new MShader("GrassShader");
    }

    public override void Setup()
    {
      base.Setup();
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      base.Render(viewproj, parentmodel);
    }
  }
}

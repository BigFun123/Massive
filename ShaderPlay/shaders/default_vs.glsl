#version 450 core

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 texcoords;

uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;

uniform float time;
out float vs_time;

out vec3 vs_FragPosition;
out vec4 vs_color;
out vec2 vs_texcoord;
out vec3 vs_normal;

void main(void)
{
	//pass data to the fragment shader
	vs_color = vec4(1, 1, 1, 1);
	vs_texcoord = texcoords;
	vs_time = time;
	vs_normal = normal;
	vs_FragPosition = vec3(model * vec4(position, 1.0));
	//the screen position range(-1,-1 to 1,1) of the vertex
	gl_Position = projection * view * model * vec4(position, 1);
}
#version 430 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec2 texcoords;

out vec3 normal;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
	mat3 normalMatrix = mat3(transpose(inverse(view * model)));
	normal = normalize(vec3(projection * vec4(normalMatrix * aNormal, 0.0)));	
	gl_Position = projection * view * model * vec4(aPos, 1.0);
}
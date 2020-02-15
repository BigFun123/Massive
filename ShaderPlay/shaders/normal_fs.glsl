#version 430 core
out vec4 FragColor;

in vec3 normal;

void main()
{
	FragColor = vec4(1.0, 1.0, 0.0, 1.0);
	//FragColor = vec4(normal*2, 1.0);
}

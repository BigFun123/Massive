#version 450 core

out vec4 color;

in vec4 vs_color;
in vec3 vs_normal;
in vec2 vs_texcoord;
in vec3 vs_FragPosition;
uniform  vec3 lightColor = vec3(1,1,1);
uniform float ambientStrength = 0.12;

in float vs_time;
uniform vec3 lightPos;
uniform sampler2D texturemap1;
uniform sampler2D texturemap2;

void main(void)
{
	// ambient	
	vec3 ambient = ambientStrength * lightColor;

	vec3 norm = normalize(vs_normal);
	vec3 lightDir = normalize(lightPos - vs_FragPosition);

	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;

	vec4 result = (vec4(ambient,1) + vec4(diffuse,1.0)) * vs_color;
	color = result;

	color *= mix(texture(texturemap1, vs_texcoord), texture(texturemap2, vs_texcoord), 0.2);	
}
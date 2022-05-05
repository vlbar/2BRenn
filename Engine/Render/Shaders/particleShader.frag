#version 330 core

in vec2 uv;
in vec4 color;

out vec4 outColor;

struct DirectionalLight
{
    vec3 color;
    float intensity;
    vec3 direction;
    float diffuseIntensity;
};
 
uniform DirectionalLight directionalLight;
uniform sampler2D texture0;

void main()
{
	outColor = vec4(directionalLight.color, 1.0) * (directionalLight.intensity + directionalLight.diffuseIntensity) * texture(texture0, uv) * color;
}
#version 330 core

in vec2 uv;
in vec4 color;

out vec4 outColor;

struct BaseLight                                                                    
{                                                                                   
    vec3 color;                                                                     
    float ambientIntensity;                                                         
    float diffuseIntensity;                                                         
}; 

struct DirectionalLight
{
    BaseLight light;
    vec3 direction;
};
 
uniform DirectionalLight directionalLight;
uniform sampler2D texture0;

void main()
{
	outColor = vec4(directionalLight.light.color, 1.0) * 
        (directionalLight.light.ambientIntensity + directionalLight.light.diffuseIntensity) * 
        texture(texture0, uv) * color;
}
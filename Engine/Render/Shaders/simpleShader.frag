#version 330 core

#define MAX_POINT_LIGHTS 2

in vec2 uv;
in vec3 normal;
in vec3 fragPos;

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

struct PointLight
{
    BaseLight light;
    vec3 position;
    float constantAttenuation;
    float linearAttenuation;
    float quadraticAttenuation;
};

uniform DirectionalLight directionalLight;
uniform PointLight pointLights[MAX_POINT_LIGHTS];
uniform int pointLightsCount;
uniform sampler2D texture0;
uniform vec4 baseColor;
uniform vec2 offset;
uniform vec2 tiling;

vec4 CalcLight(BaseLight light, vec3 direction, vec3 normal)
{
    vec4 ambientColor = vec4(light.color, 1.0f) * light.ambientIntensity;
    float diffuseFactor = max(dot(normal, -direction), 0.0f);
    vec4 diffuseColor = vec4(light.color, 1.0f) * light.diffuseIntensity * diffuseFactor;
    return (ambientColor + diffuseColor);
}

vec4 CalcDirectionalLight(vec3 normal)                                                      
{                                                                                           
    return CalcLight(directionalLight.light, directionalLight.direction, normal); 
} 

vec4 CalcPointLight(int index, vec3 normal)
{
    vec3 lightDirection = fragPos - pointLights[index].position;
    float lightDistance = length(lightDirection);
    lightDirection = normalize(lightDirection);
 
    vec4 color = CalcLight(pointLights[index].light, lightDirection, normal);
    float attenuation =  pointLights[index].constantAttenuation +
                 pointLights[index].linearAttenuation * lightDistance +
                 pointLights[index].quadraticAttenuation * lightDistance * lightDistance;
 
    return color / attenuation;
}

void main()
{
    vec4 ambientColor = texture(texture0, uv * tiling + offset) * baseColor;
    vec4 totalLight = CalcDirectionalLight(normal);
    
    for (int i = 0 ; i < pointLightsCount; i++) {                                           
        totalLight += CalcPointLight(i, normal);                                            
    }

    outColor = ambientColor * vec4(totalLight.rgb, 1.0);
}
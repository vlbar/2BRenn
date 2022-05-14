#version 330 core

#define MAX_POINT_LIGHTS 2

in vec2 uv;
in vec3 normal;
in vec3 fragPos;
in vec4 fragPosLightSpace;

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
uniform sampler2D shadowMap;
uniform vec4 baseColor;
uniform vec2 offset;
uniform vec2 tiling;

float ShadowCalculation(vec4 fragPosLightSpace)
{
    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
    projCoords = projCoords * 0.5 + 0.5;
    float closestDepth = texture(shadowMap, projCoords.xy).r;
    float currentDepth = projCoords.z;
    float bias = 0.005;

    if(projCoords.z > 1.0)
        return 1;

    float shadow = 0.0;
    vec2 texelSize = 1.0 / textureSize(shadowMap, 0);
    for(int x = -1; x <= 1; ++x)
    {
        for(int y = -1; y <= 1; ++y)
        {
            float pcfDepth = texture(shadowMap, projCoords.xy + vec2(x, y) * texelSize).r;
            shadow += currentDepth - bias > pcfDepth ? 0.5 : 0.0;
        }
    }
    shadow /= 9.0;

    return 1 - shadow;
}

vec4 CalcLight(BaseLight light, vec3 direction, vec3 normal)
{
    vec4 ambientColor = vec4(light.color, 1.0f) * light.ambientIntensity;
    float diffuseFactor = max(dot(normal, -direction), 0.0f);
    vec4 diffuseColor = vec4(light.color, 1.0f) * light.diffuseIntensity * diffuseFactor;
    float shadow = ShadowCalculation(fragPosLightSpace);
    return shadow * (ambientColor + diffuseColor);
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
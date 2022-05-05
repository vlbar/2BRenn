#version 330 core

in vec2 uv;
in vec3 normal;
in vec3 fragPos;

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
uniform vec4 baseColor;
uniform vec2 offset;
uniform vec2 tiling;

void main()
{
    vec3 ambientColor = (texture(texture0, uv * tiling + offset) * baseColor).rgb;

    // ambient
    vec3 ambient = directionalLight.color * directionalLight.intensity * ambientColor;
    
    // diffuse
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(-directionalLight.direction);
    float diff = max(dot(norm, lightDir), 0.0f);
    vec3 diffuse = directionalLight.diffuseIntensity * diff * ambientColor;
        
    vec3 result = ambient + diffuse;
    outColor = vec4(result, 1.0f);
}
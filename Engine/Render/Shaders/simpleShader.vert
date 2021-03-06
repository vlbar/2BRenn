#version 330 core

layout (location = 0) in vec3 aVertexPos;
layout (location = 1) in vec2 aTexCoords;
layout (location = 2) in vec3 aNormal;

out vec2 uv;
out vec3 normal;
out vec3 fragPos;
out vec4 fragPosLightSpace;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat4 lightSpaceMatrix;

void main()
{
    uv = aTexCoords;
    normal = mat3(transpose(inverse(model))) * aNormal;
    fragPos = vec3(model * vec4(aVertexPos, 1.0f));
    fragPosLightSpace = lightSpaceMatrix * model * vec4(aVertexPos, 1.0);
    gl_Position = projection * view * vec4(fragPos, 1.0f);
}
#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
in vec2 TexCoord;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D texture4;
uniform sampler2D texture5;

uniform bool useTexture;
uniform float ambientReflection;
uniform float diffuseReflection;
uniform float specularReflection;
uniform int specularExponent;

void main()
{
    // Ambient lighting
    float ambientStrength = ambientReflection;
    vec3 ambient = ambientStrength * vec3(1.0, 1.0, 1.0);

    // Diffuse lighting
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(vec3(0.0, 0.0, 1.0));
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * diffuseReflection * vec3(1.0, 1.0, 1.0);

    // Specular lighting
    float specularStrength = specularReflection;
    vec3 viewDir = normalize(vec3(0.0, 0.0, 1.0));
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), specularExponent);
    vec3 specular = specularStrength * spec * vec3(1.0, 1.0, 1.0);

    vec3 result = (ambient + diffuse + specular) * vec3(1.0, 1.0, 1.0);

    if (useTexture) {
        vec4 textureColor1 = texture(texture1, TexCoord);
        vec4 textureColor2 = texture(texture2, TexCoord);
        vec4 textureColor3 = texture(texture3, TexCoord);
        vec4 textureColor4 = texture(texture4, TexCoord);
        vec4 textureColor5 = texture(texture5, TexCoord);

        FragColor = textureColor1 * textureColor2 * textureColor3 * textureColor4 * textureColor5 * vec4(result, 1.0);
    } else {
        FragColor = vec4(0.5f, 0.5f, 0.5f, 1.0) * vec4(result, 1.0);
    }
}
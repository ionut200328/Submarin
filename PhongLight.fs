#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos; 
uniform vec3 viewPos; 
uniform vec3 lightColor;
uniform vec3 objectColor;

// Adăugați o variabilă uniformă pentru constanta de reflexie ambientală
uniform float ambientReflection;
// Adăugați o variabilă uniformă pentru constanta de reflexie difuză
uniform float diffuseReflection;
// Adăugați variabile uniforme pentru reflexia speculară
uniform float specularReflection;
// Adăugați o variabilă uniformă pentru exponentul specular
uniform int specularExponent;


void main()
{
    // Calculați iluminarea ambientală
    vec3 ambient = ambientReflection * lightColor;

    // Calculați difuzia
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * diffuseReflection * lightColor;

    // Calculați iluminarea speculară
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), specularExponent);
    vec3 specular = specularReflection * spec * lightColor;

    // Calculați culoarea finală utilizând componentele ambientale, difuze și speculară
    vec3 result = (ambient + diffuse + specular) * objectColor;

    FragColor = vec4(result, 1.0);
}


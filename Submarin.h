#pragma once 
#include<assimp/ai_assert.h>
#include<assimp/scene.h>
#include<assimp/postprocess.h> 
#include<assimp/AssertHandler.h>
#include<assimp/Importer.hpp>
#include<string>
#include <stdlib.h> // necesare pentru citirea shader-elor
#include <stdio.h>
#include <math.h> 
#include <random>
#include <GL/glew.h>
#include <glm/GLM.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <GLFW/glfw3.h>
#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include<assimp/mesh.h>
#include<assimp/config.h>
#include<assimp/scene.h>
#pragma comment (lib, "glfw3dll.lib")
#pragma comment (lib, "glew32.lib")
#pragma comment (lib, "OpenGL32.lib")
#pragma comment (lib, "assimp-vc143-mtd.lib")
using namespace std;
#include "Submarin.h"

struct Vertex {
    glm::vec3 position;
    glm::vec3 normal;
    glm::vec2 texCoords;
};

class Submarin {
private:
    const aiScene* scene = nullptr;

    GLuint programID;  // Shader program ID
    GLuint vaoID;      // Vertex Array Object ID
    GLuint vboID;      // Vertex Buffer Object ID
    GLuint textureID;  // Texture ID

public:
    bool LoadMesh(const string& Filename) 
    {
        Assimp::Importer importer;

        unsigned int flags = aiProcess_Triangulate | aiProcess_GenSmoothNormals | aiProcess_FlipUVs;

        scene = importer.ReadFile(Filename.c_str(), flags);

        if (!scene || scene->mFlags & AI_SCENE_FLAGS_INCOMPLETE || !scene->mRootNode) {
            cerr << "Assimp error: " << importer.GetErrorString() << endl;
            return false;
        }

        programID = LoadShaders("submarin.vs", "submarin.fs");
        if (programID == 0)
            return false;

        // Assuming the model has only one mesh for simplicity
        aiMesh* mesh = scene->mMeshes[0];

        // Generate and bind Vertex Array Object
        glGenVertexArrays(1, &vaoID);
        glBindVertexArray(vaoID);

        // Generate and bind Vertex Buffer Object
        glGenBuffers(1, &vboID);
        glBindBuffer(GL_ARRAY_BUFFER, vboID);

        // Fill VBO with vertex data
        vector<Vertex> vertices;
        for (unsigned int i = 0; i < mesh->mNumVertices; i++) {
            Vertex vertex;
            // Assuming you have position, normals, and texture coordinates
            vertex.position = glm::vec3(mesh->mVertices[i].x, mesh->mVertices[i].y, mesh->mVertices[i].z);
            vertex.normal = glm::vec3(mesh->mNormals[i].x, mesh->mNormals[i].y, mesh->mNormals[i].z);
            if (mesh->mTextureCoords[0]) {
                vertex.texCoords = glm::vec2(mesh->mTextureCoords[0][i].x, mesh->mTextureCoords[0][i].y);
            }
            else {
                vertex.texCoords = glm::vec2(0.0f, 0.0f);
            }
            vertices.push_back(vertex);
        }

        glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(Vertex), &vertices[0], GL_STATIC_DRAW);

        // Specify vertex attributes
        glEnableVertexAttribArray(0);
        glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)0);

        glEnableVertexAttribArray(1);
        glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, normal));

        glEnableVertexAttribArray(2);
        glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, texCoords));

        // Load textures (Assuming one texture for simplicity)
        glGenTextures(1, &textureID);
        glBindTexture(GL_TEXTURE_2D, textureID);

        // Load and set texture parameters

        // Unbind VAO, VBO, and texture
        glBindVertexArray(0);
        glBindBuffer(GL_ARRAY_BUFFER, 0);
        glBindTexture(GL_TEXTURE_2D, 0);

        return true;
    }
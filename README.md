# Proyecto Unity - Carga de Modelos OBJ, Normales, Cámaras y Matrices

Este proyecto fue desarrollado en Unity como parte de un trabajo práctico orientado a comprender el funcionamiento interno del renderizado 3D.  
El objetivo principal es analizar cómo se cargan modelos `.obj`, cómo se representan sus vértices y caras, cómo intervienen las matrices de transformación, vista y proyección en la visualización de una escena, implementar nuestras propias cámaras y experimentar con shaders.

## Objetivos del proyecto

- Cargar modelos 3D desde archivos `.obj`.
- Parsear manualmente vértices y caras del modelo.
- Construir una malla en Unity a partir de los datos leídos.
- Implementar y probar matrices de transformación, vista y proyección.
- Crear un sistema básico de cámara para visualizar la escena.
- Generar una escena simple con objetos, paredes y modelos cargados.
- Experimentar con shaders personalizados.
- Simular una especie de sombreado.

## Estructura del proyecto

El proyecto está organizado en distintas carpetas para separar responsabilidades:

Assets/
│
├── Loader/
│   └── Contiene las clases encargadas de leer, interpretar y cargar archivos .obj.
│
├── Camara/
│   └── Contiene scripts relacionados con el sistema de cámara, vista y proyección.
│
├── Estructura/
│   └── Contiene scripts para generar elementos de la escena, como paredes, pisos o estructuras. (Aunque luego implementamos estas estructuras con nuestro loader desd el   inspector como .obj)
│
├── Modelado/
│   └── Contiene modelos, objetos 3D y recursos relacionados con la geometría.
│
├── Shaders/
│   └── Contiene los shaders personalizados utilizados para experimentar con color, iluminación y matrices.
│
└── Adicionales/
    └── Contiene recursos complementarios utilizados en el proyecto.
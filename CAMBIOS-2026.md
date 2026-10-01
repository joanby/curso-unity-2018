# Cambios de la rama `update-2026`

> Esta rama es el mismo curso —los cursos de Unity 2018 (partes 1 a 5)—, preparada para abrirse con **Unity 6**. La rama
> principal sigue exactamente como en el vídeo.
> Revisado contra el código fuente de Unity 6, pero todavía no se ha abierto en el editor. Si algo no abre o no compila, cuéntalo en la comunidad del curso.

## Cómo usarla

1. Instala **Unity 6** (la versión LTS que te ofrezca Unity Hub).
2. Descarga solo esta rama: `git clone --depth 1 -b update-2026 https://github.com/joanby/curso-unity-2018` (o, en GitHub, cambia a la
   rama `update-2026` y *Code → Download ZIP*). Sin el `--depth 1`, git se trae también el historial
   de la rama principal, con la caché antigua dentro.
3. En Unity Hub, **Add → Add project from disk** y elige la carpeta de un proyecto (`Awakening`, `DefendTheHive`, `MyFirst3DProject`, `Pocman`, `Retro2018`, `Super JB Bross`, `Tetris`),
   no la raíz del repositorio.
4. Unity te avisará de que el proyecto es de una versión anterior: acepta la actualización.

## Qué ha cambiado y por qué

### La caché de Unity ya no está en el repositorio

La rama principal guarda en git la caché de Unity (`Library/`, `Logs/`, `obj/`…): **55840 de los 62587 ficheros**. Unity la regenera al abrir el proyecto y con Unity 6 se reconstruye
entera, así que solo hacía la descarga enorme. En esta rama no está, y el `.gitignore` evita que vuelva.
**No cambia nada de lo que ves en el vídeo:** escenas, scripts, modelos, materiales y ajustes siguen ahí.

### Scripts de utilidad que no compilan en Unity 6 y nadie usaba

Usan APIs que Unity ha eliminado (`GUIText`, `GUITexture`…). Ninguna escena, prefab ni script
del curso los usa, así que se han quitado para que el proyecto compile:

- `Awakening`: `ForcedReset.cs`, `SimpleActivatorMenu.cs`
- `DefendTheHive`: `ForcedReset.cs`, `SimpleActivatorMenu.cs`
- `MyFirst3DProject`: `ForcedReset.cs`, `SimpleActivatorMenu.cs`

### Awakening (el RPG): sin la parte final de multijugador

El curso del RPG termina con una introducción al multijugador hecha con **UNet**, el sistema de red
antiguo de Unity, que Unity eliminó del motor. Mientras esos scripts estén en un proyecto, en Unity 6
**no compila nada**, tampoco el resto del juego. En esta rama se han quitado la carpeta `Lobby` y sus
dos escenas (`Game Lobby` y `Networking Game Scene`); el código del RPG no depende de ellas.

- **Las lecciones de multijugador del final** solo se pueden seguir con la rama principal y Unity 2018.
- **Los build settings** del proyecto solo tenían las dos escenas de red: ahora tienen las tres del RPG
  (`Main Menu`, `Character Selection` y `Awakening`), que el juego carga por nombre.

### El código del curso no se ha tocado

Los scripts que escribimos en el vídeo están igual. Lo que puedes ver en la consola de Unity 6:

- `Awakening`: .drag (hoy linearDamping; lo cambia el API Updater); .velocity (en Rigidbody, hoy linearVelocity; lo cambia el API Updater); FindObjectOfType (obsoleto: aviso amarillo); propiedades antiguas de ParticleSystem (obsoletas)
- `DefendTheHive`: .drag (hoy linearDamping; lo cambia el API Updater); .velocity (en Rigidbody, hoy linearVelocity; lo cambia el API Updater); FindObjectOfType (obsoleto: aviso amarillo); propiedades antiguas de ParticleSystem (obsoletas)
- `MyFirst3DProject`: .drag (hoy linearDamping; lo cambia el API Updater); .velocity (en Rigidbody, hoy linearVelocity; lo cambia el API Updater); FindObjectOfType (obsoleto: aviso amarillo); propiedades antiguas de ParticleSystem (obsoletas)
- `Retro2018`: .velocity (en Rigidbody, hoy linearVelocity; lo cambia el API Updater)
- `Super JB Bross`: .velocity (en Rigidbody, hoy linearVelocity; lo cambia el API Updater); atajos de Unity 4 (rigidbody., renderer.…): los reescribe el API Updater
- `Tetris`: FindObjectOfType (obsoleto: aviso amarillo)

Son avisos (amarillos) o cambios que Unity hace solo al abrir el proyecto: el juego funciona igual.

## Lo que se ve distinto al vídeo

La interfaz del editor: Unity 6 ha movido y rediseñado paneles y menús. Lo que aprendes en el
curso (componentes, físicas, scripts, escenas) es exactamente lo mismo.

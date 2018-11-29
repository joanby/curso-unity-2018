using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyPlayerController : NetworkBehaviour
{

    //Variables propias del player, que no necesitan ser sincronizadas
    public Transform mainCamera; //Variable para referenciar a la cámara del jugador
    public float cameraDistance = 15f; //Distancia del jugador a la cámara
    public float cameraHeight = 15f; //Altura de la cámara por encima del jugador
    public Vector3 cameraOffset; //Delay entre la cámara y el jugador 

    //Variables del player que necesitan ser sincronizadas con el resto de players
    [SyncVar]
    public Color myColor;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        //Teñimos el tanque del color seleccionado...
        foreach(Transform child in transform){
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().material.color = myColor;
            }
        }

        cameraOffset = new Vector3(0, cameraHeight, -cameraDistance);
        mainCamera = Camera.main.transform;

        MoveCamera();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        //Si llegamos aquí, seguro que el tanque que estamos manejando es el nuestro (player local)
#if UNITY_EDITOR
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 120.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
#else
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 120f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime*3.0f;
#endif
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFireBullet();
        }
#else
        if(Input.GetButtonDown("Fire1")){
        CmdFireBullet();
        }
#endif
        MoveCamera();
    }


    void MoveCamera(){
        //Ponemos la cámara en la posición y la rotación del tanque
        mainCamera.position = transform.position;
        mainCamera.rotation = transform.rotation;
        //La trasladamos con el vector offset para atrás
        mainCamera.Translate(cameraOffset);
        //Hacemos que mire al tanque
        mainCamera.LookAt(transform);
    }

    [Command]
    void CmdFireBullet(){
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10f;
        bullet.GetComponent<Bullet>().myColor = myColor;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }
}

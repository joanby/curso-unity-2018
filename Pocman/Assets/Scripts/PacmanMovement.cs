using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {

    public float speed = 0.4f;

    public Vector2 destination = Vector2.zero;

	void Start () {
        destination = this.transform.position;
	}
	
	void FixedUpdate () {
        //Calculamos el nuevo punto donde hay que ir en base a la variable destino
        Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed*Time.deltaTime);
        //Usamos el rigidbody para transportar a Pacman hasta dicha posición
        GetComponent<Rigidbody2D>().MovePosition(newPos);


        float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);
        Debug.Log(distanceToDestination);

        if(distanceToDestination < 2.0f){
            
            if(Input.GetKey(KeyCode.UpArrow) && CanMoveTo(Vector2.up))
            {
                destination = (Vector2)this.transform.position + Vector2.up;
            }

            if(Input.GetKey(KeyCode.RightArrow) && CanMoveTo(Vector2.right))
            {
                destination = (Vector2)this.transform.position + Vector2.right;
            }

            if(Input.GetKey(KeyCode.DownArrow) && CanMoveTo(Vector2.down))
            {
                destination = (Vector2)this.transform.position + Vector2.down;
            }

            if(Input.GetKey(KeyCode.LeftArrow) && CanMoveTo(Vector2.left))
            {
                destination = (Vector2)this.transform.position + Vector2.left;
            }
        }


        Vector2 dir = destination - (Vector2)this.transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

    //Método que dada una posible dirección de movimiento
    //devuelve true si podemos ir en dicha dirección
    //y false si algo nos impide avanzar
    bool CanMoveTo(Vector2 dir){
        Vector2 pacmanPos = this.transform.position;
        //Trazamos una línea del objetivo donde quiero ir hacia Pacman
        RaycastHit2D hit = Physics2D.Linecast(pacmanPos+dir, pacmanPos);

        Debug.DrawLine(pacmanPos, pacmanPos+dir);


        Collider2D pacmanCollider = GetComponent<Collider2D>();
        Collider2D hitCollider = hit.collider;

        if(hitCollider == pacmanCollider){
            //no tengo nada más enmedio -> puedo moverme allí
            return true;
        }else{
            //tengo un collider delante que NO es el de pacman -> no puedo moverme allí
            return false;
        }
    }
}

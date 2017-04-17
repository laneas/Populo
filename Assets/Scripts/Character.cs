using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Utility variables
    public GameObject model;
    public Node objective;
    public List<Node> path = new List<Node>();

    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Animator an;

    private Vector2 currentVector;
    private Vector2 previousVector;

    public bool isAttacking = false;

    //Characteristic variables
    public int hp  = 0; //Hit Points: how much damage a character can take before dying
    public int def = 0; //Defense: how much physical damage a character resists
    public int atk = 0; //Attack: how much physical damage a character deals
    public int mgk = 0; //Magic: how much magical damage a character deals
    public int wil = 0; //Will: how much magical damage a character resists
    public int spd = 1; //Speed: how fast a character can move and attack
    public int rng = 1; //Range: how far a character's attack can reach

    // Use this for initialization
	protected void Start ()
    {
        //path = new List<Node>();
        an = model.GetComponent<Animator>();
        rb = model.GetComponent<Rigidbody2D>();
        cc = model.GetComponent<CircleCollider2D>();
        an.Play("idleSouth");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (path.Count != 0)
        {
            objective = path[0];
            move();
        }
    }

    /*
     * Given a Node, objective, move() will give the model's RigidBody a velocity
     * Calls playAnimation if velocity changes from the previous frame
     * Should only be called by FixedUpdate()
     * Don't fuck with this 
     */
    private void move()
    {
        float currentX = model.transform.position.x;
        float currentY = model.transform.position.y;
        float xVel = 0;
        float yVel = 0;
        float deltaX = 0;
        float deltaY = 0;
        if (objective != null)
        {
           deltaX = Mathf.Abs(currentX - objective.x);
           deltaY = Mathf.Abs(currentY - objective.y);
        }

        if (deltaX < .1 && deltaY < .1)
        {
            objective = null;
            path.Remove(path[0]);
        }

        if (objective != null)
        {
            if (currentX < objective.x) { xVel = .5f; }
            else if (currentX > objective.x) { xVel = -.5f; }
            else { xVel = 0; }
            if (currentY < objective.y) { yVel = .5f; }
            else if (currentY > objective.y) { yVel = -.5f; }
            else { yVel = 0; }
        }

        previousVector = currentVector;
        currentVector = new Vector2(xVel, yVel);
        rb.velocity = currentVector;
        if (previousVector != currentVector)
        {
            playAnimation(deltaX, deltaY);
        }
    }

    /**
     * Should only be called by move()
     * Only called if RigidBody's velocity changes
     * Animation changes if it is within threshold (deltaX, or deltaY)
     * Don't fuck with this either
     */
    private void playAnimation(float deltaX, float deltaY)
    {
        if (currentVector.x == 0 && currentVector.y == 0)
        {
            an.Play("idleSouth");
        }
        else
        {
            if (currentVector.x > 0 && deltaX > .1)
            {
                an.Play("walkEast");
            }
            else if (currentVector.x < 0 && deltaX > .1)
            {
                an.Play("walkWest");
            }
            else if (currentVector.y > 0 && deltaY > .1)
            {
                an.Play("walkNorth");
            }
            else if (currentVector.y < 0 && deltaY > .1)
            {
                an.Play("walkSouth");
            }
        }
    }

    public void attack(Character character)
    {
        if (!isAttacking)
        {
            IEnumerator attack = attackPrep(character);
            StartCoroutine(attack);
        }
    }

    public IEnumerator attackPrep(Character character)
    {
        while (true)
        {
            isAttacking = true;
            yield return new WaitForSeconds((float)spd);
            int phyDamage = this.atk - character.def;
            if (phyDamage <= 0) { phyDamage = 1; }
            int mgkDamage = this.mgk - character.wil;
            if (mgkDamage < 0) { mgkDamage = 0; }
            character.hp = character.hp - (phyDamage + mgkDamage);
            isAttacking = false;
        }
    }

    public abstract void select();
    public abstract void die();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Utility variables
    public GameObject model;
    public Node objective;
    public List<Node> path = new List<Node>();
    public List<IEnumerator> attacks = new List<IEnumerator>();

    public Rigidbody2D rb;
    public CircleCollider2D cc;
    public Animator an;

    private Vector2 currentVector;
    private Vector2 previousVector;
    public float lastX = 0;
    public float lastY = 0;
    public float curX = 0;
    public float curY = 0;

    public int checkCounter = 0;
    public bool checkingDifference = false;
    public bool isAttacking = false;
    public bool isMoving = false;

    //Characteristic variables
    public string type;
    public int mhp = 0; //Max Hit Points: how much health a character can have in total
    public int hp = 0; //Hit Points: how much damage a character can take before dying
    public int def = 0; //Defense: how much physical damage a character resists
    public int atk = 0; //Attack: how much physical damage a character deals
    public int mgk = 0; //Magic: how much magical damage a character deals
    public int wil = 0; //Will: how much magical damage a character resists
    public int spd = 1; //Speed: how fast a character can move and attack
    public int rng = 1; //Range: how far a character's attack can reach
    public int xp = 0;
    public int freePoints = 0;

    // Use this for initialization
    protected void Start()
    {
        //path = new List<Node>();
        an = model.GetComponent<Animator>();
        rb = model.GetComponent<Rigidbody2D>();
        cc = model.GetComponentInChildren<CircleCollider2D>();
        curX = model.transform.position.x;
        curY = model.transform.position.y;
        an.Play("idleSouth");
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (path.Count != 0)
        {
            objective = path[0];
            move();
        }
        findMoveDifference();
    }

    public void addXP(int xp)
    {
        this.xp += xp;
        if (xp >= 100)
        {
            int freePoints = UnityEngine.Random.Range(1, 2 * wil);
            mhp += 5;
            atk += 1;
            def += 1;
            mgk += 1;
            wil += 1;
            hp = mhp;
            xp = xp - 100;
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
            isMoving = false;
            objective = null;
            path.Remove(path[0]);
        }

        if (objective != null)
        {
            isMoving = true;
            if (currentX < objective.x) { xVel = 2f; }
            else if (currentX > objective.x) { xVel = -2f; }
            else { xVel = 0; }
            if (currentY < objective.y) { yVel = 2f; }
            else if (currentY > objective.y) { yVel = -2f; }
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

    public void checkIfStuck()
    {
        float deltaX = Mathf.Abs(curX - lastX);
        //Debug.Log("" + deltaX);
        float deltaY = Mathf.Abs(curY - lastY);
        //Debug.Log("" + deltaY);
        if (deltaX < .01)
        {
            //Debug.Log("Horizontal movement blocked");
            path.Insert(0, new Node(objective.x, objective.y + 2, true));
            //model.transform.position = new Vector3(objective.)
        }
        if (deltaY < .01)
        {
            //Debug.Log("Vertical movement blocked");
            rb.velocity = new Vector2(1, 0);
            path.Insert(0, (new Node(objective.x + 2, objective.y, true)));
        }
        if (path.Count > 3)
        {
            path.RemoveAt(1);
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
            attacks.Add(attack);
            StartCoroutine(attack);
        }
    }

    public IEnumerator attackPrep(Character character)
    {
        //while (true)
        //{
            isAttacking = true;
            yield return new WaitForSeconds((float)spd);
            int phyDamage = this.atk - character.def;
            if (phyDamage <= 0) { phyDamage = 1; }
            int mgkDamage = this.mgk - character.wil;
            if (mgkDamage < 0) { mgkDamage = 0; }
            character.hp = character.hp - (phyDamage + mgkDamage);
            addXP(10);
            GameObject blood = Instantiate(Resources.Load("Prefabs/Blood", typeof(GameObject))) as GameObject;
            blood.transform.position = new Vector3(character.curX, character.curY, blood.transform.position.z);
        blood.GetComponent<ParticleSystem>().IsAlive(true);
        isAttacking = false;
        //}
    }

    public void stopAttacks()
    {
        foreach (IEnumerator attack in attacks)
        {
            StopCoroutine(attack);
        }
        attacks.Clear();
    }

    public void findMoveDifference()
    {

        IEnumerator find = prepFindMoveDifference();
        StartCoroutine(find);
    }

    public IEnumerator prepFindMoveDifference()
    {
        if (!checkingDifference)
        {
            checkCounter++;
            checkingDifference = true;
            yield return new WaitForSeconds(0.5f);
            if (checkCounter % 2 == 0)
            {
                curX = model.transform.position.x;
                curY = model.transform.position.y;
                if (isMoving)
                {
                    checkIfStuck();
                }
                //Debug.Log("Current: " + model.transform.position.x + " " + model.transform.position.y);
            }
            else
            {
                lastX = model.transform.position.x;
                lastY = model.transform.position.y;
                //Debug.Log("Previous: " + model.transform.position.x + " " + model.transform.position.y);
            }
            
            checkingDifference = false;
        }
    }

    public abstract void select();
    public abstract void die();
}
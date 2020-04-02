using UnityEngine;
using System.Collections;


public class PlayerAnim
{
    public Animation anim;

    public AnimationClip Idle;
    public AnimationClip Attack01;
    public AnimationClip Dance;
    public AnimationClip Damage;
    public AnimationClip Talk;
    public AnimationClip Dead;
    public AnimationClip Idle1;
    public AnimationClip Walk;


}
public class Merchant_ani : MonoBehaviour
{

    public PlayerAnim PlayerAnim;

    [HideInInspector]
    public Animation anim;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();

        anim.Play();

    }

    // Update is called once per frame
    void Update()
    {





        if (Input.GetKeyDown("1"))
        {
            anim.Play("Attack01");
        }
        {


            if (Input.GetKeyDown("2"))
            {
                anim.Play("Damage");
            }
        }
        {


            if (Input.GetKeyDown("3"))
            {
                anim.Play("Dead");
            }
        }
        {


            if (Input.GetKeyDown("4"))
            {
                anim.Play("Idle");
            }
        }
        {


            if (Input.GetKeyDown("5"))
            {
                anim.Play("Idle1");
            }
        }
        {


            if (Input.GetKeyDown("6"))
            {
                anim.Play("Talk");
            }
        }
        {


            if (Input.GetKeyDown("7"))
            {
                anim.Play("Walk");
            }
        }
        {


            if (Input.GetKeyDown("8"))
            {
                anim.Play("Dance");
            }
        }
      
        }
    }



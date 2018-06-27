using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    private Animator anim;

    private int speedId = Animator.StringToHash("Speed");
    private int isSpeedUpId = Animator.StringToHash("IsSpeedUp");
    private int horizontalId = Animator.StringToHash("Horizontal");

    private int speedRotateId = Animator.StringToHash("SpeedRotate");
    private int speedZId = Animator.StringToHash("SpeedZ");

    private int colliderId = Animator.StringToHash("Colider");

    private bool isSpeedUp = false;

    private int vaultId = Animator.StringToHash("Vault");
    private int sliderId = Animator.StringToHash("Slider");


    private Vector3 matchTarget = Vector3.zero;

    private CharacterController characterController;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        anim.SetFloat(speedZId, Input.GetAxis("Vertical") * 4.155063f);
        anim.SetFloat(speedRotateId, Input.GetAxis("Horizontal") * 126.1101f);

        //anim.SetFloat(speedId, (Input.GetAxis("Vertical") * 4.2f));

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    isSpeedUp = true;
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    isSpeedUp = false;
        //}
        //anim.SetBool(isSpeedUpId, isSpeedUp);

        //anim.SetFloat(horizontalId, Input.GetAxis("Horizontal"));

        ProcessVault();

        ProcessSlider();

        characterController.enabled = anim.GetFloat(colliderId) < 0.5;


    }

    private void ProcessVault()
    {
        bool isVault = false;

        if (anim.GetFloat(speedZId) > 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("LocolMotion"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.3f, transform.forward, out hit, 4.8f))
            {
                if (hit.collider.tag == "Obstacle")
                {
                    if (hit.distance > 3)
                    {
                        isVault = true;

                        // 取得目标位置
                        matchTarget = hit.point;
                        matchTarget.y = hit.collider.transform.position.y + hit.collider.bounds.size.y + 0.07f;
                    }

                }
            }
        }

        anim.SetBool(vaultId, isVault);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Valut") && anim.IsInTransition(0) == false)
        {
            anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.38f, 0.50f);
        }
    }

    private void ProcessSlider()
    {
        bool isSlider = false;

        if (anim.GetFloat(speedZId) > 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("LocolMotion"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward, out hit, 3f))
            {
                if (hit.collider.tag == "Obstacle")
                {
                    if (hit.distance > 2)
                    {
                        isSlider = true;

                        // 取得目标位置
                        matchTarget = hit.point;
                        matchTarget.y = hit.collider.transform.position.y + hit.collider.bounds.size.y + 0.07f;
                    }

                }
            }
        }

        anim.SetBool(sliderId, isSlider);

        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") && anim.IsInTransition(0) == false)
        //{
        //    anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.38f, 0.50f);
        //}
    }
}

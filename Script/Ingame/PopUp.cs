using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PopUp : MonoBehaviour
{
    [SerializeField] bool healPopup;
    [SerializeField] bool dmgPopup;
    [SerializeField] MMF_Player feedbackDMG;

    private void Start()
    {
        feedbackDMG = GameObject.Find("Damage Boost Pop Up").GetComponent<MMF_Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (healPopup)
            {
                PlayerHealth.instance.HealUp(10);
                Tower.instance.AddHealth(100);
            }

            if(dmgPopup)
            {
                PlayerShoot.instance.IncreaseWeaponDmg(8);

                feedbackDMG.StopFeedbacks();
                feedbackDMG.ResetFeedbacks();
                feedbackDMG.PlayFeedbacks();
            }

            Destroy(this.gameObject);
        }
    }
}

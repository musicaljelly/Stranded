using UnityEngine;
using System.Collections;

public class Camp : MonoBehaviour {

    float shelterTime = 30f; // time in seconds
    float shelterCounter = 0f;
    float campfireCounter = 0f;
    float campfireTime = 20f; // time in seconds
    float campfireStartCounter = 0f;
    float campfireStartTime = 20f; // time in seconds
    int campfireLv = 3;
    int shelterLv = 0;
    int foodStock = 0;
    int woodStock = 2;
    int palmStock = 4;

    bool shelterUpgrading = false;
    bool campfireStarting = false;

	GameObject fire1 = null;
	GameObject fire2 = null;
	GameObject fire3 = null;
    
    // Use this for initialization
	void Start () {
		fire1 = GameObject.Find("Fire1");
		fire2 = GameObject.Find("Fire2");
		fire3 = GameObject.Find("Fire3");

		SetFireLevel(campfireLv);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCampfire(); // Degrade over time
	}

    void UpdateCampfire()
    {
        // If campfire is lit, decrease level over time
        if (campfireLv > 0)
        {
            campfireCounter += 1 * Time.deltaTime;
            if (campfireCounter >= campfireTime)
            {
                campfireCounter = 0f;
                campfireLv--;
				SetFireLevel(campfireLv);
            }
        }
    }
    public void StartCampfire()
    {
        if (IsValidSTARTCAMPFIRE())
        {
            woodStock--;
            campfireStarting = true;
        }

        if (campfireStarting)
        {
            if (campfireStartCounter < campfireStartTime)
            {
                campfireStartCounter += Time.deltaTime;
            }
            else
            {
                campfireStartCounter = 0;
                campfireStarting = false;
                campfireLv++;
				SetFireLevel(campfireLv);
            }
        }
    }
    public bool IsValidSTARTCAMPFIRE()
    {
        if (woodStock > 0 && campfireLv == 0 && campfireStarting == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void StokeCampfire()
    {
        if (IsValidSTOKECAMPFIRE())
        {
            woodStock--;
            campfireLv++;
			SetFireLevel(campfireLv);
        }
    }
    public bool IsValidSTOKECAMPFIRE()
    {
        if (woodStock > 0 && campfireLv > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpgradeShelter()
    {
        if (IsValidUPGRADESHELTER())
        {
            woodStock -= 2;
            palmStock -= 4;
            shelterUpgrading = true;
        }

        if (shelterUpgrading == true)
        {
            if (shelterCounter < shelterTime)
            {
                shelterCounter += 1 * Time.deltaTime;
            }
            else
            {
                shelterCounter = 0;
                shelterUpgrading = false;
                shelterLv++;
            }
        }
    }
    public bool IsValidUPGRADESHELTER()
    {
        if (shelterLv == 0 && woodStock >= 2 && palmStock >= 4 && shelterUpgrading == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	void SetFireLevel(int level) {
		fire1.renderer.enabled = false;
		fire2.renderer.enabled = false;
		fire3.renderer.enabled = false;

		if (level == 1) {
			fire1.renderer.enabled = true;
		} else if (level == 2) {
			fire2.renderer.enabled = true;
		} else if (level == 3) {
			fire3.renderer.enabled = true;
		}
	}
}

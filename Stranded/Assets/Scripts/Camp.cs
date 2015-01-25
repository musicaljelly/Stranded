using UnityEngine;
using System.Collections;

public class Camp : MonoBehaviour {

    static float shelterTime = 30f; // time in seconds
    static float shelterCounter = 0f;
    static float campfireCounter = 0f;
    static float campfireTime = 3f; // Default = 15. time in seconds
    static float campfireStartCounter = 0f;
    static float campfireStartTime = 20f; // time in seconds
    public static int campfireLv = 3;
    public static int shelterLv = 0;

    public static int foodStock = 0;
    public static int woodStock = 2;
    public static int palmStock = 4;

    public static bool shelterUpgrading = false;
    public static bool campfireStarting = false;

	static GameObject fire1 = null;
    static GameObject fire2 = null;
    static GameObject fire3 = null;

    Sound sound;
    
    // Use this for initialization
	void Start () {
		fire1 = GameObject.Find("Fire1");
		fire2 = GameObject.Find("Fire2");
		fire3 = GameObject.Find("Fire3");

		SetFireLevel(campfireLv);

        sound = GameObject.FindGameObjectWithTag("Global").GetComponent<Sound>();
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
            sound.PlaySound(1);
            campfireCounter += 1 * Time.deltaTime;
            if (campfireCounter >= campfireTime)
            {
                sound.StopSound(1);
                campfireCounter = 0f;
                campfireLv--;
				SetFireLevel(campfireLv);
            }
        }
    }
    public static void StartCampfire()
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
    public static bool IsValidSTARTCAMPFIRE()
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
    public static void StokeCampfire()
    {
        if (IsValidSTOKECAMPFIRE())
        {
            woodStock--;
            campfireLv++;
			SetFireLevel(campfireLv);
        }
    }
    public static bool IsValidSTOKECAMPFIRE()
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

    public static void UpgradeShelter()
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
    public static bool IsValidUPGRADESHELTER()
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

    static void SetFireLevel(int level)
    {
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

	public static void AddToFoodStock(int numFood)
	{
		Camp.foodStock = Camp.foodStock + numFood;
	}

	public static void AddToWoodStock(int numWood)
	{
		Camp.woodStock = Camp.woodStock+ numWood;
	}
}

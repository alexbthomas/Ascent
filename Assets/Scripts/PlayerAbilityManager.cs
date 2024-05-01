using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public bool silentTakedownUnlocked = false; // Track if silent takedown is unlocked
    public bool cloakUnlocked = false; // Track if cloak is unlocked
    public bool empUnlocked = false; // Track if EMP is unlocked

    public void UnlockSilentTakedown()
    {
        silentTakedownUnlocked = true; // Unlock silent takedown
        Debug.Log("Silent Takedown Unlocked");
    }

    public void UnlockCloak()
    {
        cloakUnlocked = true; // Unlock cloak
        Debug.Log("Cloak Unlocked");
    }

    public void UnlockEMP()
    {
        empUnlocked = true; // Unlock EMP
        Debug.Log("EMP Unlocked");
    }
}

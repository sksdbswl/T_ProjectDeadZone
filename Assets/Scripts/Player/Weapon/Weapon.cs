using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform rightHandTarget;
    public Transform leftHandTarget;
    
    public void EquipWeapon(Weapon newWeapon, IKController ikController)
    {
        // IKController에 타겟 전달
        ikController.SetIKTargets(newWeapon.rightHandTarget, newWeapon.leftHandTarget);
    }
}


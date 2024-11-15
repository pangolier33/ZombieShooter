using UnityEngine;

namespace _Scripts.Components
{
    public class DamageComponent : MonoBehaviour
    {
        public static int _hpDelta = 1;

        public static void Apply(GameObject target)
        {
            HealthComponent healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.HealthValue -= _hpDelta;
            }
        }
    }
}

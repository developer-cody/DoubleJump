using BepInEx;
using GorillaLocomotion;
using UnityEngine;

namespace DoubleJump
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERSION)]
    [BepInDependency("Husky.BananaOS")]
    public class DoubleJumpPlugin : BaseUnityPlugin
    {
        public static bool _isEnabled = true;
        public static float _jumpForce = 6f;
        public static float _jumpCooldown = 1f;
        private float _lastJumpTime = 0f;

        private void ValidateSettings()
        {
            _jumpForce = Mathf.Clamp(_jumpForce, 1f, 14f);
            _jumpCooldown = Mathf.Clamp(_jumpCooldown, 1f, 7f);
        }

        private void Update()
        {
            if (!_isEnabled) return;

            ValidateSettings();

            if (NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton && Time.time >= _lastJumpTime + _jumpCooldown)
                {
                    GTPlayer.Instance.ApplyKnockback(Vector3.up, _jumpForce, false);
                    _lastJumpTime = Time.time;
                }
            }
        }

        private void OnEnable() => _isEnabled = true;
        private void OnDisable() => _isEnabled = false;
    }
}

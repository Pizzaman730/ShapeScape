using System;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    /// <summary>
    /// Manages which morph is currently active.
    /// </summary>
    public class MorphManager
    {
        /// <summary>
        /// The currently active morph.
        /// </summary>
        private IMorph currentMorph;

        /// <summary>
        /// The name of the current morph (empty string if none).
        /// </summary>
        public string morphName = "None";

        /// <summary>
        /// The number of frames that must pass before a morph can be switched again.
        /// </summary>
        private int morphSwitchCooldown = 10;

        /// <summary>
        /// The number of frames remaining until a morph can be switched.
        /// </summary>
        private int morphSwitchTimer = 0;

        public MorphManager()
        {
            Logger.Log("MorphManager instantiated.");
        }

        /// <summary>
        /// Switches to the given morph.
        /// </summary>
        /// <param name="newMorph">The morph to switch to.</param>
        /// <param name="player">The player object to apply the morph to.</param>
        public void SwitchMorph(IMorph newMorph, Player player)
        {
            if (newMorph == null)
            {
                Logger.LogError("Attempted to switch to a null morph.");
                throw new ArgumentNullException(nameof(newMorph), "New morph cannot be null.");
            }

            if (currentMorph?.GetType() == newMorph.GetType())
            {
                Logger.Log("Same morph selected, skipping switch.");
                return;
            }

            currentMorph?.RevertMorph(player);
            currentMorph = newMorph;
            currentMorph.ApplyMorph(player);
            morphName = newMorph.GetType().Name;
            Logger.Log($"Morph switched to: {morphName}");
        }

        /// <summary>
        /// Updates the morph manager.
        /// </summary>
        /// <param name="player">The player object to check for morph switching.</param>
        public void Update(Player player)
        {
            if (player == null)
            {
                Logger.LogError("Player object is null.");
                throw new ArgumentNullException(nameof(player), "Player cannot be null.");
            }

            if (morphSwitchTimer > 0)
            {
                morphSwitchTimer--;
                return;
            }

            TrySwitchMorph(player.inputs.PlayerMorphButton, new PlayerMorph(), player, "None");
            TrySwitchMorph(player.inputs.CircleMorphButton, new CircleMorph(), player, "Circle");
            TrySwitchMorph(player.inputs.OvalMorphButton, new BouncyOvalMorph(), player, "BouncyOval");
            TrySwitchMorph(player.inputs.TrapezoidMorphButton, new TrapezoidMorph(), player, "Trapezoid");
            TrySwitchMorph(player.inputs.TriangleMorphButton, new TriangleMorph(), player, "Triangle");
        }

        /// <summary>
        /// Tries to switch to the given morph if the given key is down.
        /// </summary>
        /// <param name="morphKey">The key to check for.</param>
        /// <param name="newMorph">The morph to switch to if the key is down.</param>
        /// <param name="player">The player object to switch the morph on.</param>
        /// <param name="morphName">The name of the morph to switch to.</param>
        private void TrySwitchMorph(Keys morphKey, IMorph newMorph, Player player, string morphName)
        {
            if (InputManager.GetKeyDown(morphKey))
            {
                this.morphName = morphName;
                SwitchMorph(newMorph, player);
                morphSwitchTimer = morphSwitchCooldown;
            }
        }
    }
}
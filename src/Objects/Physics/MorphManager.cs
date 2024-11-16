using System;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    public class MorphManager
    {
        private IMorph currentMorph;
        public string morphName = "None";

        private int morphSwitchCooldown = 10;
        private int morphSwitchTimer = 0;

        // Constructor
        public MorphManager()
        {
            Logger.Log("MorphManager instantiated.");
        }

        // Switch the morph of the player
        public void SwitchMorph(IMorph newMorph, Player player)
        {
            try
            {
                Logger.Log("SwitchMorph method called.");

                if (currentMorph != null && currentMorph.GetType() == newMorph.GetType())
                {
                    Logger.Log("Same morph selected, skipping switch.");
                    return;
                }

                if (newMorph == null)
                {
                    Logger.LogError("Attempted to switch to a null morph.");
                    throw new ArgumentNullException(nameof(newMorph), "New morph cannot be null.");
                }

                if (currentMorph != null)
                {
                    Logger.Log($"Reverting current morph: {currentMorph.GetType().Name}");
                    currentMorph.RevertMorph(player);
                }

                Logger.Log($"Applying new morph: {newMorph.GetType().Name}");
                currentMorph = newMorph;
                currentMorph.ApplyMorph(player);

                Logger.Log($"Morph switched to: {newMorph.GetType().Name}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in SwitchMorph: {ex.Message}");
            }
        }

        // Update the state of morphing
        public void Update(Player player)
        {
            try
            {
                if (player == null)
                {
                    Logger.LogError("Player object is null.");
                    throw new ArgumentNullException(nameof(player), "Player cannot be null.");
                }

                // Only allow morph switching if the cooldown has elapsed
                if (morphSwitchTimer > 0)
                {
                    morphSwitchTimer--; // Decrease cooldown timer
                    return; // Do nothing if cooldown is not complete
                }

                // Check for key presses
                if (InputManager.GetKeyDown(Keys.D1))
                {
                    morphName = "None";
                    SwitchMorph(new PlayerMorph(), player);
                    morphSwitchTimer = morphSwitchCooldown; // Reset the cooldown
                }
                if (InputManager.GetKeyDown(Keys.D2)) 
                {
                    morphName = "Circle";
                    SwitchMorph(new CircleMorph(), player);
                    morphSwitchTimer = morphSwitchCooldown; // Reset the cooldown
                }
                if (InputManager.GetKeyDown(Keys.D3)) 
                {
                    morphName = "BouncyOval";
                    SwitchMorph(new BouncyOvalMorph(), player);
                    morphSwitchTimer = morphSwitchCooldown; // Reset the cooldown
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in Update: {ex.Message}");
            }
        }
    }
}

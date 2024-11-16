using System;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    public class MorphManager
    {
        private IMorph currentMorph;
        public string morphName = "None";

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

                if (InputManager.GetKeyDown(Keys.E)) 
                {
                    //Logger.Log("Key 'E' pressed. Switching to FastMorph.");
                    morphName = "Circle";
                    SwitchMorph(new CircleMorph(), player);
                }
                else
                {
                    //Logger.Debug("No morph switch.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in Update: {ex.Message}");
            }
        }
    }
}

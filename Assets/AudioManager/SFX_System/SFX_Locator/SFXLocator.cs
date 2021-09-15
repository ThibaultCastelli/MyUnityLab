using UnityEngine;

namespace SFXTC
{
    // Used to get a global access to the current audio player 
    public class SFXLocator
    {
        static SFXPlayerBase _currentSFXPlayer;

        public static void SetSFXPlayer(SFXPlayerBase newSFXPlayer) => _currentSFXPlayer = newSFXPlayer;

        // Return the current audio player
        public static SFXPlayerBase GetSFXPlayer()
        {
            if (_currentSFXPlayer == null)
                Debug.LogError("ERROR : The service locator is null, you need to add an AudioManager.");

            return _currentSFXPlayer;
        }
    }
}

using UnityEngine;

namespace AudioTC
{
    // Used to get a global access to the current audio player 
    public class AudioLocator
    {
        static IAudioPlayer _serviceProvider;

        public static void SetAudioPlayer(IAudioPlayer serviceProvider) => _serviceProvider = serviceProvider;

        // Return the current audio player
        public static IAudioPlayer GetAudioPlayer()
        {
            if (_serviceProvider == null)
                Debug.LogError("ERROR : The service locator is null, you need to add an AudioManager.");

            return _serviceProvider;
        }
    }
}

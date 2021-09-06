using UnityEngine;

namespace AudioThibaultCastelli
{
    // Used to get a global access to the current audio player 
    public class AudioLocator
    {
        static IAudioPlayer _serviceProvider;

        public static void SetServiceProvider(IAudioPlayer serviceProvider) => _serviceProvider = serviceProvider;

        // Return the current audio player
        public static IAudioPlayer GetServiceProvider()
        {
            if (_serviceProvider == null)
                Debug.LogError("ERROR : The service locator is null, you need to add an AudioManager.");

            return _serviceProvider;
        }
    }
}

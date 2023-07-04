# Resonance Audio

This is a guide to develop Unity projects using Resonance Audio. 
For more information, please refer to [Resonance Audio Unity's documentation](https://resonance-audio.github.io/resonance-audio/develop/unity/getting-started.html).


## Create New Unity Project & Import SDK

1. Open Unity Hub and create a new 3D project. 

2. Select `Assets -> Import Package -> Custom Package`.

3. Select the [ResonanceAudioForUnity_*.unitypackage](https://github.com/resonance-audio/resonance-audio-unity-sdk/releases) file that you downloaded.

4. In the `Importing Package` dialog, click `Import`. Accept any API upgrades if prompted.


## Configure Unity Project to use Resonance Audio

1. Use `Edit -> Project Settings -> Audio` to open the AudioManager settings.

2. Select `Resonance Audio` as the `Spatializer Plugin`.

3. Select `Resonance Audio` as the `Ambisonic Decoder Plugin`.
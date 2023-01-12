# Unity Audio Source

Audio Clip is required to create an Audio Source. The Clip is the actual sound file that will be played back. The Source is like a controller for starting and stopping playback of that clip, and modifying other audio properties.

To create a new Audio Source:

1. Import your audio files into your Unity Project. These are now Audio Clips.

2. Go to `GameObject -> Create Empty` from the menubar.

3. With the new GameObject selected, select `Component -> Audio -> Audio Source`.

4. In the Inspector, find the Audio Clip property on the Audio Source Component and assign a clip, either by dragging one from the Project Window or by clicking the small circle icon to the right of the Inspector property, then selecting a clip from the list.

> [!Note]
> If you want to create an **Audio Source** just for one **Audio Clip** that you have in the Assets folder then you can just drag that clip to the scene view - a GameObject with an Audio Source component will be created automatically for it. 
> 
> Dragging a clip onto on existing GameObject will attach the clip along with a new Audio Source if there isn’t one already there. If the object does already have an Audio Source then the newly dragged clip will replace the one that the source currently uses.
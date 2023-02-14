# YVR Manager

The script [YVRManager](xref:YVR.Core.YVRManager) is the core manager of the whole SDK, which controls the execution of VR event functions and offers different settings for rendering quality, tracking mode, performance, etc.

## Configure Settings

The settings in terms of rendering quality, tracking space, performance, etc. can be set via the Inspector panel of [YVRManager](xref:YVR.Core.YVRManager).

![YVRManager Inspector](./YVRManager/2022-04-18-19-31-07.png)

### Quality

-   `VSync Count`: The number of VSyncs that should pass between each frame.
-   `Fixed Foveated Rendering Level`: the level for fixed foveated rendering. The higher the value, the more blurred the edges of the picture, but the better the performance.
-   `UseRecommendMSAALevel`: Whether to use recommend MSAA level.

### Tracking

-   `Tracking Space`: The tracking space of the rigid poses.
    - `Eye Level`: HMD pose will not consider ground height.
    - `Floor Level`: Mode used in most cases, which will consider ground height and recenter state.
    - `Stage`: HMD pose will not consider recenter state.

## Events

The events happened at runtime are all managed in [YVRManager.instance.eventsManager](xref:YVR.Core.YVREventsManager), including:

1.  `onTrackingAcquired`: Occurs when head gained tracking.
2.  `onTrackingLost`: Occurs when head lost tracking.
3.  `onHMDMounted`: Occurs when an HMD is put on the user's head.
4.  `onHMDUnMounted`: Occurs when an HMD is taken off the user's head.
5.  `onRecenterOccurred`: Occurs when recenter occurred.
6.  `onFocusGained`: Occurs when application focus gained.
7.  `onFocusLost`: Occurs when application focus lost.
8.  `onVisibilityGained`: Occurs when application is visible
9.  `onVisibilityLost`: Occurs when application is completely obscured by other content.
10. `onUpdate`: Occurs at the update function of every frame.
11. `onPreSubmitGfx`: Occurs just before current frame is submitted to native, which will be triggered in rendering thread.
12. `onPostSubmitGfx`: Occurs just after current frame is submitted to native, which will be triggered in rendering thread.

## Device state

Device state can be quired in [YVRManager.instance.hmdManager](xref:YVR.Core.YVRHMDManager), e.g. battery level, battery temperature, volume level.

## Boundary state

All boundary state can be quired in [YVRManager.instance.boundary](xref:YVR.Core.YVRBoundary), e.g. boundary dimensions, boundary visibility. Also, [YVRManager.instance.boundary](xref:YVR.Core.YVRBoundary) provide several APIs for querying intersections state with boundary.


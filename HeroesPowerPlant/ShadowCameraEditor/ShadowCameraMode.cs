namespace HeroesPowerPlant.ShadowCameraEditor {

    /*
        0 = PJSCamera::CameraModeGeneral
        1 = PJSCamera::CameraModeFree
        2 = PJSCamera::CameraModeTestPlayer
        3 = PJSCamera::CameraModeTestPlayerNoRot
        4 = BROKEN/REMOVED CODE/UNUSED
        5 = PJSCamera::CameraModeSetEditor
        6 = PJSCamera::CameraModeSetCamEdit
        7 = PJSCamera::CameraModeFixEye
        8 = PJSCamera::CameraModeFixLook
        9 = BROKEN/REMOVED CODE/UNUSED
        10 = PJSCamera::CameraModeGeneral (its zoomed in more?)
        11 = PJSCamera::CameraModeLookFront
        12 = PJSCamera::CameraModeFixEye2
        13 = PJSCamera::CameraModeFixLook2
        14 = PJSCamera::CameraModeFixLookChar (partner?)       
     */
    public enum ShadowCameraMode {
        General=0,
        Free=1,
        TestPlayer=2,
        TestPlayerNoRot=3,
        SetEditor=5,
        SetCamEdit=6,
        FixEye=7,
        FixLook=8,
        General_withCamDistCamHeight=10,
        LookFront=11,
        FixEye2=12,
        FixLook2=13,
        FixLookChar=14
    }
}

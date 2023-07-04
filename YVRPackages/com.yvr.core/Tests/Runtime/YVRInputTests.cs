using NUnit.Framework;

namespace YVR.Core.Test
{
    public class YVRInputTests
    {
        [Test]
        [TestCase(ControllerType.LeftTouch)]
        [TestCase(ControllerType.RightTouch)]
        public void GetPrimaryController_JustSet_EqualsToSetValue(ControllerType controller)
        {
            YVRInput.SetPrimaryController(controller);
            ControllerType ret = YVRInput.GetPrimaryController();
            Assert.That(controller == ret);
        }
    }
}
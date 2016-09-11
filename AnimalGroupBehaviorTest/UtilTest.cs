using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalGroupBehavior;

namespace AnimalGroupBehaviorTest
{
    [TestClass]
    public class UtilTest
    {

        #region TryVerifyGUID  测试

        [TestMethod]
        public void TryVerifyGUIDTestValid()
        {
            string guid = "e4e87cb2-8e9a-4749-abb6-26c59344dfee";
            Assert.AreEqual(Util.TryVerifyGUID(guid), true);
        }

        [TestMethod]
        public void TryVerifyGUIDTestWithBlank()
        {
            string guid = "e4e8 7cb2-8e9a-47  49-abb6-26c593 44dfee";
            Assert.AreEqual(Util.TryVerifyGUID(guid), false);
        }

        [TestMethod]
        public void TryVerifyGUIDTestWithOtherCharacter()
        {
            string guid = "e4e8*7cb2-8e/9a-4749-abb6-26c593,44dfee";
            Assert.AreEqual(Util.TryVerifyGUID(guid), false);
        }

        [TestMethod]
        public void TryVerifyGUIDTestWithRandomCharacter()
        {
            string guid = "sdf98^*09234adasbhi&%$#(_kasdfa2222";
            Assert.AreEqual(Util.TryVerifyGUID(guid), false);
        }

        #endregion


        #region TryVerifyDateTime  测试

        [TestMethod]
        public void TryVerifyDateTimeTestValid()
        {
            string dtstr = "2016/09/02 22:30:46";
            DateTime dt = new DateTime();

            Assert.AreEqual(Util.TryVerifyDateTime(dtstr, out dt), true);
            Assert.AreEqual(dt.Equals(DateTime.MinValue), false);
        }

        [TestMethod]
        public void TryVerifyDateTimeTestWithOtherCharacter()
        {
            string dtstr = "20ab/09/02 22:cd:46";
            DateTime dt = new DateTime();

            Assert.AreEqual(Util.TryVerifyDateTime(dtstr, out dt), false);
            Assert.AreEqual(dt, DateTime.MinValue);
        }

        [TestMethod]
        public void TryVerifyDateTimeTestWithShoterLength()
        {
            string dtstr = "2016/09/02 22:10";
            DateTime dt = new DateTime();

            Assert.AreEqual(Util.TryVerifyDateTime(dtstr, out dt), false);
            Assert.AreEqual(dt, DateTime.MinValue);
        }

        [TestMethod]
        public void TryVerifyDateTimeTestWithInvalidDate()
        {
            string dtstr = "2016/20/02 22:80:23";
            DateTime dt = new DateTime();

            Assert.AreEqual(Util.TryVerifyDateTime(dtstr, out dt), false);
            Assert.AreEqual(dt, DateTime.MinValue);
        }

        [TestMethod]
        public void TryVerifyDateTimeTestWithRandomCharacter()
        {
            string dtstr = "dfsf///sdfsfwe/g3342 6sf:->sfweii56";
            DateTime dt = new DateTime();

            Assert.AreEqual(Util.TryVerifyDateTime(dtstr, out dt), false);
            Assert.AreEqual(dt, DateTime.MinValue);
        }

        #endregion


        #region TryVerifyCoordinate  测试

        [TestMethod]
        public void TryVerifyCoordinateTestValid()
        {
            string str1 = "cat1 10 9 2 -1";
            string str2 = "cat2 2 3";
            AnimalCoordinate cdnDest = null;
            AnimalCoordinate coordinate = null;

            cdnDest = new AnimalCoordinate("cat1", 10, 9, 2, -1);
            coordinate = new AnimalCoordinate();
            Assert.AreEqual(Util.TryVerifyCoordinate(str1, ref coordinate), true);
            Assert.IsTrue(coordinate.ValueEquals(cdnDest));

            cdnDest = new AnimalCoordinate("cat2", 2, 3);
            coordinate = new AnimalCoordinate();
            Assert.AreEqual(Util.TryVerifyCoordinate(str2, ref coordinate), true);
            Assert.IsTrue(coordinate.ValueEquals(cdnDest));
        }

        [TestMethod]
        public void TryVerifyCoordinateTestWithOtherCharacter()
        {
            string str = "cat1 10 9a 2 -1";
            AnimalCoordinate coordinate = new AnimalCoordinate();

            Assert.AreEqual(Util.TryVerifyCoordinate(str, ref coordinate), false);

        }

        [TestMethod]
        public void TryVerifyCoordinateTestWithShorterLength()
        {
            string str1 = "cat1 10 9 2";
            string str2 = "10";
            AnimalCoordinate coordinate = new AnimalCoordinate();

            Assert.AreEqual(Util.TryVerifyCoordinate(str1, ref coordinate), false);
            Assert.AreEqual(Util.TryVerifyCoordinate(str2, ref coordinate), false);

        }

        [TestMethod]
        public void TryVerifyCoordinateTestWithRandomCharacter()
        {
            string str = "sdas jhsdr dasfsf ds s^ad";
            AnimalCoordinate coordinate = new AnimalCoordinate();

            Assert.AreEqual(Util.TryVerifyCoordinate(str, ref coordinate), false);
        }

        #endregion


    }
}

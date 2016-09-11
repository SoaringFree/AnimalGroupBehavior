using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalGroupBehavior;

namespace AnimalGroupBehaviorTest
{
    [TestClass]
    public class AnimalGroupTest
    {
        #region GetSnapShot  测试

        [TestMethod]
        public void GetSnapShotTestValid()
        {
            AnimalGroupInfo groupInfo = new AnimalGroupInfo();
            string data = "e4e87cb2-8e9a-4749-abb6-26c59344dfee\n" +
                "2016/09/02 22:30:46\n" +
                "cat1 10 9\n\n" +
                "351055db-33e6-4f9b-bfe1-16f1ac446ac1\n" +
                "2016/09/02 22:30:52\n" +
                "cat1 10 9 2 -1\n" +
                "cat2 2 3\n\n" +
                "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155\n" +
                "2016/09/02 22:31:02\n" +
                "cat1 12 8 3 4\n";
            string id = "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155";

            string expSnapShot = "cat1 15 12\n" +
                "cat2 2 3\n";

            Assert.AreEqual(groupInfo.GetSnapShot(data, id), expSnapShot);
        }

        [TestMethod]
        public void GetSnapShotTestInvalidGUID()
        {
            AnimalGroupInfo groupInfo = new AnimalGroupInfo();
            string data = "e4e87cb2-8e%E*9a-47 49-abb6-26c59344dfee\n" +    // 错误
                "2016/09/02 22:30:46\n" +
                "cat1 10 9\n\n" +
                "351055db-33e6-4f9b-bfe1-16f1ac446ac1\n" +
                "2016/09/02 22:30:52\n" +
                "cat1 10 9 2 -1\n" +
                "cat2 2 3\n\n" +
                "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155\n" +
                "2016/09/02 22:31:02\n" +
                "cat1 12 8 3 4\n";
            string id = "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155";

            string expSnapShot = "Invalid format.";

            Assert.AreEqual(groupInfo.GetSnapShot(data, id), expSnapShot);
        }

        [TestMethod]
        public void GetSnapShotTestInvalidDateTime()
        {
            AnimalGroupInfo groupInfo = new AnimalGroupInfo();
            string data = "e4e87cb2-8e9a-4749-abb6-26c59344dfee\n" +
                "2016/09/02 22:90:46\n" +   // 错误
                "cat1 10 9\n\n" +
                "351055db-33e6-4f9b-bfe1-16f1ac446ac1\n" +
                "2016/09/02 22\n" +
                "cat1 10 9 2 -1\n" +
                "cat2 2 3\n\n" +
                "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155\n" +
                "2016/09/02 22:31:02\n" +
                "cat1 12 8 3 4\n";
            string id = "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155";

            string expSnapShot = "Invalid format.";

            Assert.AreEqual(groupInfo.GetSnapShot(data, id), expSnapShot);
        }


        [TestMethod]
        public void GetSnapShotTestInvalidCoordnate()
        {
            AnimalGroupInfo groupInfo = new AnimalGroupInfo();
            string data = "e4e87cb2-8e9a-4749-abb6-26c59344dfee\n" +
                "2016/09/02 22:30:46\n" +
                "cat1 10 9\n\n" +
                "351055db-33e6-4f9b-bfe1-16f1ac446ac1\n" +
                "2016/09/02 22:30:52\n" +
                "cat1 10 9 2 -1\n" +
                "cat2 2 3 5\n\n" +      // 错误
                "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155\n" +
                "2016/09/02 22:31:02\n" +
                "cat1 12 8 3 4\n";
            string id = "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155";

            string expSnapShot = "Invalid format.";

            Assert.AreEqual(groupInfo.GetSnapShot(data, id), expSnapShot);
        }

        [TestMethod]
        public void GetSnapShotTestInvalidConflict()
        {
            AnimalGroupInfo groupInfo = new AnimalGroupInfo();
            string data = "e4e87cb2-8e9a-4749-abb6-26c59344dfee\n" +
                "2016/09/02 22:30:46\n" +
                "cat1 10 9\n\n" +
                "351055db-33e6-4f9b-bfe1-16f1ac446ac1\n" +
                "2016/09/02 22:30:52\n" +
                "cat1 10 9 2 -1\n" +
                "cat2 2 3\n\n" +
                "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155\n" +
                "2016/09/02 22:31:02\n" +
                "cat1 11 8 3 4\n";      // 冲突
            string id = "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155";

            string expSnapShot = "Conflict found at " + id;

            Assert.AreEqual(groupInfo.GetSnapShot(data, id), expSnapShot);
        }

        [TestMethod]
        public void GetSnapShotTestWithMultiLineFeed()
        {
            AnimalGroupInfo groupInfo = new AnimalGroupInfo();
            string data = "e4e87cb2-8e9a-4749-abb6-26c59344dfee\n" +
                "2016/09/02 22:30:46\n" +
                "cat1 10 9\n\n" +
                "351055db-33e6-4f9b-bfe1-16f1ac446ac1\n" +
                "2016/09/02 22:30:52\n" +
                "cat1 10 9 2 -1\n" +
                "cat2 2 3\n\n" +
                "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155\n" +
                "2016/09/02 22:31:02\n" +
                "cat1 12 8 3 4\n";      // 冲突
            string id = "dcfa0c7a-5855-4ed2-bc8c-4accae8bd155";

            string expSnapShot = "Conflict found at " + id;

            Assert.AreEqual(groupInfo.GetSnapShot(data, id), expSnapShot);
        }

        #endregion




    }
}

using System;
using System.Linq;
using System.Xml.Schema;
using DijkstraAlgorithm;
using NUnit.Framework;

namespace graph
{
    [TestFixture]
    public class UnitTests1
    {
        [Test]
        public void HeadTest()//revers
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.test11);
            var res = ee.GetMinPath(1, 23, null).ToString();

            var res1 = ee.GetMinPath(23, 1, null);
            res1.PathNodeIdList.Reverse();
            var t = res == res1.ToString();
            Assert.True(t);


        }

        [Test]
        public void HeadTest2()//revers and crash
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.test11);
            var res = ee.GetMinPath(1, 23, 8).ToString();
           
            Node res1 = ee.GetMinPath(23, 1, 8);
            res1.PathNodeIdList.Reverse();
            var t = res == res1.ToString();
            Assert.True(t);
        }

        [Test]
        public void Doublelink()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.doublelink);
            Node res = ee.GetMinPath(1, 23, 8);
            Assert.True(res == null && ee.LastErrorList.Any());
        }
        [Test]
        public void DoubleNode()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.doublenode);
            Node res = ee.GetMinPath(1, 23, 8);
            Assert.True(res == null && ee.LastErrorList.Any());
        }

        [Test]
        public void Linktoherself()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.linktoherself);
            Node res = ee.GetMinPath(1, 23, 8);
            Assert.True(res == null && ee.LastErrorList.Any());
        }

        [Test]
        public void ZeroLink()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.zerolink);
            Node res = ee.GetMinPath(1, 23, 8);
            Assert.True(res == null && ee.LastErrorList.Any());
        }

        [Test]
        public void TwinGraph1()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            Node res = ee.GetMinPath(1, 30, 8);
            Assert.True(res == null && !ee.LastErrorList.Any());
        }
        [Test]
        public void TwinGraph2()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            Node res = ee.GetMinPath(30, 1, 8);
            Assert.True(res == null && !ee.LastErrorList.Any());
        }
        [Test]
        public void StartAsFinish()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            Node res = ee.GetMinPath(1, 1, 8);
            Assert.True(res == null && ee.LastErrorList.Any());
        }
        [Test]
        public void StartAsCrash()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            Node res = ee.GetMinPath(1, 12, 1,4,5,6);
            Assert.True(res == null && ee.LastErrorList.Any());
        }

        [Test]
        public void FinishAsCrash()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            Node res = ee.GetMinPath(1, 12,3,4,5,6, 12);
            Assert.True(res == null && ee.LastErrorList.Any());
        }

        [Test]
        public void ZeroCrash()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            Node res = ee.GetMinPath(1, 12, 3, 4, 5, 6, 1233,4,456);
            Assert.True(res == null && ee.LastErrorList.Any());
           
            var resw = ee.GetMinPath(1, 23, null).ToString();
            var res1 = ee.GetMinPath(23, 1, null);
            res1.PathNodeIdList.Reverse();
            Assert.True(resw==res1.ToString());
        }

          [Test]
        public void EmptyCollectionNodes()
        {
            var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
            ee.Clear();
            Node res = ee.GetMinPath(1, 12, 8, 4, 5, 6);
            Assert.True(res == null && ee.LastErrorList.Any());
        }
          [Test]
          public void FailSrartIdNode()
          {
              var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
              Node res = ee.GetMinPath(231, 12, 8, 4, 5, 6);
              Assert.True(res == null && ee.LastErrorList.Any());
          }
          [Test]
          public void FailFinishIdNode()
          {
              var ee = (Graph)Program.GetNodes(Properties.Resources.twingraph);
              Node res = ee.GetMinPath(231, 12, 8, 4, 5, 6);
             
          
          }


    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Examine.Test
{
    [TestFixture]
    public class OrderedDictionaryTests
    {
        private OrderedDictionary<string, string> GetAlphabetDictionary(IEqualityComparer<string> comparer = null)
        {
            OrderedDictionary<string, string> alphabet = (comparer == null ? new OrderedDictionary<string, string>() : new OrderedDictionary<string, string>(comparer));
            for (int a = Convert.ToInt32('a'); a <= Convert.ToInt32('z'); a++)
            {
                char c = Convert.ToChar(a);
                alphabet.Add(c.ToString(), c.ToString().ToUpper());
            }
            Assert.AreEqual(26, alphabet.Count);
            return alphabet;
        }

        private List<KeyValuePair<string, string>> GetAlphabetList()
        {
            var alphabet = new List<KeyValuePair<string, string>>();
            for (int a = Convert.ToInt32('a'); a <= Convert.ToInt32('z'); a++)
            {
                char c = Convert.ToChar(a);
                alphabet.Add(new KeyValuePair<string, string>(c.ToString(), c.ToString().ToUpper()));
            }
            Assert.AreEqual(26, alphabet.Count);
            return alphabet;
        }

        [Test]
        public void TestAdd()
        {
            var od = new OrderedDictionary<string, string>();
            Assert.AreEqual(0, od.Count);
            Assert.AreEqual(-1, od.IndexOf("foo"));

            od.Add("foo", "bar");
            Assert.AreEqual(1, od.Count);
            Assert.AreEqual(0, od.IndexOf("foo"));
            //Assert.AreEqual(od[0].Value, "bar");
            Assert.AreEqual(od["foo"].Value, "bar");
            Assert.AreEqual(od[0].Key, "foo");
            Assert.AreEqual(od[0].Value, "bar");
        }

        [Test]
        public void TestRemove()
        {
            var od = new OrderedDictionary<string, string>();

            od.Add("foo", "bar");
            Assert.AreEqual(1, od.Count);

            od.Remove("foo");
            Assert.AreEqual(0, od.Count);
        }

        [Test]
        public void TestRemoveAt()
        {
            var od = new OrderedDictionary<string, string>();

            od.Add("foo", "bar");
            Assert.AreEqual(1, od.Count);

            od.RemoveAt(0);
            Assert.AreEqual(0, od.Count);
        }

        [Test]
        public void TestClear()
        {
            OrderedDictionary<string, string> od = GetAlphabetDictionary();
            Assert.AreEqual(26, od.Count);
            od.Clear();
            Assert.AreEqual(0, od.Count);
        }

        [Test]
        public void TestOrderIsPreserved()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();
            List<KeyValuePair<string, string>> alphabetList = GetAlphabetList();
            Assert.AreEqual(26, alphabetDict.Count);
            Assert.AreEqual(26, alphabetList.Count);

            var keys = alphabetDict.Keys.ToList();
            var values = alphabetDict.Values.ToList();

            for (int i = 0; i < 26; i++)
            {
                string dictItem = alphabetDict.GetItem(i);
                KeyValuePair<string, string> listItem = alphabetList[i];
                string key = keys[i];
                string value = values[i];

                Assert.AreEqual(dictItem, listItem.Value);
                Assert.AreEqual(key, listItem.Key);
                Assert.AreEqual(value, listItem.Value);
            }
        }

        [Test]
        public void TestTryGetValue()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();
            string result = null;
            Assert.IsFalse(alphabetDict.TryGetValue("abc", out result));
            Assert.IsNull(result);
            Assert.IsTrue(alphabetDict.TryGetValue("z", out result));
            Assert.AreEqual("Z", result);
        }

        [Test]
        public void TestEnumerator()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();

            var keys = alphabetDict.Keys.ToList();
            Assert.AreEqual(26, keys.Count);

            int i = 0;
            foreach (KeyValuePair<string, string> kvp in alphabetDict)
            {
                KeyValuePair<string, string> value = alphabetDict[kvp.Key];
                Assert.AreEqual(kvp.Value, value.Value);
                i++;
            }
        }

        [Test]
        public void TestInvalidIndex()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                KeyValuePair<string, string> found = alphabetDict[100];
            });

        }

        [Test]
        public void TestMissingKey()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();

            Assert.Throws<KeyNotFoundException>(() =>
            {
                KeyValuePair<string, string> found = alphabetDict["abc"];
            });
        }

        //[Test]
        //public void TestUpdateExistingValue()
        //{
        //    var alphabetDict = GetAlphabetDictionary();
        //    Assert.IsTrue(alphabetDict.ContainsKey("c"));
        //    Assert.AreEqual(2, alphabetDict.IndexOf("c"));
        //    Assert.AreEqual(alphabetDict[2], "C");
        //    alphabetDict[2] = "CCC";
        //    Assert.IsTrue(alphabetDict.ContainsKey("c"));
        //    Assert.AreEqual(2, alphabetDict.IndexOf("c"));
        //    Assert.AreEqual(alphabetDict[2], "CCC");
        //}

        [Test]
        public void TestInsertValue()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();
            Assert.IsTrue(alphabetDict.ContainsKey("c"));
            Assert.AreEqual(2, alphabetDict.IndexOf("c"));
            Assert.AreEqual(alphabetDict[2].Value, "C");
            Assert.AreEqual(26, alphabetDict.Count);
            Assert.IsFalse(alphabetDict.Values.Contains("ABC"));

            alphabetDict.Insert(2, new KeyValuePair<string, string>("abc", "ABC"));
            Assert.IsTrue(alphabetDict.ContainsKey("c"));
            Assert.AreEqual(2, alphabetDict.IndexOf("abc"));
            Assert.AreEqual(alphabetDict[2].Value, "ABC");
            Assert.AreEqual(27, alphabetDict.Count);
            Assert.IsTrue(alphabetDict.Values.Contains("ABC"));
        }

        [Test]
        public void TestValueComparer()
        {
            OrderedDictionary<string, string> alphabetDict = GetAlphabetDictionary();
            Assert.IsFalse(alphabetDict.Values.Contains("a"));
            Assert.IsTrue(alphabetDict.Values.Contains("a", StringComparer.OrdinalIgnoreCase));
        }

        //[Test]
        //public void TestSortByKeys()
        //{
        //    var alphabetDict = GetAlphabetDictionary();
        //    var reverseAlphabetDict = GetAlphabetDictionary();
        //    Comparison<string> stringReverse = ((x, y) => (String.Equals(x, y) ? 0 : String.Compare(x, y) >= 1 ? -1 : 1));
        //    reverseAlphabetDict.SortKeys(stringReverse);
        //    for (int j = 0, k = 25; j < alphabetDict.Count; j++, k--)
        //    {
        //        var ascValue = alphabetDict.GetItem(j);
        //        var dscValue = reverseAlphabetDict.GetItem(k);
        //        Assert.AreEqual(ascValue.Key, dscValue.Key);
        //        Assert.AreEqual(ascValue.Value, dscValue.Value);
        //    }
        //}
    }
}
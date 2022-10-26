using NUnit.Framework;

namespace Examine.Test.Examine.Lucene.Search
{
    [TestFixture]
    public class StringTests //: AbstractPartialTrustFixture<StringTests>
    {
        [Test]
        public void Search_Remove_Stop_Words()
        {

            string stringPhrase1 = "hello my name is \"Shannon Deminick\" \"and I like to code\", here is a stop word and or two";
            string stringPhrase2 = "\"into the darkness\" this is a sentence with a quote at \"the front and the end\"";

            string parsed1 = stringPhrase1.RemoveStopWords();
            string parsed2 = stringPhrase2.RemoveStopWords();

            Assert.AreEqual("hello my name \"Shannon Deminick\" \"and I like to code\" , here stop word two", parsed1);
            Assert.AreEqual("\"into the darkness\" sentence quote \"the front and the end\"", parsed2);
        }

        [Test]
        public void Search_Remove_Stop_Words_Uneven_Quotes()
        {

            string stringPhrase1 = "hello my name is \"Shannon Deminick \"and I like to code\", here is a stop word and or two";

            string parsed1 = stringPhrase1.RemoveStopWords();

            Assert.AreEqual("hello my name \"Shannon Deminick\" I like code , here stop word two", parsed1);

        }

    }
}

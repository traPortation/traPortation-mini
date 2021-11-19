using NUnit.Framework;

namespace Tests
{
    public class Example
    {
        // Testアトリビュートを付ける
        [Test]
        public void ExampleTest()
        {
            // 条件式がtrueだったら成功
            Assert.That(1 < 10);
        }
    }
}
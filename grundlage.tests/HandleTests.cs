using NUnit.Framework;
using Shouldly;

namespace grundlage.tests
{
    public class HandleTests
    {
        [Test]
        public void Equality()
        {
            var a = new Handle("a");
            var aa = new Handle("a");

            a.ShouldBe(a);
            a.ShouldBe(aa);
        }

        [Test]
        public void Inequality()
        {
            var a = new Handle("a");
            var b = new Handle("b");
            
            a.ShouldNotBe(b);
        }
    }
}

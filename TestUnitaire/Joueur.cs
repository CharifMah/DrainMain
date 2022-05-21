using DrainMind;
using System;
using System.Threading;
using System.Windows.Controls;
using Xunit;

namespace TestUnitaire
{
    public class Joueur
    {

        [Fact]
        public void TestVie()
        {
            var t = new Thread(o =>
            {
                Vie v = new Vie(new Canvas(), 10, 50);
                Assert.Equal(v._Vie, 10);
                v.AddLife(50);
                Assert.Equal(v._Vie, 50);
                v.RemoveLife(49);
                Assert.Equal(v._Vie, 1);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();         
        }
    }
}
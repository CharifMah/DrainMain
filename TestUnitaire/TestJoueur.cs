using DrainMind;
using DrainMind.Metier.joueur;
using System;
using System.Threading;
using System.Windows.Controls;
using Xunit;

namespace TestUnitaire
{
    public class TestJoueur
    {

        [Fact]
        public void TestVie()
        {
            
            var t = new Thread(o =>
            {
                Vie v = new Vie(new Canvas(), 10, 50);
                Joueur s = new Joueur(0, 0, new Canvas(), new DrainMind.Metier.DrainMindGame(new Canvas(), new ScrollViewer(), new Canvas()), v);

                Assert.Equal(v._Vie, 10);
                s.LooseLife(10);
                Assert.Equal(v._Vie,0);

                v.AddLife(50);
                Assert.Equal(v._Vie, 50);

                v.RemoveLife(49);
                Assert.Equal(v._Vie, 1);

                v.AddLife(19);
                Assert.Equal(v._Vie, 20);

                v.RemoveEmptyLife(5);
                Assert.Equal(v._Vie, 15);

                v.AddEmptyLife(5);
                Assert.Equal(v._Vie, 15);

                v.AddLife(100);
                Assert.Equal(v._Vie, 55);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();         
        }
    }
}
using DrainMind;
using DrainMind.metier.Grille;
using DrainMind.metier.joueur;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using DrainMind.View;
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
                Canvas ui = new Canvas();
                Vie v = new Vie(ui, 10, 50);
                Joueur s = new Joueur(0, 0, v);

                Assert.Equal(v._Vie, 10);
                s.LooseLife(10);
                Assert.Equal(v._Vie,0);
                MyGrid.ResizeCanvas();
                v.AddLife(50);
                Assert.Equal(v._Vie, 50);

                v.RemoveLife(49);
                Assert.Equal(v._Vie, 1);
                MyGrid.ResizeCanvas();
                v.AddLife(19);
                Assert.Equal(v._Vie, 20);

                v.RemoveEmptyLife(5);
                Assert.Equal(v._Vie, 15);

                v.AddEmptyLife(5);
                Assert.Equal(v._Vie, 15);
                MyGrid.ResizeCanvas();
                v.AddLife(100);
                Assert.Equal(v._Vie, 55);
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();         
        }


        [Fact]
        public void TestEnemieEtJoueurCollision()
        {

            var t = new Thread(o =>
            {
                ///Creation d'une partie
                DrainMind.Metier.DrainMindGame g = new DrainMind.Metier.DrainMindGame();
                Vie v = new Vie(new Canvas(), 10, 50);
                Joueur s = new Joueur(0, 0,v);
                Enemie e = new Enemie(0, 0,"");

                //Meme Position
                Assert.Equal(e.IsCollide(s), true);



            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        [Fact]
        public void testXp()
        {

            var t = new Thread(o =>
            {
                ///Creation d'une partie
                StatsPersoModel.Instance.XP += 10;
                Assert.Equal(StatsPersoModel.Instance.XP, 10 * StatsPersoModel.Instance.Xpmult);

            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
         
        }
    }
}
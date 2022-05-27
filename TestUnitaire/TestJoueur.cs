using DrainMind.metier.Grille;
using DrainMind.metier.joueur;
using DrainMind.Metier.enemie;
using DrainMind.Metier.joueur;
using DrainMind.View;
using DrainMind.ViewModel;
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
                DrainMind.View.DrainMindView.MainCanvas = new Canvas();
                DrainMind.Metier.DrainMindGame g = new DrainMind.Metier.DrainMindGame();
                Vie v = new Vie(10, 50);
                Joueur s = new Joueur(0, 0);

                Assert.Equal(10,v._Vie);
                Assert.Equal(0,v._Vie);

                v.AddLife(50);
                Assert.Equal(50,v._Vie);

                v.RemoveLife(49);
                Assert.Equal(1, v._Vie);

                v.AddLife(19);
                Assert.Equal(20, v._Vie);

                v.RemoveEmptyLife(5);
                Assert.Equal(15, v._Vie);

                v.AddEmptyLife(5);
                Assert.Equal(15, v._Vie);

                v.AddLife(100);
                Assert.Equal(55, v._Vie);
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
                DrainMind.View.DrainMindView.MainCanvas = new Canvas();
                Vie v = new Vie(10, 50);
                Joueur s = new Joueur(0, 0);
                //Enemie e = new Enemie(0, 0,"");

                //Meme Position
                //Assert.True(e.IsCollide(s));

            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        [Fact]
        public void testXp()
        {

            var t = new Thread(o =>
            {
                DrainMind.View.DrainMindView.MainCanvas = new Canvas();
                DrainMindView.UIcanvas = new Canvas();
                DrainMind.Metier.DrainMindGame g = new DrainMind.Metier.DrainMindGame();
              
                new StatsPersoModel(0,10,10);
                ///Creation d'une partie
                StatsPersoModel.Instance.XP = 10;
                Assert.Equal(StatsPersoModel.Instance.XP, 10 * StatsPersoModel.Instance.Xpmult);

            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
         
        }

        [Fact]
        public void testNiveau()
        {
            var t = new Thread(o =>
            {
                DrainMind.View.DrainMindView.MainCanvas = new Canvas();
                DrainMindView.UIcanvas = new Canvas();
                DrainMind.Metier.DrainMindGame g = new DrainMind.Metier.DrainMindGame();
             
                ///Creation d'une partie
                var s = new StatsPersoModel(0, 1, 1);
                s.XP = 10;
                Assert.Equal(10 * s.Xpmult,s.XP);

                Assert.Equal(3,s.Life._Vie);

            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
    }
}
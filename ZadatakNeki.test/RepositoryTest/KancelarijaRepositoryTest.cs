using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Moq;
using Xunit;
using ZadatakNeki.Models;
using ZadatakNeki.Repository;

namespace ZadatakNeki.test.RepositoryTest
{

    public class KancelarijaRepositoryTest
    {
        [Fact]
        public void DajSveEntitete_KadRadi()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ToDoContext>().Setup(k => k.Kancelarije.ToList()).Returns(lazneKa());

                var klasa = mock.Create<KancelarijaRepository>();

                var ocekujes = lazneKa();
                var dobijas = klasa.DajSveEntitete();

                Assert.True(dobijas != null);
                Assert.Equal(ocekujes.Count, dobijas.Count);
                for (int i = 0; i < lazneKa().Count; i++)
                {
                    Assert.Equal(ocekujes[i], dobijas[i]);
                }
            }
        }

        public List<Kancelarija> lazneKa()
        {
            List<Kancelarija> kancelarije = new List<Kancelarija>()
            {
                new Kancelarija()
                {
                    Opis = "nestoVako"
                },
                new Kancelarija()
                {
                    Opis = "marketing"
                },
                new Kancelarija()
                {
                    Opis = "dizajn"
                },
                new Kancelarija()
                {
                    Opis = "sala za sastanke"
                }
            };
            return kancelarije;
        }

        [Theory]
        [InlineData(6)]
        public void EntitetPoId_KadRadi(long id)
        {
            Kancelarija ocekujes = lazneKa()[0];

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ToDoContext>().Setup(w => w.Kancelarije.Find(It.IsAny<long>())).Returns(ocekujes);

                var dobijas = mock.Create<KancelarijaRepository>().EntitetPoId(id);

                Assert.Equal(ocekujes, dobijas);
            }
        }

        //[Fact]
        //public void DodajEntitet_KadRadi()
        //{
        //    Kancelarija ocekujes = lazneKa()[0];

        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        mock.Mock<ToDoContext>().Setup(v => v.Kancelarije.Add(ocekujes));

                
        //    }
        //}

        //[Fact]
        //public void Izmeni_WhenWork()
        //{
        //}

        [Theory]
        [InlineData("kantina")]
        public void PretragaPoNazivu_WhenRadi(string ime)
        {
            using (var mock = AutoMock.GetLoose())
            {
                Kancelarija kancelarija = new Kancelarija() {Opis = "kantina"};

                mock.Mock<ToDoContext>()
                    .Setup(c => c.Kancelarije.Where(k => k.Opis == It.IsAny<string>()).ToString())
                    .Returns(kancelarija.ToString);

                var dobijas = mock.Create<KancelarijaRepository>().PretragaPoNazivu(ime);

                Assert.Equal(kancelarija, dobijas);
            }
        }
    }
}

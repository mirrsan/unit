using Autofac.Extras.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;
using Xunit;
using ZadatakNeki.Controllers;
using ZadatakNeki.Models;
using ZadatakNeki.Repository;
using ZadatakNeki.Repositorys.IRepository;

namespace ZadatakNeki.test.RepositoryTest
{
    public class OsobaRepositoryTest
    {
        [Fact]
        public void DajSveEntitete_KadRadi()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ToDoContext>().Setup(g => g.Osobe.ToList()).Returns(NekeOsobe());

                var ocekujes = NekeOsobe();
                var dobijas = mock.Create<OsobaRepository>().DajSveEntitete();

                Assert.Equal(ocekujes.Count, dobijas.Count);
            }
        }

        public List<Osoba> NekeOsobe()
        {
            List<Osoba> osobe = new List<Osoba>()
            {
                new Osoba()
                {
                    Ime = "mirsan",
                    Prezime = "kajovic",
                    Kancelarija = new Kancelarija() {Opis = "kuca"}
                },
                new Osoba()
                {
                    Ime = "maida",
                    Prezime = "rondic",
                    Kancelarija = new Kancelarija() {Opis = "sala za sastanke"}
                },
                new Osoba()
                {
                    Ime = "semsa",
                    Prezime = "semsovic",
                    Kancelarija = new Kancelarija() {Opis = "kantina"}
                }
            };
            return osobe;
        }
    }
}

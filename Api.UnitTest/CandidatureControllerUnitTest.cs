using AppRecrutement.Controllers;
using AppRecrutement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTest
{
    public class CandidatureControllerUnitTest
    {
        private readonly Candidature _candidature;
        private readonly AuthenticationContext _context;
        public Guid Id;
        public string IdCandidat;
        #region public Methods
        [Fact]
        public void ShouldReturnListOfExemple()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.getCandidatures();
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple1()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.GetCandidature(Id);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple2()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.AddCandidature(_candidature);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple3()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.PutCandidature(Id, _candidature);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple4()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.AnnulerCandidature(Id);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple5()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.getCadidaturesByCandidat(IdCandidat);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple6()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.getstats();
            //Assert//
            Assert.NotNull(result); // not null//

        }
        [Fact]
        public void ShouldReturnListOfExemple7()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new CandidatureController(_context);


            //Act// //execute notre controlleur//

            var result = controller.GetAllCandidatures();
            //Assert//
            Assert.NotNull(result); // not null//

        }



        #endregion
    }
}

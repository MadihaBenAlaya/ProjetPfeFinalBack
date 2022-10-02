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
    public class OffreControllerUnitTest
    {
        private readonly Offre _offre;
        private readonly AuthenticationContext _context;
        public Guid Id;
        
        #region public Methods
        [Fact]
        public void ShouldReturnListOfExemple()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new OffreController(_context);


            //Act// //execute notre controlleur//

            var result = controller.GetOffre(Id);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple1()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new OffreController(_context);


            //Act// //execute notre controlleur//

            var result = controller.GetAllOffres();
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple2()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new OffreController(_context);


            //Act// //execute notre controlleur//

            var result = controller.AddOffre(_offre);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple3()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new OffreController(_context);


            //Act// //execute notre controlleur//

            var result = controller.PutOffre(Id, _offre);
            //Assert//
            Assert.NotNull(result); // not null//

        }

        [Fact]
        public void ShouldReturnListOfExemple4()
        {
            //Arrange// //Appel notre controlleur//
            var controller = new OffreController(_context);


            //Act// //execute notre controlleur//

            var result = controller.DeleteOffre(Id);
            //Assert//
            Assert.NotNull(result); // not null//

        }

      
        #endregion
    }
}

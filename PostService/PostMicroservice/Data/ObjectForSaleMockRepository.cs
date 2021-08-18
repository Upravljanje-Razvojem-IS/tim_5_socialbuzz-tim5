using PostMicroservice.Models;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Data
{
    public class ObjectForSaleMockRepository : IObjectForSaleMockRepository
    {

        public static List<ObjectForSaleDTO> ObjectsForSale { get; set; } = new List<ObjectForSaleDTO>();

        public ObjectForSaleMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ObjectsForSale.AddRange(new List<ObjectForSaleDTO>
            {
                new ObjectForSaleDTO
                {
                    ObjectForSaleId = 5,
                    Name = "stolica"
                },
                new ObjectForSaleDTO
                {
                    ObjectForSaleId = 5,
                    Name = "krevet"
                },
                new ObjectForSaleDTO
                {
                    ObjectForSaleId = 5,
                    Name = "ormar"
                },




            });
        }

        public ObjectForSaleDTO GetObjectForSaleByID(string objectForSaleID)
        {
            throw new NotImplementedException();
        }

        public ObjectForSaleDTO GetObjectForSaleByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

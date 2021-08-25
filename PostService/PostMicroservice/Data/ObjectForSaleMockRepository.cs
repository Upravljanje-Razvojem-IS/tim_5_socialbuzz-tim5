using PostMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ObjectForSaleDTO GetObjectForSaleByID(int objectForSaleID)
        {
            return ObjectsForSale.FirstOrDefault(e => e.ObjectForSaleId == objectForSaleID);

        }

        public ObjectForSaleDTO GetObjectForSaleByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

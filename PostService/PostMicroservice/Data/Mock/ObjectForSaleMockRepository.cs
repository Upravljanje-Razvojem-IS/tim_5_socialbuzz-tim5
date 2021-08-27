using PostMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Data
{
    public class ObjectForSaleMockRepository : IObjectForSaleMockRepository
    {

        public static List<ObjectForSaleDto> ObjectsForSale { get; set; } = new List<ObjectForSaleDto>();

        public ObjectForSaleMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ObjectsForSale.AddRange(new List<ObjectForSaleDto>
            {
                new ObjectForSaleDto
                {
                    ObjectForSaleId = 5,
                    Name = "stolica"
                },
                new ObjectForSaleDto
                {
                    ObjectForSaleId = 5,
                    Name = "krevet"
                },
                new ObjectForSaleDto
                {
                    ObjectForSaleId = 5,
                    Name = "ormar"
                },




            });
        }

        public ObjectForSaleDto GetObjectForSaleByID(int objectForSaleID)
        {
            return ObjectsForSale.FirstOrDefault(e => e.ObjectForSaleId == objectForSaleID);

        }

        public ObjectForSaleDto GetObjectForSaleByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

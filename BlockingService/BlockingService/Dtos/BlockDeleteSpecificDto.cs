using System.Collections.Generic;

namespace BlockingService.Dtos
{
    public class BlockDeleteSpecificDto
    {
        public List<int> BlockedUserIds { get; set; }
    }
}

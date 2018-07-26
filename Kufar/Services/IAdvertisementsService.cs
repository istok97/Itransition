using System.Collections.Generic;
using Kufar.Models;

namespace Kufar.Services
{
    public interface IAdvertisementsService
    {
        IList<Advertisement> GetAdvertisements(int pageNumber, int pageSize, SortType sortType);

        int GetTotalCount();
    }
}

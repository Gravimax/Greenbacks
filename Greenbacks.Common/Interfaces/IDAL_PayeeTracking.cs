using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_PayeeTracking
    {
        List<PayeeTrackModel> GetPayeeTracks();

        List<PayeeTrackModel> GetPayeeTracks(int id);

        PayeeTrackModel GetPayeeTrack(int id);

        void CreatePayeeTrack(PayeeTrackModel payeeTrack);

        void UpdatePayeeTrack(PayeeTrackModel payeeTrack);

        void DeletePayeeTrack(int id);

        bool DoesPayeeTrackExist(int accountId, int payeeId);
    }
}

using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.Database
{
    public class DALPayeeTracking
    {
        public List<PayeeTrackModel> GetPayeeTracks()
        {
            var payees = new DAL_Payees().GetPayees();
            var accounts = new DAL_Accounts().GetAccounts();
            var tracks = new DAL_PayeeTracking().GetPayeeTracks();

            var temp = tracks.Select(x => new PayeeTrackModel
            {
                AccountId = x.AccountId,
                Amount = x.Amount,
                DueOn = x.DueOn,
                Note = x.Note,
                PayeeId = x.PayeeId,
                Id = x.Id
            }).ToList();

            foreach (var item in temp)
            {
                var account = accounts.Single(x => x.Id == item.AccountId);
                var payee = payees.Single(x => x.Id == item.PayeeId);
                item.BankName = "(" + account.BankName + ") " + account.Name;
                item.PayeeName = payee.Name;
                Utilities.FormatTrackDate(item);
            }

            return temp;
        }

        public List<PayeeTrackModel> GetPayeeTrack(int id)
        {

            var payee = new DAL_Payees().GetPayee(id);
            var accounts = new DAL_Accounts().GetAccounts();
            var tracks = new DAL_PayeeTracking().GetPayeeTracks(id);

            var temp = tracks.Select(x => new PayeeTrackModel
            {
                AccountId = x.AccountId,
                Amount = x.Amount,
                DueOn = x.DueOn,
                Note = x.Note,
                PayeeId = x.PayeeId,
                Id = x.Id
            }).ToList();

            foreach (var item in temp)
            {
                var account = accounts.Single(x => x.Id == item.AccountId);
                item.BankName = "(" + account.BankName + ") " + account.Name;
                item.PayeeName = payee.Name;
                Utilities.FormatTrackDate(item);
            }

            return temp;
        }

        public void AddPayeeTrack(PayeeTrackModel payeeTrack)
        {
            if (payeeTrack != null)
            {
                new DAL_PayeeTracking().CreatePayeeTrack(payeeTrack);
                return;
            }

            throw new ArgumentNullException(nameof(payeeTrack), "Argument cannot be null");
        }

        public void UpdatePayeeTrack(PayeeTrackModel payeeTrack)
        {
            if (payeeTrack != null)
            {
                new DAL_PayeeTracking().UpdatePayeeTrack(payeeTrack);
                return;
            }

            throw new ArgumentNullException(nameof(payeeTrack), "Argument cannot be null");
        }

        public void DeletePayeeTrack(PayeeTrackModel payeeTrack)
        {
            if (payeeTrack != null)
            {
                new DAL_PayeeTracking().DeletePayeeTrack(payeeTrack.Id);
                return;
            }

            throw new ArgumentNullException(nameof(payeeTrack), "Argument cannot be null");
        }

        public bool CheckTrackExists(int accountId, int payeeId)
        {
            return new DAL_PayeeTracking().DoesPayeeTrackExist(accountId, payeeId);
        }
    }
}

using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_PayeeTracking : IDAL_PayeeTracking
    {
        public void CreatePayeeTrack(PayeeTrackModel payeeTrack)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                PayeeTrack track = new PayeeTrack
                {
                    AccountId = payeeTrack.AccountId,
                    Amount = payeeTrack.Amount,
                    DueOn = payeeTrack.DueOn,
                    Note = payeeTrack.Note,
                    PayeeId = payeeTrack.PayeeId
                };

                context.PayeeTracks.Add(track);
                context.SaveChanges();

                payeeTrack.Id = track.Id;
            }
        }

        public void DeletePayeeTrack(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                PayeeTrack track = new PayeeTrack { Id = id };
                context.PayeeTracks.Attach(track);
                context.PayeeTracks.Remove(track);
                context.SaveChanges();
            }
        }

        public bool DoesPayeeTrackExist(int accountId, int payeeId)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.PayeeTracks.Count(x => x.AccountId == accountId && x.PayeeId == payeeId) > 0;
            }
        }

        public PayeeTrackModel GetPayeeTrack(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                var track = context.PayeeTracks.Select(y => new PayeeTrackModel
                {
                    AccountId = y.AccountId,
                    Amount = y.Amount,
                    DueOn = y.DueOn,
                    Note = y.Note,
                    PayeeId = y.PayeeId,
                    Id = y.Id
                }).Single(x => x.Id == id);

                var account = context.Accounts.Single(x => x.Id == track.AccountId);
                var payee = context.Payees.Single(x => x.Id == track.PayeeId);

                track.BankName = "(" + account.BankName + ") " + account.Name;
                track.PayeeName = payee.Name;
                FormatTrackDate(track);

                return track;
            }
        }

        public List<PayeeTrackModel> GetPayeeTracks()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                var payees = context.Payees.ToList();
                var accounts = context.Accounts.ToList();
                var tracks = context.PayeeTracks.ToList();

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
                    FormatTrackDate(item);
                }

                return temp;
            }
        }

        /// <summary>
        /// Gets the payee tracks.
        /// </summary>
        /// <param name="id">The payee id.</param>
        /// <returns></returns>
        public List<PayeeTrackModel> GetPayeeTracks(int payeeId)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                var payees = context.Payees.ToList();
                var accounts = context.Accounts.ToList();
                var tracks = context.PayeeTracks.Where(x => x.PayeeId == payeeId).ToList();

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
                    FormatTrackDate(item);
                }

                return temp;
            }
        }

        public void UpdatePayeeTrack(PayeeTrackModel payeeTrack)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                PayeeTrack track = context.PayeeTracks.Single(x => x.Id == payeeTrack.Id);

                track.Amount = payeeTrack.Amount;
                track.DueOn = payeeTrack.DueOn;
                track.Note = payeeTrack.Note;

                context.SaveChanges();
            }
        }

        private void FormatTrackDate(PayeeTrackModel track)
        {
            var now = DateTime.Now;

            if (track.DueOn < now.Day)
            {
                var future = now.AddMonths(1);
                track.DueDate = new DateTime(future.Year, future.Month, track.DueOn).ToShortDateString();
            }
            else
            {
                track.DueDate = new DateTime(now.Year, now.Month, track.DueOn).ToShortDateString();
            }
        }
    }
}

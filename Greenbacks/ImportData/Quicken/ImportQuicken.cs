using System;
using Greenbacks.ImportData.Models;

namespace Greenbacks.ImportData.Quicken
{
    public class ImportQuicken : IImport
    {
        private const string DataStart = "<OFX>";
        private const string DataEnd = "</OFX>";
        private const string HeaderStart = "<SIGNONMSGSRSV1>";
        private const string HeaderEnd = "</SIGNONMSGSRSV1>";
        private const string BankDataStart = "<BANKMSGSRSV1>";
        private const string BankDataEnd = "</BANKMSGSRSV1>";

        private const string TransSectionStart = "<STMTTRNRS>";
        private const string TransSectionEnd = "</STMTTRNRS>";
        private const string StatusStart = "<STATUS>";
        private const string StatusEnd = "</STATUS>";
        private const string CodeElement = "<CODE>";
        private const string SeverityElement = "<SEVERITY>";

        private const string TransactionsStart = "<STMTRS>";
        private const string TransactionsEnd = "</STMTRS>";
        private const string BankAcctInfoStart = "<BANKACCTFROM>";
        private const string BankAcctInfoEnd = "</BANKACCTFROM>";
        private const string BankIdElement = "<BANKID>";
        private const string AccountIdElement = "<ACCTID>";
        private const string AccountTypeElement = "<ACCTTYPE>";

        private const string BankTransListStart = "<BANKTRANLIST>";
        private const string BankTransListEnd = "</BANKTRANLIST>";
        private const string DateStartElement = "<DTSTART>";
        private const string DateEndElement = "<DTEND>";

        private const string TransactionStart = "<STMTTRN>";
        private const string TransactionEnd = "</STMTTRN>";
        private const string TransTypeElement = "<TRNTYPE>";
        private const string DatePostedElement = "<DTPOSTED>";
        private const string TransAmountElement = "<TRNAMT>";
        private const string TransIdElement = "<FITID>";
        private const string NameElement = "<NAME>";
        private const string MemoElement = "<MEMO>";

        private const string LedgerBalanceStart = "<LEDGERBAL>";
        private const string LedgerBalanceEnd = "</LEDGERBAL>";
        private const string AvailableBalanceStart = "<AVAILBAL>";
        private const string AvailableBalanceEnd = "</AVAILBAL>";
        private const string BalanceAmountElement = "<BALAMT>";
        private const string DateAsOfElement = "<DTASOF>";


        public ImportModel ProcessImport(string[] fileData)
        {
            ImportModel importModel = new ImportModel();
            importModel.TransactionList = new System.Collections.Generic.List<BankTransaction>();

            importModel.ImportHeader = ProcessHeader(fileData);

            // Look for STMTTRNRS
            // Then for STATUS
            GetSTMTTRNRS(importModel, fileData);

            // Then for STMTRS
            // Then for BANKACCTFROM
            GetBANKACCTFROM(importModel, fileData);

            // Then for BANKTRANLIST
            GetBANKTRANLIST(importModel, fileData);

            // Then for DTSTART & DTEND
            GetDateStartEnd(importModel, fileData);

            // Then for each transaction section: STMTTRN
            GetSTMTTRNRS(importModel, fileData);

            // Then for LEDGERBAL and BALAMT & DTASOF
            GetLedgerBalance(importModel, fileData);

            // Then for AVAILBAL and BALAMT & DTASOF
            GetAvailableValance(importModel, fileData);

            // Were done.

            return importModel;
        }


        private QuickenHeader ProcessHeader(string[] data)
        {
            QuickenHeader header = new QuickenHeader();

            foreach (var line in data)
            {
                if (line.StartsWith(DataStart) || string.IsNullOrEmpty(line)) { break; }

                string[] temp = line.Split(':') ;

                switch (temp[0])
                {
                    case "OFXHEADER":
                        header.OFXHeader = temp[1];
                        break;
                    case "DATA":
                        header.Data = temp[1];
                        break;
                    case "VERSION":
                        header.Version = temp[1];
                        break;
                    case "SECURITY":
                        header.Security = temp[1];
                        break;
                    case "ENCODING":
                        header.Encoding = temp[1];
                        break;
                    case "CHARSET":
                        header.Charset = temp[1];
                        break;
                    case "COMPRESSION":
                        header.Compression = temp[1];
                        break;
                    case "OLDFILEUID":
                        header.OldFileUID = temp[1];
                        break;
                    case "NEWFILEUID":
                        header.NewFileUID = temp[1];
                        break;
                    default:
                        break;
                }
            }

            return header;
        }


        private void GetSTMTTRNRS(ImportModel model, string[] data)
        {
            model.Status = new Status();

            int startIndex = GetElementIndex(data, TransSectionStart);
            int endIndex = GetElementIndex(data, TransactionsStart);

            if (startIndex == -1 || endIndex == -1) { return; }

            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i].Contains(CodeElement))
                {
                    model.Status.Code = GetElementData(data[i]);
                }
                else if (data[i].Contains(SeverityElement))
                {
                    model.Status.Severity = GetElementData(data[i]);
                }
            }
        }


        private void GetBANKACCTFROM(ImportModel model, string[] data)
        {
            int startIndex = GetElementIndex(data, BankAcctInfoStart);
            int endIndex = GetElementIndex(data, BankAcctInfoEnd);

            if (startIndex == -1 || endIndex == -1) { return; }

            model.AccountInfo = new BankAccountInfo();

            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i].Contains(BankIdElement))
                {
                    model.AccountInfo.BankId = GetElementData(data[i]);
                }
                else if (data[i].Contains(AccountIdElement))
                {
                    model.AccountInfo.AccountId = GetElementData(data[i]);
                }
                else if (data[i].Contains(AccountTypeElement))
                {
                    model.AccountInfo.AccountType = GetElementData(data[i]);
                }
            }
        }


        private void GetDateStartEnd(ImportModel model, string[] data)
        {
            int startIndex = GetElementIndex(data, BankTransListStart);
            int endIndex = startIndex + 3;

            if (startIndex == -1 || endIndex == -1) { return; }

            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i].Contains(DateStartElement))
                {
                    model.DateStart = ConvertToDateTime(GetElementData(data[i]), true);
                }
                else if (data[i].Contains(DateEndElement))
                {
                    model.DateEnd = ConvertToDateTime(GetElementData(data[i]), true);
                }
            }
        }


        private void GetBANKTRANLIST(ImportModel model, string[] data)
        {
            int startIndex = GetElementIndex(data, BankTransListStart);
            int endIndex = GetElementIndex(data, BankTransListEnd);

            if (startIndex == -1 || endIndex == -1) { return; }
            int tempIndex = startIndex;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i].Contains(BankTransListEnd)) { break; }

                if (data[i].Contains(TransactionStart))
                {
                    model.TransactionList.Add(ConvertTransaction(data, i, out tempIndex));
                    i = tempIndex;
                }
            }
        }


        private BankTransaction ConvertTransaction(string[] data, int index, out int newIndex)
        {
            BankTransaction trans = new BankTransaction();
            
            for (int i = index + 1; i < index + 10; i++)
            {
                if (data[i].Contains(TransactionEnd))
                {
                    index = i;
                    break;
                }

                if (data[i].Contains(TransTypeElement))
                {
                    trans.TransactionType = GetElementData(data[i]);
                }
                else if (data[i].Contains(DatePostedElement))
                {
                    trans.DatePosted = ConvertToDateTime(GetElementData(data[i])).Value;
                }
                else if (data[i].Contains(TransAmountElement))
                {
                    trans.TransactionAmount = ConvertMoney(GetElementData(data[i]));
                }
                else if (data[i].Contains(TransIdElement))
                {
                    trans.TransactionId = GetElementData(data[i]);
                }
                else if (data[i].Contains(NameElement))
                {
                    trans.Name = GetElementData(data[i]);
                }
                else if (data[i].Contains(MemoElement))
                {
                    trans.Memo = GetElementData(data[i]);
                }
            }

            newIndex = index;
            return trans;
        }


        public void GetLedgerBalance(ImportModel model, string[] data)
        {
            int startIndex = GetElementIndex(data, LedgerBalanceStart);
            int endIndex = GetElementIndex(data, LedgerBalanceEnd);

            if (startIndex == -1 || endIndex == -1) { return; }

            model.LedgerBalance = new LedgerBalance();

            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i].Contains(BalanceAmountElement))
                {
                    model.LedgerBalance.Amount = string.Format("{0:C}", ConvertMoney(GetElementData(data[i])));
                }
                else if (data[i].Contains(DateAsOfElement))
                {
                    model.LedgerBalance.DateAsOf = ConvertToDateTime(GetElementData(data[i]), true).Value.ToString();
                }
            }
        }


        public void GetAvailableValance(ImportModel model, string[] data)
        {
            int startIndex = GetElementIndex(data, AvailableBalanceStart);
            int endIndex = GetElementIndex(data, AvailableBalanceEnd);

            if (startIndex == -1 || endIndex == -1) { return; }

            model.AvailableBalance = new AvailableBalance();

            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i].Contains(BalanceAmountElement))
                {
                    model.AvailableBalance.Amount = string.Format("{0:C}", ConvertMoney(GetElementData(data[i])));
                }
                else if (data[i].Contains(DateAsOfElement))
                {
                    model.AvailableBalance.DateAsOf = ConvertToDateTime(GetElementData(data[i]), true).Value.ToString();
                }
            }
        }


        private BankTransaction GetTransaction(string[] data)
        {
            return null;
        }


        private string GetSectionContent(string data, string startElement, string endElement)
        {
            return null;
        }


        private string GetSectionContent(string data, int startIndex, string endElement)
        {
            return null;
        }


        private int GetElementIndex(string[] data, string element)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Contains(element))
                {
                    return i;
                }
            }

            return -1;
        }


        private string GetElementData(string element)
        {
            element = element.Trim();
            int index = element.IndexOf('>') + 1;
            return element.Substring(index, element.Length - index);
        }


        private DateTime? ConvertToDateTime(string data, bool convertFull = false)
        {
            if (!string.IsNullOrEmpty(data))
            {
                string year = data.Substring(0, 4);
                string month = data.Substring(4, 2);
                string day = data.Substring(6, 2);

                if (!convertFull)
                {
                    return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                }
                else
                {
                    string hours = data.Substring(8, 2);
                    string minutes = data.Substring(10, 2);
                    string seconds = data.Substring(12, 2);
                    //string milliseconds = data.Substring(16, 3);

                    return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hours), int.Parse(minutes), int.Parse(seconds));
                }
            }

            return null;
        }


        private decimal ConvertMoney(string data)
        {
            decimal amount = 0;

            if (!string.IsNullOrEmpty(data))
            {
                if (decimal.TryParse(data, out amount))
                {
                    return amount;
                }           
            }

            return amount;
        }
    }
}

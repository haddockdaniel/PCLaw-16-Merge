using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace PCLaw_16_Merge
{
    public class ConversionControl
    {
        public ConversionControl(string oldConnString, string newConnString)
        {
            oldConnectionString = oldConnString;
            newConnectionString = newConnString;
            getOldLawyerList(oldConnString);
            getOldMatterList(oldConnString);
            getOldClientList(oldConnString);
            getOldTOLlist(oldConnString);
        }

        List<Lawyer> lawList = new List<Lawyer>();
        List<Client> clientList = new List<Client>();
        List<Matter> matterList = new List<Matter>();
        List<TypeOfLaw> TOLlist = new List<TypeOfLaw>();
        Lawyer atty = null;
        Matter matt = null;
        TypeOfLaw tol = null;
        Client client = null;
        string oldConnectionString = "";
        string newConnectionString = "";

        private void getOldLawyerList(string connectionString)
        {
            string sQuery = @"SELECT lawyerid, LawInfNickName from [LawInf] where LawInfStatus = 0";
              
            SqlConnection con = new SqlConnection(connectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            atty = new Lawyer();
                            atty.oldID = int.Parse(reader["lawyerid"].ToString().Trim());

                            atty.nickName = reader["LawInfNickName"].ToString().Trim();

                            lawList.Add(atty);


                        }
                        catch (Exception inner1)
                        { MessageBox.Show("Inner: " + inner1.Message); }
                    }
                }
            }
        }

        private void getOldMatterList(string connectionString)
        {
            string sQuery = @"SELECT MatterID, MatterInfoMatterNum from mattinf where MatterInfoStatus = 0";

            SqlConnection con = new SqlConnection(connectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            matt = new Matter();
                            matt.oldID = int.Parse(reader["MatterID"].ToString().Trim());

                            matt.nickName = reader["MatterInfoMatterNum"].ToString().Trim();

                            matterList.Add(matt);


                        }
                        catch (Exception inner1)
                        { MessageBox.Show("Inner: " + inner1.Message); }
                    }
                }
            }
        }

        private void getOldTOLlist(string connectionString)
        {
            string sQuery = @"SELECT AreaOfLawID, AreaOfLawNickName from arealaw where AreaOfLawStatus = 0";

            SqlConnection con = new SqlConnection(connectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            tol = new TypeOfLaw();
                            tol.oldID = int.Parse(reader["AreaOfLawID"].ToString().Trim());

                            tol.nickName = reader["AreaOfLawNickName"].ToString().Trim();

                            TOLlist.Add(tol);


                        }
                        catch (Exception inner1)
                        { MessageBox.Show("Inner: " + inner1.Message); }
                    }
                }
            }
        }

        private void getOldClientList(string connectionString)
        {
            //we have to get the client list but to do that we have to get the contact list
            //and to do that we need the address list

            //get client list where they have a matter assigned and are status 0
            string sQuery = @"select clientinfoclientid, PersonID from clntinf where clientinfoclientid in (select ClientInfoClientID from mattinf where MatterInfoStatus = 0) and ClientInfoStatus = 0";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sQuery, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                client = new Client();
                                client.oldID = int.Parse(reader["clientinfoclientid"].ToString().Trim());
                                client.contact = new Contact();
                                client.contact.oldID = Convert.ToInt32(reader["PersonID"].ToString().Trim());
                                clientList.Add(client);
                            }
                            catch (Exception inner1)
                            { MessageBox.Show("Inner: " + inner1.Message); }
                        }
                    }
                }
            }

            //now get all contact and address ids
            sQuery = @"select contactid, ContactMainAddressID from Contact where contactid in (select personid from clntinf where ClientInfoClientID in (select clientinfoclientid from clntinf where clientinfoclientid in (select ClientInfoClientID from mattinf where MatterInfoStatus = 0))) and ContactStatus = 0";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sQuery, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                foreach (Client c in clientList)
                                {
                                    if (c.contact.oldID == Convert.ToInt32(reader["contactid"].ToString().Trim()))
                                    {
                                        c.address = new Address();
                                        c.address.oldID = Convert.ToInt32(reader["ContactMainAddressID"].ToString().Trim());
                                    }
                                }
                            }
                            catch (Exception inner1)
                            { MessageBox.Show("Inner: " + inner1.Message); }
                        }
                    }
                }
            }



        }

        private void getNewLawyerIDs(string connectionString)
        {
            string sQuery = @"SELECT * from [LawInf] where LawInfStatus = 0";

            SqlConnection con = new SqlConnection(connectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreach (Lawyer l in lawList)
                        {
                            if (reader["LawInfNickName"].ToString().Trim().Equals(l.nickName))
                                l.ID = int.Parse(reader["lawyerid"].ToString().Trim());

                        }

                    }
                }
            }
        }

        private void getNewTolIDs(string connectionString)
        {
            string sQuery = @"SELECT * from [AreaLaw] where AreaOfLawStatus = 0";

            SqlConnection con = new SqlConnection(connectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        foreach (TypeOfLaw t in TOLlist)
                        {
                            if (reader["AreaOfLawNickName"].ToString().Trim().Equals(t.nickName))
                                t.ID = int.Parse(reader["AreaOfLawID"].ToString().Trim());

                        }

                    }
                }
            }
        }



        public string MergeLawyers(string lawyers, bool respAtty, bool tol, string connString, string secondaryDB, string primaryDB)
        {
            string sQuery = "";
            string error = "";
            sQuery = @"insert into " + primaryDB + ".dbo.LawInf SELECT  [LawInfStatus] ,[LawInfDeptID] ,(select max(LawyerID) from [" + primaryDB + "].[dbo].LawInf) + row_number() over (order by [" + secondaryDB + "].[dbo].[LawInf].LawyerID) ,[LawInfClassID] ,[LawInfIncPct] ,[LawInfFeePct] ,[LawInfFeeBdg] ,[LawInfFlags] ,[LawInfDefTrust] ,[LawInfGenTrust] ,[LawInfTransNewID]  ,[LawInfTransChangeID]  ,[LawInfNickName] ,[LawInfInitials]  ,''  ,[LawInfMemberNum] ,[LawInfCertNum] ,[LawInfGSTNum]  ,[LawInfPSTNum] ,[LawInfLANum]  ,[LawInfICBCNum] ,[LawInfAttGenNum] ,[LawInfLawyerName] FROM [" + secondaryDB + "].[dbo].[LawInf] where LawInfStatus = 0";
            error = error + "\r\n" + MergeFunctions.RunSQLCommand(sQuery, connString);
            sQuery = @"update [LawInf] set LawInfTransNewID = lawyerid";
            error = error + "\r\n" + MergeFunctions.RunSQLCommand(sQuery, connString);
           // MessageBox.Show("Lawyers done: " + error);
            getNewLawyerIDs(newConnectionString);
            return error;
        }

        public string MergeMatters(string lawyers, bool respAtty, bool tol, string connString, string secondaryDB, string primaryDB)
        { 
            //now we add contacts with new ids and associated new addrinf ids
            string error = "";
            string customSQL = "";
            if (respAtty)
                customSQL = " and MatterInfoRespLwyr in (" + lawyers + ")";
            if (tol)
                customSQL = " and MatterInfoTypeofLaw in (" + lawyers + ")";
            string sQuery = @"SELECT * FROM [" + secondaryDB + "].[dbo].[mattinf] where MatterInfoStatus = 0 and MatterInfoMatterNum <> 'Prospect'" + customSQL;

            using (SqlConnection con = new SqlConnection(oldConnectionString))
            {
                using (var command = new SqlCommand(sQuery, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        int pk = Convert.ToInt32(MergeFunctions.GetMaxID("Select max(matterid) from mattinf", newConnectionString));
                        while (reader.Read())
                        {
                            pk = pk + 1;
                            int clientID = 0;
                            int clientID2 = 0;
                            int addyID = 0;
                            int addy2ID = 0;
                            int addy3ID = 0;
                            int addy4ID = 0;
                            int addy5ID = 0;
                            int respLawyerID = 0;
                            int typeOfLawID = 0;
                            int refLawyerID = 0;
                            try
                            {
                                //insert addresses while getting new ids in the process
                                using (SqlConnection connection = new SqlConnection(connString))
                                {
                                    connection.Open();
                                    using (SqlCommand command1 = new SqlCommand())
                                    {
                                        if (Convert.ToInt32(reader["ClientInfoClientID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //store the new contactid for later
                                                if (Convert.ToInt32(reader["ClientInfoClientID"].ToString().Trim()) == c.oldID)
                                                {
                                                    clientID = c.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoMajorClientID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //store the new contactid for later
                                                if (Convert.ToInt32(reader["MatterInfoMajorClientID"].ToString().Trim()) == c.oldID)
                                                {
                                                    clientID2 = c.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoRespLwyr"].ToString().Trim()) != 0)
                                        {
                                            foreach (Lawyer l in lawList) //get the new lawyerid so we can insert it
                                                if (Convert.ToInt32(reader["MatterInfoRespLwyr"].ToString().Trim()) == l.oldID)
                                                {
                                                    respLawyerID = l.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoRefLwyr"].ToString().Trim()) != 0)
                                        {
                                            foreach (Lawyer l in lawList) //get the new lawyerid so we can insert it
                                                if (Convert.ToInt32(reader["MatterInfoRefLwyr"].ToString().Trim()) == l.oldID)
                                                {
                                                    refLawyerID = l.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoBillAddID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new addyid so we can insert it
                                                if (Convert.ToInt32(reader["MatterInfoBillAddID"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addyID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoBillAddID2"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["MatterInfoBillAddID2"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addy2ID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoBillAddID3"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["MatterInfoBillAddID3"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addy3ID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoBillAddID4"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["MatterInfoBillAddID4"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addy4ID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoBillAddID5"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["MatterInfoBillAddID5"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addy5ID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["MatterInfoTypeofLaw"].ToString().Trim()) != 0)
                                        {
                                            foreach (TypeOfLaw t in TOLlist) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["MatterInfoTypeofLaw"].ToString().Trim()) == t.oldID)
                                                {
                                                    typeOfLawID = t.ID;
                                                    break;
                                                }
                                        }
                                        command1.Connection = connection;
                                        command1.CommandType = CommandType.Text;
                                        command1.CommandText = @"INSERT into [mattinf] ([MatterInfoStatus] ,[MatterID],[MatterInfoRespLwyr]  ,[MatterInfoMatterNum] ,[ClientInfoClientID]  ,[MatterInfoMajorClientID] ,[MatterInfoSortName]  ,[MatterInfoBillAddID] ,[MatterInfoBillAddID2] ,[MatterInfoBillAddID3]  ,[MatterInfoBillAddID4] ,[MatterInfoBillAddID5]  ,[MatterInfoSecretaryID] ,[MatterInfoOpenDate]  ,[MatterInfoGSTFees]  ,[MatterInfoGSTDisbs] ,[MatterInfoSalesTaxFees] ,[MatterInfoSalesTaxDisbs] ,[MatterInfoTransactionChange]   ,[MatterInfoTransactionNew]  ,[MatterInfoAutoTransferTrust]  ,[MatterInfoTypeofLaw]  ,[MatterInfoDefRateType] ,[MatterInfoDefRateAmt]  ,[MatterInfoRefLwyr] ,[MatterInfoAmtQuoted] ,[MatterInfoBudgetFees] ,[MatterInfoBudgetDisbs]  ,[MatterInfoCreditHold] ,[MatterInfoTrustHold]  ,[MatterInfoActiveMatter] ,[MatterInfoFormatFile] ,[MatterInfoBillFreq]  ,[MatterInfoFeeDiscSrchg] ,[MatterInfoFeeDiscSrchgPct] ,[MatterInfoFeeDiscSrchgDesc] ,[MatterInfoDisbDiscSrchg] ,[MatterInfoDisbDiscSrchgPct]  ,[MatterInfoDisbDiscSrchgDesc] ,[MatterInfoCntngMatt]  ,[MatterInfoCntngPct]  ,[MatterInfoProdRmndr] ,[MatterInfoSOAFormat] ,[MatterInfoChargeInt]  ,[MatterInfoUseDefIntRate] ,[MatterInfoIntRate]  ,[MatterInfoAutoFeeAlloc] ,[MatterInfoDefGSTCat] ,[MatterInfoUseTaskBill] ,[MatterInfoTaskListToUse] ,[MatterInfoReferredByType]  ,[MatterInfoDestroyDate] ,[MatterInfoSpareLong1] ,[MatterInfoSpareLong2] ,[MatterInfoSpareDouble] ,[MatterInfoFlags]  ,[MatterInfoHasTaskQuoted]  ,[MatterInfoQuotedType]  ,[MatterInfoAutoChrgType] ,[MatterInfoAutoChrgExplCode]  ,[MatterInfoAutoChrgGLAcct] ,[MatterInfoAutoChrgAmount] ,[MatterInfoReferredBy] ,[MatterInfoFileLocation]  ,[MatterInfoCrossReference] ,[MatterInfoCourtIDNumber]  ,[MatterInfoJurisdiction],[MatterInfoJudge] ,[MatterInfoSpareString] ,[MatterInfoFileDesc] ,[MatterInfoDocumentPath]  ,[MatterInfoInterestSettings] ,[MatterInfoUKLegalAid] ,[MatterInfoAutoChrgExpl] ,[MatterInfoCaseNotes] ,[MatterInfoExtraCaseNote1]   ,[MatterInfoExtraCaseNote2]  ,[MatterInfoExtraCaseNote3]  ,[MatterInfoExtraCaseNote4] ,[MatterInfoExtraCaseNote5] ,[MatterInfoExtraCaseNote6] ,[MatterInfoCollectionNotes] ,[MatterInfoExtraCollectNote1] ,[MatterInfoExtraCollectNote2] ,[MatterInfoExtraCollectNote3] ,[MatterInfoExtraCollectNote4] ,[MatterInfoExtraCollectNote5] ,[MatterInfoExtraCollectNote6],[MatterInfoQuickBooksID]) VALUES (@MatterInfoStatus ,@MatterID,@MatterInfoRespLwyr  ,@MatterInfoMatterNum ,@ClientInfoClientID  ,@MatterInfoMajorClientID ,@MatterInfoSortName  ,@MatterInfoBillAddID ,@MatterInfoBillAddID2 ,@MatterInfoBillAddID3  ,@MatterInfoBillAddID4 ,@MatterInfoBillAddID5  ,@MatterInfoSecretaryID ,@MatterInfoOpenDate  ,@MatterInfoGSTFees  ,@MatterInfoGSTDisbs ,@MatterInfoSalesTaxFees ,@MatterInfoSalesTaxDisbs ,@MatterInfoTransactionChange   ,@MatterInfoTransactionNew  ,@MatterInfoAutoTransferTrust  ,@MatterInfoTypeofLaw  ,@MatterInfoDefRateType ,@MatterInfoDefRateAmt  ,@MatterInfoRefLwyr ,@MatterInfoAmtQuoted ,@MatterInfoBudgetFees ,@MatterInfoBudgetDisbs  ,@MatterInfoCreditHold ,@MatterInfoTrustHold  ,@MatterInfoActiveMatter ,@MatterInfoFormatFile ,@MatterInfoBillFreq  ,@MatterInfoFeeDiscSrchg ,@MatterInfoFeeDiscSrchgPct ,@MatterInfoFeeDiscSrchgDesc ,@MatterInfoDisbDiscSrchg ,@MatterInfoDisbDiscSrchgPct  ,@MatterInfoDisbDiscSrchgDesc ,@MatterInfoCntngMatt  ,@MatterInfoCntngPct  ,@MatterInfoProdRmndr ,@MatterInfoSOAFormat ,@MatterInfoChargeInt  ,@MatterInfoUseDefIntRate ,@MatterInfoIntRate  ,@MatterInfoAutoFeeAlloc ,@MatterInfoDefGSTCat ,@MatterInfoUseTaskBill ,@MatterInfoTaskListToUse ,@MatterInfoReferredByType  ,@MatterInfoDestroyDate ,@MatterInfoSpareLong1 ,@MatterInfoSpareLong2 ,@MatterInfoSpareDouble ,@MatterInfoFlags  ,@MatterInfoHasTaskQuoted  ,@MatterInfoQuotedType  ,@MatterInfoAutoChrgType ,@MatterInfoAutoChrgExplCode  ,@MatterInfoAutoChrgGLAcct ,@MatterInfoAutoChrgAmount ,@MatterInfoReferredBy ,@MatterInfoFileLocation  ,@MatterInfoCrossReference ,@MatterInfoCourtIDNumber  ,@MatterInfoJurisdiction,@MatterInfoJudge ,@MatterInfoSpareString ,@MatterInfoFileDesc ,@MatterInfoDocumentPath  ,@MatterInfoInterestSettings ,@MatterInfoUKLegalAid ,@MatterInfoAutoChrgExpl ,@MatterInfoCaseNotes ,@MatterInfoExtraCaseNote1   ,@MatterInfoExtraCaseNote2  ,@MatterInfoExtraCaseNote3  ,@MatterInfoExtraCaseNote4 ,@MatterInfoExtraCaseNote5 ,@MatterInfoExtraCaseNote6 ,@MatterInfoCollectionNotes ,@MatterInfoExtraCollectNote1 ,@MatterInfoExtraCollectNote2 ,@MatterInfoExtraCollectNote3 ,@MatterInfoExtraCollectNote4 ,@MatterInfoExtraCollectNote5 ,@MatterInfoExtraCollectNote6,@MatterInfoQuickBooksID)";
                                        command1.Parameters.AddWithValue("@MatterInfoStatus", reader["MatterInfoStatus"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterID", pk);
                                        command1.Parameters.AddWithValue("@MatterInfoRespLwyr", respLawyerID);
                                        command1.Parameters.AddWithValue("@MatterInfoMatterNum", reader["MatterInfoMatterNum"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoClientID", clientID);
                                        command1.Parameters.AddWithValue("@MatterInfoMajorClientID", clientID2);
                                        command1.Parameters.AddWithValue("@MatterInfoSortName", reader["MatterInfoSortName"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoBillAddID", addyID);
                                        command1.Parameters.AddWithValue("@MatterInfoBillAddID2", addy2ID);
                                        command1.Parameters.AddWithValue("@MatterInfoBillAddID3", addy3ID);
                                        command1.Parameters.AddWithValue("@MatterInfoBillAddID4", addy4ID);
                                        command1.Parameters.AddWithValue("@MatterInfoBillAddID5", addy5ID);
                                        command1.Parameters.AddWithValue("@MatterInfoSecretaryID", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoOpenDate", reader["MatterInfoOpenDate"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoGSTFees", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoGSTDisbs", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoSalesTaxFees", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoSalesTaxDisbs", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoTransactionChange", pk);
                                        command1.Parameters.AddWithValue("@MatterInfoTransactionNew", pk);
                                        command1.Parameters.AddWithValue("@MatterInfoAutoTransferTrust", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoTypeofLaw", typeOfLawID);
                                        command1.Parameters.AddWithValue("@MatterInfoDefRateType", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoRefLwyr", refLawyerID);
                                        command1.Parameters.AddWithValue("@MatterInfoAmtQuoted", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoBudgetFees", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoBudgetDisbs", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoCreditHold", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoTrustHold", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoActiveMatter", 1);
                                        command1.Parameters.AddWithValue("@MatterInfoFormatFile", reader["MatterInfoFormatFile"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoBillFreq", reader["MatterInfoBillFreq"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoFeeDiscSrchg", reader["MatterInfoFeeDiscSrchg"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoFeeDiscSrchgPct", reader["MatterInfoFeeDiscSrchgPct"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoFeeDiscSrchgDesc", reader["MatterInfoFeeDiscSrchgDesc"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoDisbDiscSrchg", reader["MatterInfoDisbDiscSrchg"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoDisbDiscSrchgPct", reader["MatterInfoDisbDiscSrchgPct"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoDisbDiscSrchgDesc", reader["MatterInfoDisbDiscSrchgDesc"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoCntngMatt", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoCntngPct", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoProdRmndr", reader["MatterInfoProdRmndr"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoSOAFormat", reader["MatterInfoSOAFormat"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoChargeInt", reader["MatterInfoChargeInt"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoUseDefIntRate", reader["MatterInfoUseDefIntRate"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoIntRate", reader["MatterInfoIntRate"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoAutoFeeAlloc", reader["MatterInfoAutoFeeAlloc"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoDefGSTCat", reader["MatterInfoDefGSTCat"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoUseTaskBill", reader["MatterInfoUseTaskBill"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoTaskListToUse", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoReferredByType", reader["MatterInfoReferredByType"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoDestroyDate", reader["MatterInfoDestroyDate"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoSpareLong1", reader["MatterInfoSpareLong1"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoSpareLong2", reader["MatterInfoSpareLong2"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoFlags", reader["MatterInfoFlags"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoHasTaskQuoted", reader["MatterInfoHasTaskQuoted"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoQuotedType", reader["MatterInfoQuotedType"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoAutoChrgType", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoAutoChrgExplCode", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoAutoChrgGLAcct", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoAutoChrgAmount", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoReferredBy", reader["MatterInfoReferredBy"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoFileLocation", reader["MatterInfoFileLocation"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoCrossReference", reader["MatterInfoCrossReference"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoCourtIDNumber", reader["MatterInfoCourtIDNumber"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoJurisdiction", reader["MatterInfoJurisdiction"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoJudge", reader["MatterInfoJudge"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoSpareString", reader["MatterInfoSpareString"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoFileDesc", reader["MatterInfoFileDesc"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoDocumentPath", reader["MatterInfoDocumentPath"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoInterestSettings", reader["MatterInfoInterestSettings"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoUKLegalAid", reader["MatterInfoUKLegalAid"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoAutoChrgExpl", 0);
                                        command1.Parameters.AddWithValue("@MatterInfoCaseNotes", reader["MatterInfoCaseNotes"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCaseNote1", reader["MatterInfoExtraCaseNote1"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCaseNote2", reader["MatterInfoExtraCaseNote2"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCaseNote3", reader["MatterInfoExtraCaseNote3"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCaseNote4", reader["MatterInfoExtraCaseNote4"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCaseNote5", reader["MatterInfoExtraCaseNote5"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCaseNote6", reader["MatterInfoExtraCaseNote6"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoCollectionNotes", reader["MatterInfoCollectionNotes"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCollectNote1", reader["MatterInfoExtraCollectNote1"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCollectNote2", reader["MatterInfoExtraCollectNote2"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCollectNote3", reader["MatterInfoExtraCollectNote3"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCollectNote4", reader["MatterInfoExtraCollectNote4"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCollectNote5", reader["MatterInfoExtraCollectNote5"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoExtraCollectNote6", reader["MatterInfoExtraCollectNote6"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@MatterInfoQuickBooksID", 0);

                                        //make double
                                        command1.Parameters.Add("@MatterInfoDefRateAmt", SqlDbType.Decimal).Value = reader["MatterInfoDefRateAmt"].ToString().Trim();
                                        command1.Parameters.Add("@MatterInfoSpareDouble", SqlDbType.Decimal).Value = reader["MatterInfoSpareDouble"].ToString().Trim();
                                        try
                                        {
                                            command1.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex1)
                                        {
                                            MessageBox.Show(ex1.Message);
                                        }


                                    }

                                    connection.Close();
                                }


                            }
                            catch (Exception inner1)
                            { MessageBox.Show("Inner: " + inner1.Message); }
                        }
                    }
                }
            }

            return error;
        }

        public string MergeClients(string lawyers, bool respAtty, bool tol, string connString, string secondaryDB, string primaryDB)
        {
            //here we have to merge addrinf, contact and clntinf without adding any vendors or other contacts, addresses or clients
            //We only add clients that are assigned to an active matter
            string error = "";
            //inserts addrinf
            string sQuery = @"SELECT * FROM [" + secondaryDB + "].[dbo].[AddrInf] where AddressInfoStatus = 0 and addressid in (select ContactMainAddressID from Contact where contactid in (select contactid from Contact where contactid in (select personid from clntinf where ClientInfoClientID in (select clientinfoclientid from clntinf where clientinfoclientid in (select ClientInfoClientID from mattinf where MatterInfoStatus = 0))) and ContactStatus = 0))";

            using (SqlConnection con = new SqlConnection(oldConnectionString))
            {
                using (var command = new SqlCommand(sQuery, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        int pk = Convert.ToInt32(MergeFunctions.GetMaxID("Select max(addressid) from addrinf", newConnectionString));
                        while (reader.Read())
                        {
                            pk = pk + 1;
                            try
                            {
                                //insert addresses while getting new ids in the process
                                using (SqlConnection connection = new SqlConnection(connString))
                                {
                                    connection.Open();
                                    using (SqlCommand command1 = new SqlCommand())
                                    {
                                        foreach (Client c in clientList)
                                            if (Convert.ToInt32(reader["AddressID"].ToString().Trim()) == c.address.oldID)
                                                c.address.ID = pk;
                                        command1.Connection = connection;
                                        command1.CommandType = CommandType.Text;
                                        command1.CommandText = @"INSERT into [addrinf] ([AddressInfoStatus] ,[AddressID],[AddressInfoAddrType] ,[AddressInfoAddrGroup] ,[AddressInfoLastName] ,[AddressInfoTitle] ,[AddressInfoFirst]  ,[AddressInfoMiddle]  ,[AddressInfoSuffix] ,[AddressInfoAttLast] ,[AddressInfoAttPosition] ,[AddressInfoAddrLine1] ,[AddressInfoAddrLine2],[AddressInfoCompany],[AddressInfoCity] ,[AddressInfoProv]  ,[AddressInfoCode] ,[AddressInfoCountry],[AddressInfoBusPhone],[AddressInfoHomePhone]  ,[AddressInfoFaxPhone] ,[AddressInfoHomeFaxPhone] ,[AddressInfoCellPhone],[AddressInfoCarPhone] ,[AddressInfoPager],[AddressInfoInternetAddr] ,[AddressInfoHomeEmail] ,[AddressInfoWebPage] ,[AddressInfoExtra],[AddressInfoAlias]) VALUES (@AddressInfoStatus ,@AddressID,@AddressInfoAddrType ,@AddressInfoAddrGroup ,@AddressInfoLastName ,@AddressInfoTitle ,@AddressInfoFirst  ,@AddressInfoMiddle  ,@AddressInfoSuffix ,@AddressInfoAttLast ,@AddressInfoAttPosition ,@AddressInfoAddrLine1 ,@AddressInfoAddrLine2,@AddressInfoCompany,@AddressInfoCity ,@AddressInfoProv  ,@AddressInfoCode ,@AddressInfoCountry,@AddressInfoBusPhone,@AddressInfoHomePhone  ,@AddressInfoFaxPhone ,@AddressInfoHomeFaxPhone ,@AddressInfoCellPhone,@AddressInfoCarPhone ,@AddressInfoPager,@AddressInfoInternetAddr ,@AddressInfoHomeEmail ,@AddressInfoWebPage ,@AddressInfoExtra,@AddressInfoAlias)";
                                        command1.Parameters.AddWithValue("@AddressInfoStatus", reader["AddressInfoStatus"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressID", pk);
                                        command1.Parameters.AddWithValue("@AddressInfoAddrType", reader["AddressInfoAddrType"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoAddrGroup", reader["AddressInfoAddrGroup"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoLastName", reader["AddressInfoLastName"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoTitle", reader["AddressInfoTitle"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoFirst", reader["AddressInfoFirst"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoMiddle", reader["AddressInfoMiddle"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoSuffix", reader["AddressInfoSuffix"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoAttLast", reader["AddressInfoAttLast"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoAttPosition", reader["AddressInfoAttPosition"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoAddrLine1", reader["AddressInfoAddrLine1"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoAddrLine2", reader["AddressInfoAddrLine2"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoCompany", reader["AddressInfoCompany"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoCity", reader["AddressInfoCity"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoProv", reader["AddressInfoProv"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoCode", reader["AddressInfoCode"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoCountry", reader["AddressInfoCountry"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoBusPhone", reader["AddressInfoBusPhone"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoHomePhone", reader["AddressInfoHomePhone"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoFaxPhone", reader["AddressInfoFaxPhone"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoHomeFaxPhone", reader["AddressInfoHomeFaxPhone"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoCellPhone", reader["AddressInfoCellPhone"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoCarPhone", reader["AddressInfoCarPhone"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoPager", reader["AddressInfoPager"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoInternetAddr", reader["AddressInfoInternetAddr"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoHomeEmail", reader["AddressInfoHomeEmail"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoWebPage", reader["AddressInfoWebPage"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoExtra", reader["AddressInfoExtra"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@AddressInfoAlias", reader["AddressInfoAlias"].ToString().Trim());
                                        try
                                        {
                                            command1.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex1)
                                        {
                                            MessageBox.Show(ex1.Message);
                                        }


                                    }

                                    connection.Close();
                                }


                            }
                            catch (Exception inner1)
                            { MessageBox.Show("Inner: " + inner1.Message); }
                        }
                    }
                }
            }

            string customSQL = "";
            if (respAtty)
                customSQL = " and MatterInfoRespLwyr in (" + lawyers + ")";
            if (tol)
                customSQL = " and MatterInfoTypeofLaw in (" + lawyers + ")";
            //now we add contacts with new ids and associated new addrinf ids
            sQuery = @"SELECT * FROM [" + secondaryDB + "].[dbo].[Contact] where ContactStatus = 0 and contactid in (select personid from clntinf where ClientInfoClientID in (select clientinfoclientid from clntinf where clientinfoclientid in (select ClientInfoClientID from mattinf where MatterInfoStatus = 0" + customSQL + ")))";



            using (SqlConnection con = new SqlConnection(oldConnectionString))
            {
                using (var command = new SqlCommand(sQuery, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        int pk = Convert.ToInt32(MergeFunctions.GetMaxID("Select max(contactid) from contact", newConnectionString));
                        while (reader.Read())
                        {
                            pk = pk + 1;
                            int addyID = 0;
                            int addy2ID = 0;
                            int lawyerid = 0;
                            try
                            {
                                //insert addresses while getting new ids in the process
                                using (SqlConnection connection = new SqlConnection(connString))
                                {
                                    connection.Open();
                                    using (SqlCommand command1 = new SqlCommand())
                                    {
                                        foreach (Client c in clientList) //store the new contactid for later
                                            if (Convert.ToInt32(reader["ContactID"].ToString().Trim()) == c.contact.oldID)
                                            {
                                                c.contact.ID = pk;
                                                break;
                                            }
                                        if (Convert.ToInt32(reader["ContactLawyerID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Lawyer l in lawList) //get the new lawyerid so we can insert it
                                                if (Convert.ToInt32(reader["ContactLawyerID"].ToString().Trim()) == l.oldID)
                                                {
                                                    lawyerid = l.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["ContactMainAddressID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new addyid so we can insert it
                                                if (Convert.ToInt32(reader["ContactMainAddressID"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addyID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["ContactAddressID2"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["ContactAddressID2"].ToString().Trim()) == c.address.oldID)
                                                {
                                                    addy2ID = c.address.ID;
                                                    break;
                                                }
                                        }
                                        command1.Connection = connection;
                                        command1.CommandType = CommandType.Text;
                                        command1.CommandText = @"INSERT into [contact] ([ContactStatus] ,[ContactID],[ContactMainContactType] ,[ContactLawyerID] ,[ContactFlags] ,[ContactTransactionNew] ,[ContactTransactionChange] ,[ContactMainAddressID] ,[ContactAddressID2],[ContactEntityTypes]  ,[ContactFirstContact] ,[ContactLastContact] ,[ContactExtra1] ,[ContactExtra2] ,[ContactExtra3]  ,[ContactFirstPurchases] ,[ContactTotalPurchases] ,[ContactAmountOwing] ,[ContactSortName] ,[ContactFirmName],[ContactQuickBooksID],[ContactNotes] ,[ContactUserFields] ,[ContactAccountNumber],[ContactActivityStatus],[ContactExtraStr]) VALUES (@ContactStatus ,@ContactID,@ContactMainContactType ,@ContactLawyerID ,@ContactFlags ,@ContactTransactionNew ,@ContactTransactionChange ,@ContactMainAddressID ,@ContactAddressID2,@ContactEntityTypes  ,@ContactFirstContact ,@ContactLastContact ,@ContactExtra1 ,@ContactExtra2 ,@ContactExtra3  ,@ContactFirstPurchases ,@ContactTotalPurchases ,@ContactAmountOwing ,@ContactSortName ,@ContactFirmName,@ContactQuickBooksID,@ContactNotes ,@ContactUserFields ,@ContactAccountNumber,@ContactActivityStatus,@ContactExtraStr)";
                                        command1.Parameters.AddWithValue("@ContactStatus", reader["ContactStatus"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactID", pk);
                                        command1.Parameters.AddWithValue("@ContactMainContactType", 0);
                                        command1.Parameters.AddWithValue("@ContactLawyerID", lawyerid);
                                        command1.Parameters.AddWithValue("@ContactFlags", reader["ContactFlags"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactTransactionNew", pk);
                                        command1.Parameters.AddWithValue("@ContactTransactionChange", pk);
                                        command1.Parameters.AddWithValue("@ContactMainAddressID", addyID);
                                        command1.Parameters.AddWithValue("@ContactAddressID2", addy2ID);
                                        command1.Parameters.AddWithValue("@ContactEntityTypes", 0);
                                        command1.Parameters.AddWithValue("@ContactFirstContact", reader["ContactFirstContact"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactLastContact", reader["ContactLastContact"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactExtra1", reader["ContactExtra1"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactExtra2", reader["ContactExtra2"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactExtra3", reader["ContactExtra3"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactFirstPurchases", 0);
                                        command1.Parameters.AddWithValue("@ContactTotalPurchases", 0);
                                        command1.Parameters.AddWithValue("@ContactAmountOwing", 0);
                                        command1.Parameters.AddWithValue("@ContactSortName", reader["ContactSortName"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactFirmName", reader["ContactFirmName"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactQuickBooksID", "");
                                        command1.Parameters.AddWithValue("@ContactNotes", reader["ContactNotes"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactUserFields", "");
                                        command1.Parameters.AddWithValue("@ContactAccountNumber", reader["ContactAccountNumber"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactActivityStatus", reader["ContactActivityStatus"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ContactExtraStr", reader["ContactExtraStr"].ToString().Trim());
                                        try
                                        {
                                            command1.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex1)
                                        {
                                            MessageBox.Show(ex1.Message);
                                        }


                                    }

                                    connection.Close();
                                }


                            }
                            catch (Exception inner1)
                            { MessageBox.Show("Inner: " + inner1.Message); }
                        }
                    }
                }
            }

            customSQL = "";
            if (respAtty)
                customSQL = " and MatterInfoRespLwyr in (" + lawyers + ")";
            if (tol)
                customSQL = " and MatterInfoTypeofLaw in (" + lawyers + ")";
            //now we add contacts with new ids and associated new addrinf ids
            sQuery = @"SELECT * FROM [" + secondaryDB + "].[dbo].[clntinf] where clientinfoclientid in (select ClientInfoClientID from mattinf where MatterInfoStatus = 0" + customSQL + ") and ClientInfoStatus = 0 and ClientInfoClientNum <> 'Prospect Client'";

            using (SqlConnection con = new SqlConnection(oldConnectionString))
            {
                using (var command = new SqlCommand(sQuery, con))
                {
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        int pk = Convert.ToInt32(MergeFunctions.GetMaxID("Select max(ClientInfoClientID) from Clntinf", newConnectionString));
                        while (reader.Read())
                        {
                            pk = pk + 1;
                            int contactID = 0;
                            int contact2ID = 0;
                            int lawyerid = 0;
                            try
                            {
                                //insert addresses while getting new ids in the process
                                using (SqlConnection connection = new SqlConnection(connString))
                                {
                                    connection.Open();
                                    using (SqlCommand command1 = new SqlCommand())
                                    {
                                        foreach (Client c in clientList) //store the new clientid for later
                                            if (Convert.ToInt32(reader["ClientInfoClientID"].ToString().Trim()) == c.oldID)
                                            {
                                                c.ID = pk;
                                                break;
                                            }
                                        if (Convert.ToInt32(reader["LawyerID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Lawyer l in lawList) //get the new lawyerid so we can insert it
                                                if (Convert.ToInt32(reader["LawyerID"].ToString().Trim()) == l.oldID)
                                                {
                                                    lawyerid = l.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["PersonID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new addyid so we can insert it
                                                if (Convert.ToInt32(reader["PersonID"].ToString().Trim()) == c.contact.oldID)
                                                {
                                                    contactID = c.contact.ID;
                                                    break;
                                                }
                                        }
                                        if (Convert.ToInt32(reader["ClientInfoSecondaryPersonID"].ToString().Trim()) != 0)
                                        {
                                            foreach (Client c in clientList) //get the new lawyerid so we can insert it (for field 2)
                                                if (Convert.ToInt32(reader["ClientInfoSecondaryPersonID"].ToString().Trim()) == c.contact.oldID)
                                                {
                                                    contact2ID = c.contact.ID;
                                                    break;
                                                }
                                        }
                                        command1.Connection = connection;
                                        command1.CommandType = CommandType.Text;
                                        command1.CommandText = @"INSERT into [clntinf] ([ClientInfoStatus]  ,[ClientInfoClientID] ,[ClientInfoClientNum] ,[PersonID] ,[ClientInfoMajorClient] ,[ClientInfoFlags] ,[LawyerID] ,[ClientInfoMatterBase] ,[ClientInfoMatterExt]  ,[ClientInfoSortName] ,[ClientInfoTransactionChange] ,[ClientInfoTransactionNew] ,[ClientInfoLinkHostID] ,[ClientInfoExtra1]  ,[ClientInfoExtra2] ,[ClientInfoDisplayAs],[ClientInfoSecondaryPersonID] ,[ClientInfoDocumentPath] ,[ClientInfoClientDefault] ,[ClientInfoStrExtra1]) VALUES (@ClientInfoStatus  ,@ClientInfoClientID ,@ClientInfoClientNum ,@PersonID ,@ClientInfoMajorClient ,@ClientInfoFlags ,@LawyerID ,@ClientInfoMatterBase ,@ClientInfoMatterExt  ,@ClientInfoSortName ,@ClientInfoTransactionChange ,@ClientInfoTransactionNew ,@ClientInfoLinkHostID ,@ClientInfoExtra1  ,@ClientInfoExtra2 ,@ClientInfoDisplayAs,@ClientInfoSecondaryPersonID ,@ClientInfoDocumentPath ,@ClientInfoClientDefault ,@ClientInfoStrExtra1)";
                                        command1.Parameters.AddWithValue("@ClientInfoStatus", reader["ClientInfoStatus"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoClientID", pk);
                                        command1.Parameters.AddWithValue("@ClientInfoClientNum", reader["ClientInfoClientNum"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@PersonID", contactID);
                                        command1.Parameters.AddWithValue("@ClientInfoMajorClient", reader["ClientInfoMajorClient"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoFlags", reader["ClientInfoFlags"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@LawyerID", lawyerid);
                                        command1.Parameters.AddWithValue("@ClientInfoMatterBase", reader["ClientInfoMatterBase"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoMatterExt", reader["ClientInfoMatterExt"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoSortName", reader["ClientInfoSortName"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoTransactionChange", pk);
                                        command1.Parameters.AddWithValue("@ClientInfoTransactionNew", pk);
                                        command1.Parameters.AddWithValue("@ClientInfoLinkHostID", 0);
                                        command1.Parameters.AddWithValue("@ClientInfoExtra1", reader["ClientInfoExtra1"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoExtra2", reader["ClientInfoExtra2"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoDisplayAs", reader["ClientInfoDisplayAs"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoSecondaryPersonID", contact2ID);
                                        command1.Parameters.AddWithValue("@ClientInfoDocumentPath", reader["ClientInfoDocumentPath"].ToString().Trim());
                                        command1.Parameters.AddWithValue("@ClientInfoClientDefault", "");
                                        command1.Parameters.AddWithValue("@ClientInfoStrExtra1", reader["ClientInfoStrExtra1"].ToString().Trim());
                                        try
                                        {
                                            command1.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex1)
                                        {
                                            MessageBox.Show(ex1.Message);
                                        }


                                    }

                                    connection.Close();
                                }


                            }
                            catch (Exception inner1)
                            { MessageBox.Show("Inner: " + inner1.Message); }
                        }
                    }
                }
            }

            return error;
        }

        public string MergeUsers(string lawyers, bool respAtty, bool tol, string connString, string secondaryDB, string primaryDB)
        {
            string error = "";
            string sQuery = @"SELECT * from [Pcuser] where PClawUserStatus = 0 and PClawUserNN <> 'ADMIN'";
            //get all items from the secondary books
            SqlConnection con = new SqlConnection(oldConnectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    int pk = Convert.ToInt32(MergeFunctions.GetMaxID("Select max(pclawuserid) from pcuser", newConnectionString));
                    while (reader.Read())
                    {
                        pk = pk + 1;
                        try
                        {
                            //insert them into the primary books while looking up the appropriate new ids
                            using (SqlConnection connection = new SqlConnection(connString))
                            {
                                connection.Open();
                                using (SqlCommand command1 = new SqlCommand())
                                {
                                    int newID = 0;
                                    if (Convert.ToInt32(reader["PClawUserLawyerId"].ToString().Trim()) != 0)
                                    {
                                        foreach (Lawyer l in lawList)
                                            if (Convert.ToInt32(reader["PClawUserLawyerId"].ToString().Trim()) == l.oldID)
                                            {
                                                newID = l.ID;
                                                break;
                                            }
                                    }
                                    command1.Connection = connection;
                                    command1.CommandType = CommandType.Text;
                                    command1.CommandText = @"INSERT into [pcuser] ([PClawUserStatus] ,[PClawUserID] ,[PClawUserNN] ,[PClawUserName] ,[PClawUserDescr] ,[PClawUserPassword] ,[PClawUserFlags] ,[PClawUserPermission] ,[PClawUserDate] ,[PClawUserLawyerId] ,[PClawUserEmail] ,[PClawUserExtra1],[PClawUserExtra2]) VALUES (@PClawUserStatus ,@PClawUserID ,@PClawUserNN ,@PClawUserName ,@PClawUserDescr ,@PClawUserPassword ,@PClawUserFlags ,@PClawUserPermission ,@PClawUserDate ,@PClawUserLawyerId ,@PClawUserEmail ,@PClawUserExtra1,@PClawUserExtra2)";
                                    command1.Parameters.AddWithValue("@PClawUserStatus", reader["PClawUserStatus"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserID", pk);
                                    command1.Parameters.AddWithValue("@PClawUserNN", reader["PClawUserNN"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserName", reader["PClawUserName"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserDescr", reader["PClawUserDescr"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserPassword", reader["PClawUserPassword"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserFlags", reader["PClawUserFlags"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserPermission", reader["PClawUserPermission"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserDate", reader["PClawUserDate"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserLawyerId", newID);
                                    command1.Parameters.AddWithValue("@PClawUserEmail", reader["PClawUserEmail"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserExtra1", reader["PClawUserExtra1"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@PClawUserExtra2", reader["PClawUserExtra2"].ToString().Trim());
                                    try
                                    {
                                        command1.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex1)
                                    {
                                        MessageBox.Show(ex1.Message);
                                    }


                                }

                                connection.Close();
                            }


                        }
                        catch (Exception inner1)
                        { MessageBox.Show("Inner: " + inner1.Message); }
                    }
                }
            }
            return error;
        }

        public string MergeTypesOfLaw(string lawyers, bool respAtty, bool tol, string connString, string secondaryDB, string primaryDB)
        {
            string sQuery = "";
            string error = "";
            sQuery = @"insert into " + primaryDB + ".dbo.AreaLaw SELECT  [AreaOfLawStatus] ,(select max(AreaOfLawID) from [" + primaryDB + "].[dbo].AreaLaw) + row_number() over (order by [" + secondaryDB + "].[dbo].[AreaLaw].AreaOfLawID) ,[AreaOfLawFlags] ,[AreaOfLawTransNewID] ,[AreaOfLawTransChangeID] ,[AreaOfLawNickName] ,[AreaOfLawName] ,[AreaOfLawQBID] ,[BillSettingsID] FROM [" + secondaryDB + "].[dbo].[AreaLaw] where AreaOfLawStatus = 0 and [" + secondaryDB + "].[dbo].[AreaLaw].AreaOfLawNickName not in (select AreaOfLawNickName from [" + primaryDB + "].[dbo].[AreaLaw])";
            error = error + "\r\n" + MergeFunctions.RunSQLCommand(sQuery, connString);
            sQuery = @"update [arealaw] set AreaOfLawTransNewID = AreaOfLawID";
            error = error + "\r\n" + MergeFunctions.RunSQLCommand(sQuery, connString);
            // MessageBox.Show("Lawyers done: " + error);
            getNewTolIDs(newConnectionString);
            return error;
        }

        public string MergeRates(string lawyers, bool respAtty, bool tol, string connString, string secondaryDB, string primaryDB)
        {
            string error = "";
            string sQuery = @"SELECT * from [LawRate] where LawRateStatus = 0 and lawyerid not in (select lawyerid from lawinf where LawInfStatus = 0)";
            //get all items from the secondary books
            SqlConnection con = new SqlConnection(oldConnectionString);
            using (var command = new SqlCommand(sQuery, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            //insert them into the primary books while looking up the appropriate new ids
                            using (SqlConnection connection = new SqlConnection(connString))
                            {
                                connection.Open();
                                using (SqlCommand command1 = new SqlCommand())
                                {
                                    int newID = 0;
                                    foreach (Lawyer l in lawList)
                                        if (Convert.ToInt32(reader["LawyerID"].ToString().Trim()) == l.oldID)
                                        {
                                            newID = l.ID;

                                        }
                                    command1.Connection = connection;
                                    command1.CommandType = CommandType.Text;
                                    command1.CommandText = @"INSERT into [LawRate] ([LawRateStatus],[LawyerID] ,[RateID],[LawRateAmount],[LawRateInternalAmount]) VALUES (@LawRateStatus, @LawyerID, @RateID, @LawRateAmount, @LawRateInternalAmount)";
                                    command1.Parameters.AddWithValue("@LawRateStatus", reader["LawRateStatus"].ToString().Trim());
                                    command1.Parameters.AddWithValue("@LawyerID", newID);
                                    command1.Parameters.AddWithValue("@RateID", reader["RateID"].ToString().Trim());
                                    command1.Parameters.Add("@LawRateAmount", SqlDbType.Decimal).Value = Double.Parse(reader["LawRateAmount"].ToString().Trim());
                                    command1.Parameters.Add("@LawRateInternalAmount", SqlDbType.Decimal).Value = Double.Parse(reader["LawRateInternalAmount"].ToString().Trim());

                                    try
                                    {
                                        command1.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex1)
                                    {
                                        MessageBox.Show(ex1.Message);
                                    }


                                }

                                connection.Close();
                            }


                        }
                        catch (Exception inner1)
                        { MessageBox.Show("Inner: " + inner1.Message); }
                    }
                }
            }
            return error;
        }

    }
}

using Application.Interfaces;
using AutoMapper;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DbRepo
{
    public class CMSProfile : ICMSProfile
    {

        private string _sqlConnStr;
        private IMapper _mapper;
        public CMSProfile(IConfiguration config, IMapper mapper)
        {
            _sqlConnStr = config.GetConnectionString("Default");
            _mapper = mapper;
        }

        public  CMSProfileModel Load(int _profileID)
        {

            CMSProfileModel _CMSProfileModel = new CMSProfileModel();
            CMSProfileModelSimple _CMSProfileModelSimple = new CMSProfileModelSimple();

            var procedure = "Profile_GetOne";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: _profileID);

            
            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<CMSProfileModelSimple>(procedure, _params, commandType: CommandType.StoredProcedure);

                _CMSProfileModelSimple = result.FirstOrDefault();
                _mapper.Map(_CMSProfileModelSimple, _CMSProfileModel);

                return _CMSProfileModel;
   
            }
           
        }

        public async Task<List<CMSProfileModel>> GetList(int? AppID, int? StatusID)
        {
            var procedure = "Profile_GetList";
            var _params = new DynamicParameters();

            if (AppID == null)
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: System.DBNull.Value);
            }
            else
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: AppID);
            }

            if (StatusID == null)
            {
                _params.Add(name: "@profileStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: System.DBNull.Value);
            }
            else
            {
                _params.Add(name: "@profileStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: StatusID);
            }

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<CMSProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CMSProfileModel> _CMSProfile = result.ToList<CMSProfileModel>();

                return _CMSProfile;
            }


        }

        public async Task<List<CMSProfileModel>> GetDeleteList(int? AppID)
        {
            var procedure = "Profile_GetDeleteList";
            var _params = new DynamicParameters();

            if (AppID == null)
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: System.DBNull.Value);
            }
            else
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: AppID);
            }


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<CMSProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CMSProfileModel> _CMSProfile = result.ToList<CMSProfileModel>();

                return _CMSProfile;
            }


        }

        public async Task<CMSProfileModel> CreateNew(int ModifiedBy, int appID, string profileName, string profilDescr, string EmailLanguage)
        {
            var procedure = "Profile_Create";
            var _params = new DynamicParameters();

            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifiedBy);
            _params.Add(name: "@ProfileName", dbType: DbType.String, direction: ParameterDirection.Input, value: profileName);
            _params.Add(name: "@ProfilDescr", dbType: DbType.String, direction: ParameterDirection.Input, value: profilDescr);
            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@NotificationLang", dbType: DbType.String, direction: ParameterDirection.Input, value: EmailLanguage);
            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                int ReturnValue = _params.Get<int>("@ReturnValue");

                if (ReturnValue == 0)
                {
                    return Load(_params.Get<Int32>("@ProfilID"));
                }

                return null;

            }

        }

        public async Task<CMSProfileModel> ProfilCopy(int SourceProfilID, int ModifiedBy)
        {

            var procedure = "Profile_Copy";
            var _params = new DynamicParameters();

            _params.Add(name: "@SourceProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: SourceProfilID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifiedBy);
            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                int ReturnValue = _params.Get<int>("@ReturnValue");

                if (ReturnValue == 0)
                {
                    return Load(_params.Get<Int32>("@ProfilID"));
                }

                return null;

            }


        }


        #region Save
        public async Task<bool> UpdateParticipantsAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_Participants";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@NotifyAllProfileParticipants", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: cmsProfileModel.NotifyAllProfileParticipants);
            _params.Add(name: "@profileParticipants", dbType: DbType.Xml, direction: ParameterDirection.Input, value: cmsProfileModel.profileParticipants);
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                
                return true;

            }


        }
        public async Task<bool> UpdateEscalationAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_Escalation";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@EscalationUsers", dbType: DbType.Xml, direction: ParameterDirection.Input, value: cmsProfileModel.escalationUsers);
            _params.Add(name: "@EscalationRules", dbType: DbType.Xml, direction: ParameterDirection.Input, value: cmsProfileModel.escalationRules);
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);

                return true;

            }

            
        }
        public async Task<bool> UpdateGeneralInfoAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_General";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@ProfileName", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.profileName);
            _params.Add(name: "@ProfilDescr", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.profilDescr);
            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.appID);
            _params.Add(name: "@NotificationLang", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.NotificationLang);
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

           
            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);                    
                return true;

            }
           

            
        }
        public async Task<bool> UpdateStatusAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_Status";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@profileStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileStatusID);
            _params.Add(name: "@ReasonToDelete", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.ReasonToDelete);
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);

                return true;

            }


        }
        #endregion

    }
}

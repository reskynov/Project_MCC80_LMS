﻿using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Accounts;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "accounts/") : base(request)
        {
        }
        public async Task<ResponseHandler<TokenVM>> Login(LoginVM login)
        {
            ResponseHandler<TokenVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<TokenVM>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseHandler<RegisterVM>> Register(RegisterVM entity)
        {
            ResponseHandler<RegisterVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "register", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<RegisterVM>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseHandler<ForgotPasswordVM>> ForgotPassword(ForgotPasswordVM entity)
        {
            ResponseHandler<ForgotPasswordVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "forgot-password", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<ForgotPasswordVM>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseHandler<ChangePasswordVM>> ChangePassword(ChangePasswordVM entity)
        {
            ResponseHandler<ChangePasswordVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "change-password", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<ChangePasswordVM>>(apiResponse);
            }
            return entityVM;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PetHelp.Dtos.Base;

namespace PetHelp.Services.verificator
{
    public interface IODataVerificator<T> where T : BaseDto
    {
        Task<bool> VerifyExistency(int id);
        Task<bool> VerifyInexistency(int id);
        Task<bool> VerifyDependency(int id);
        Task<bool> VerifyDependents(int Id);
    }
}
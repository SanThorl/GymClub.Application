namespace GymClub.App.Services
{
    public interface IInjectService
    {
        Task EnableLoading();
        Task DisableLoading();
    }
}

using Models.SystemSettings;

namespace Application.SystemSettings
{
    public interface ISystemSettingService
    {
        Task<int> Create(SystemSettingCreateRequest req);

        Task<int> Update(SystemSettingUpdateRequest req);

        Task<SystemSettingVm> GetSettings();
    }
}
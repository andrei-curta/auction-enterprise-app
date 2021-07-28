namespace DataMapper.DAO
{
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    public class ApplicationSettingDataService : BaseRepository<ApplicationSetting>, IApplicationSettingDataService
    {
    }
}
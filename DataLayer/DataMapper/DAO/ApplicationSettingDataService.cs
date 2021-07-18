namespace DataMapper.DAO
{
    using DataMapper.Interfaces;
    using DataMapper.Repository;
    using DomainModel.Models;

    class ApplicationSettingDataService : BaseRepository<ApplicationSetting>, IApplicationSettingDataService
    {
    }
}
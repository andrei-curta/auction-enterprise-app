namespace DataMapper.Interfaces
{
    using DataMapper.Repository;
    using DomainModel.Models;

    interface IApplicationSettingDataService : IRepository<ApplicationSetting>
    {
    }
}

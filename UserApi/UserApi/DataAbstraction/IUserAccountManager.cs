namespace UserApi.DataAbstraction
{
    public interface IUserAccountManager
    {
        void Add(UserAccount userAccount);
        UserAccount GetById(int id);
        UserAccount GetByUserName(string userName);

        void Update(UserAccount userAccount);
    }
}

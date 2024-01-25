namespace PublishCore.Auth.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o matricula de nuestra interfaces a nivel de repository

        IUsuarioRepository Usuario { get; }
        IEmpresaRerpository Empresa {  get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
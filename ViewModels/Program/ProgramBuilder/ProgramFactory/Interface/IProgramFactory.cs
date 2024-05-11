using CinemaVillage.ViewModels.User.UserBuilder;

namespace CinemaVillage.ViewModels.Program.ProgramBuilder.ProgramFactory.Interface
{
    public interface IProgramFactory
    {
        ProgramBuilder CreateBuilder();
    }
}

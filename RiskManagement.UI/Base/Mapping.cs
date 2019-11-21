using AutoMapper;
using RiskManagement.Model;
using RiskManagement.UI.ViewModels;

namespace RiskManagement.UI.Base
{
    public class Mapping
    {
        public IMapper Create()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataViewModel, DataModel>();
                cfg.CreateMap<DataModel, DataViewModel>()
                    .ForMember(dataViewModel => dataViewModel.CancelSolution, 
                               options => options.Ignore());

                cfg.CreateMap<ProjectViewModel, Project>();
                cfg.CreateMap<Project, ProjectViewModel>();

                cfg.CreateMap<IterationViewModel, Iteration>();
                cfg.CreateMap<Iteration, IterationViewModel>();

                cfg.CreateMap<RiskViewModel, Risk>();
                cfg.CreateMap<Risk, RiskViewModel>();

                cfg.CreateMap<ExpertViewModel, Expert>();
                cfg.CreateMap<Expert, ExpertViewModel>();

                cfg.CreateMap<SolutionViewModel, Solution>();
                cfg.CreateMap<Solution, SolutionViewModel>();

                cfg.CreateMap<ProbabilityTableRowViewModel, ProbabilityTableRow>();
                cfg.CreateMap<ProbabilityTableRow, ProbabilityTableRowViewModel>();

                cfg.CreateMap<RecentFilesViewModel, RecentFilesModel>();
                cfg.CreateMap<RecentFilesModel, RecentFilesViewModel>();

                cfg.CreateMap<LossTableRowViewModel, LossTableRow>();
                cfg.CreateMap<LossTableRow, LossTableRowViewModel>();

                cfg.CreateMap<RiskEventRowViewModel, RiskEventRow>();
                cfg.CreateMap<RiskEventRow, RiskEventRowViewModel>();

                cfg.CreateMap<ProjectInfoRowViewModel, ProjectInfoRow>();
                cfg.CreateMap<ProjectInfoRow, ProjectInfoRowViewModel>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}

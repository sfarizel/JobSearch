using System;
using System.ComponentModel.DataAnnotations;

namespace JobSearch.APP.Domain.Models
{

    //Linha adicionada para teste do GIT com autenticação token
    //Continuando nos testes
    //Mais um teste de commit pra ver
    //Outro teste vamos ver se vai o git com token



    /// <summary>
    /// teste teste teste
    /// </summary>



    //testamdo 12 3 


    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Company", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public string Company { get; set; }

        [Display(Name = "JobTitle", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public string JobTitle { get; set; }

        [Display(Name = "CityState", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10,ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E003")]
        public string CityState { get; set; }

        [Display(Name = "InitialSalary", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public double InitialSalary { get; set; }

        [Display(Name = "FinalSalary", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public double FinalSalary { get; set; }

        [Display(Name = "ContractType", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public string ContractType { get; set; }

        [Display(Name = "TecnologyTools", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public string TecnologyTools { get; set; }

        [Display(Name = "CompanyDescription", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        public string CompanyDescription { get; set; }

        [Display(Name = "JobDescription", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        public string JobDescription { get; set; }

        [Display(Name = "Benefits", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        public string Benefits { get; set; }

        [Display(Name = "InterestedSendEmailTo", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [Required(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(JobSearch.Domain.Utility.Language.Messages), ErrorMessageResourceName = "MSG_E002")]
        public string InterestedSendEmailTo { get; set; }

        [Display(Name = "PublicationDate", ResourceType = typeof(JobSearch.Domain.Utility.Language.Fields))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PublicationDate { get; set; }

        public int UserId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("UserId")]
        public User User { get; set; }

    }
}

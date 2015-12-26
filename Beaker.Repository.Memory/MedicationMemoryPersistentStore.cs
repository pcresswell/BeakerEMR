using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core.Medication;
using Beaker.Core;

namespace Beaker.Repository.Memory
{
    public class MedicationMemoryPersistentStore : MemoryPersistentStore<Medication>
    {
        protected override void Copy(Medication source, Medication destination)
        {
            destination.DrugCode = source.Product.DrugCode;
            AssignSourceCompany(source, destination);
            AssignSourceForm(source, destination);
            AssignSourceIngredient(source, destination);
            AssignSourcePackage(source, destination);
            AssignSourcePharmaceutical(source, destination);
            AssignSourceProduct(source, destination);
            AssignSourceRoute(source, destination);
            AssignSourceSchedule(source, destination);
            AssignSourceStatus(source, destination);
            AssignSourceTherapeutic(source, destination);
            
        }

        private void AssignSourceTherapeutic(Medication source, Medication destination)
        {
            foreach (var sourceTherapeutic in source.Therapeutics)
            {
                Therapeutic destinationTherapeutic = new Therapeutic(sourceTherapeutic.DrugCode, sourceTherapeutic.TCATCNumber, sourceTherapeutic.TCATC, sourceTherapeutic.TCAHFSNumber, sourceTherapeutic.TCAHFS);
                AssignBaseEntityProperties(sourceTherapeutic, destinationTherapeutic);
                destination.AddTherapeutic(destinationTherapeutic);
            }
        }

        private void AssignSourceStatus(Medication source, Medication destination)
        {
            foreach (var sourceStatus in source.Statuses)
            {
                Status destinationStatus = new Status(sourceStatus.DrugCode, sourceStatus.CurrentStatusFlag, sourceStatus.StatusCode, sourceStatus.HistoryDate);
                AssignBaseEntityProperties(sourceStatus, destinationStatus);
                destination.AddStatus(destinationStatus);
            }
        }

        private void AssignSourceSchedule(Medication source, Medication destination)
        {
            foreach (var sourceSchedule in source.Schedules)
            {
                Schedule destinationSchedule = new Schedule(sourceSchedule.DrugCode, sourceSchedule.ScheduleCode);
                AssignBaseEntityProperties(sourceSchedule, destinationSchedule);
                destination.AddSchedule(destinationSchedule);
            }
        }

        private void AssignSourceRoute(Medication source, Medication destination)
        {
            foreach (var sourceRoute in source.Routes)
            {
                Route destinationRoute = new Route(sourceRoute.DrugCode, sourceRoute.RouteOfAdministrationCode, sourceRoute.RouteOfAdministration);
                AssignBaseEntityProperties(sourceRoute, destinationRoute);
                destination.AddRoute(destinationRoute);
            }
        }

        private void AssignSourceProduct(Medication source, Medication destination)
        {
            DrugProduct sourceProduct = source.Product;
            if (sourceProduct == null) return;

            DrugProduct destinationProduct = new DrugProduct(sourceProduct.DrugCode, sourceProduct.ProductCategorization, sourceProduct.ProductClass, sourceProduct.DrugIdentificationNumber, sourceProduct.BrandName, sourceProduct.Descriptor, sourceProduct.PediatricFlag, sourceProduct.AccessionNumber, sourceProduct.NumberOfAIS, sourceProduct.LastUpdateDate, sourceProduct.AIGroupNumber);
            AssignBaseEntityProperties(sourceProduct, destinationProduct);
            destination.Product = destinationProduct;
        }

        private void AssignSourcePharmaceutical(Medication source, Medication destination)
        {
            foreach (var sourcePharmaceutical in source.Pharmaceuticals)
            {
                Pharmaceutical destinationPharmaceutical = new Pharmaceutical(
                    sourcePharmaceutical.DrugCode,
                    sourcePharmaceutical.PharmaceuticalSTD);
                AssignBaseEntityProperties(sourcePharmaceutical, destinationPharmaceutical);
                destination.AddPharmaceutical(destinationPharmaceutical);
            }
        }

        private void AssignSourcePackage(Medication source, Medication destination)
        {
            foreach (var sourcePackage in source.Packages)
            {
                Package destinationPackage = new Package(
                    sourcePackage.DrugCode,
                    sourcePackage.UPC,
                    sourcePackage.PackageSize,
                    sourcePackage.PackageType,
                    sourcePackage.PackageSize,
                    sourcePackage.ProductInformation);
                AssignBaseEntityProperties(sourcePackage, destinationPackage);
                destination.AddPackage(destinationPackage);
            }
        }

        private void AssignSourceIngredient(Medication source, Medication destination)
        {
            foreach (var sourceIngredient in source.Ingredients)
            {
                if (sourceIngredient == null) return;

                Ingredient destinationIngredient = new Ingredient(
                    sourceIngredient.DrugCode,
                    sourceIngredient.ActiveIngredientCode,
                    sourceIngredient.IngredientCode,
                    sourceIngredient.IngredientSuppliedInd,
                    sourceIngredient.Strength,
                    sourceIngredient.StrengthUnit,
                    sourceIngredient.StrengthType,
                    sourceIngredient.DosageValue,
                    sourceIngredient.Base,
                    sourceIngredient.DosageUnit,
                    sourceIngredient.Notes);

                AssignBaseEntityProperties(sourceIngredient, destinationIngredient);
                destination.AddIngredient(destinationIngredient);
            }
        }

        private void AssignSourceForm(Medication source, Medication destination)
        {
            foreach (var sourceForm in source.Forms)
            {
                Form destinationForm = new Form(sourceForm.DrugCode, sourceForm.PharmaceuticalFormCode, sourceForm.PharmaceuticalForm);
                AssignBaseEntityProperties(sourceForm, destinationForm);
                destination.AddForm(destinationForm);
            }
        }

        private void AssignSourceCompany(Medication source, Medication destination)
        {
            Company sourceCompany = source.Company;
            if (sourceCompany == null) return; 

            Company destinationCompany = new Company(
                sourceCompany.DrugCode,
                sourceCompany.MFRCode,
                sourceCompany.CompanyCode,
                sourceCompany.CompanyName,
                sourceCompany.CompanyType,
                sourceCompany.AddressMailingFlag,
                sourceCompany.AddressBillingFlag,
                sourceCompany.AddressNotificationFlag,
                sourceCompany.AddressOther,
                sourceCompany.SuiteNumber,
                sourceCompany.Street,
                sourceCompany.City,
                sourceCompany.Province,
                sourceCompany.Country,
                sourceCompany.PostalCode,
                sourceCompany.POBox);
            AssignBaseEntityProperties(sourceCompany, destinationCompany);

            destination.Company = destinationCompany;
        }

        private void AssignBaseEntityProperties(DomainObject sourceEntity, DomainObject destinationEntity)
        {
            destinationEntity.ID = sourceEntity.ID;
            destinationEntity.AuthorID = sourceEntity.AuthorID;
            destinationEntity.DomainObjectID = sourceEntity.DomainObjectID;
            destinationEntity.RecordEndDateTime = sourceEntity.RecordEndDateTime;
            destinationEntity.RecordStartDateTime = sourceEntity.RecordStartDateTime;
            destinationEntity.ValidEndDateTime = sourceEntity.ValidEndDateTime;
            destinationEntity.ValidStartDateTime = sourceEntity.ValidStartDateTime;
        }
    }
}

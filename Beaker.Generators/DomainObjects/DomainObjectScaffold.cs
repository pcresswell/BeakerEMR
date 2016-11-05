using System;
using System.Collections.Generic;
using Humanizer;
using Beaker.Generators.DomainObjects.Methods;
using Beaker.Generators.Methods;
using System.Linq;

namespace Beaker.Generators.DomainObjects
{
	/// <summary>
	/// Generates all scafollding required for a domain model.
	/// </summary>
	public class DomainObjectScaffold : Generator
	{
		public DomainObjectScaffold()
		{
			this.Properties = new List<DomainObjectProperty>();
		}

		public string Namespace { get; set; }
		public string Name { get; private set; }

		// Generated Classes
		public Class DomainObjectClass { get; private set; }
		public Class SQLiteTable { get; private set; }
		public Class CreateEditor { get; private set; }
		public Class ReadEditor { get; private set; }
		public Class UpdateEditor { get; private set; }
		public Class StorageEngine { get; set; }

		private IList<DomainObjectProperty> Properties { get; set; }

		public IList<Class> Classes
		{
			get
			{
				return new List<Class>() {
					DomainObjectClass,
					SQLiteTable,
					CreateEditor,
					ReadEditor,
					UpdateEditor,
					StorageEngine };
			}
		}

		public DomainObjectScaffold End()
		{
			return this;
		}

		public DomainObjectProperty AddProperty(string t = default(string), string name = default(string))
		{
			var prop = new DomainObjectProperty(this);
			prop.Type(t);
			prop.Name(name);
			this.Properties.Add(prop);
			return prop;
		}

		public DomainObjectScaffold ClassName(string name)
		{
			this.Name = name;
			return this;
		}

		public override void Generate()
		{
			this.CreateDomainObjectClass();
			this.CreateSQLiteTableClass();
			this.CreateCreateEditorClass();
			this.CreateReadEditorClass();
			this.CreateUpdateEditorClass();
			this.CreateStorageEngineClass();

			this.DomainObjectClass.Generate();
			this.SQLiteTable.Generate();
			this.CreateEditor.Generate();
			this.ReadEditor.Generate();
			this.UpdateEditor.Generate();
			this.StorageEngine.Generate();
		}

		private void CreateCreateEditorClass()
		{
			this.CreateEditor = new Class()
				.Namespace(this.Namespace)
				.ClassName("Create" + this.Name + "Editor")
				.BaseClassName("Editor")
				.AddUsing("Authorize")
				.AddUsing("Beaker.Core");


			foreach (var prop in this.Properties)
			{
				this.CreateEditor
					.AddProperty<Property>()
					.SetVisibility(Visibility.Public)
						.SetType(prop.PropertyType)
						.SetName(prop.PropertyName)
							.Accessor()
								.SetAccessorType(AccessorType.Get)
								.SetVisibility(Visibility.Private)
							.End
							.Accessor()
								.SetAccessorType(AccessorType.Set)
				   				.SetVisibility(Visibility.None)
							.End
				.End();
			}

			CreateEditor.AddConstructor(Visibility.Public, CreateEditor.GetNameAsPascal());

			var method = CreateEditor
				.AddMethod<CreateMethodGenerator>()
					.SetVisibility(Visibility.Public)
					.SetStaticMethod(false)
					.NoMethodArguments()
					.SetReturnType(this.Name);

			foreach (var prop in this.Properties)
			{
				method.AddArgument().Name(prop.PropertyName).Type(prop.PropertyType);
			}
		}

		private void CreateReadEditorClass()
		{
			this.ReadEditor = new Class()
				.Namespace(this.Namespace)
				.ClassName("Read" + this.Name + "Editor")
				.BaseClassName("Editor")
				.AddUsing("Authorize")
				.AddUsing("Beaker.Core");

			this.ReadEditor.AddConstructor(Visibility.Public, this.ReadEditor.GetNameAsPascal());

			foreach (var prop in this.Properties)
			{
				this.ReadEditor
					.AddProperty<Property>()
					.SetVisibility(Visibility.Public)
						.SetType(prop.PropertyType)
						.SetName(prop.PropertyName)
							.Accessor()
								.SetAccessorType(AccessorType.Get)
								.SetBodyContents("return this.GetSubjectAs<" + this.Name + ">()." + prop.PropertyName + ";")
							.End
					.End();
			}
		}

		private void CreateUpdateEditorClass()
		{
			this.UpdateEditor = new Class()
				.Namespace(this.Namespace)
				.ClassName("Update" + this.Name + "Editor")
				.BaseClassName("Editor")
				.AddUsing("Authorize")
				.AddUsing("Beaker.Core");


			foreach (var prop in this.Properties)
			{
				this.UpdateEditor
					.AddProperty<Property>()
					.SetVisibility(Visibility.Public)
						.SetType(prop.PropertyType)
						.SetName(prop.PropertyName)
							.Accessor()
								.SetAccessorType(AccessorType.Get)
								.SetVisibility(Visibility.Private)
							.End
							.Accessor()
								.SetAccessorType(AccessorType.Set)
				   				.SetVisibility(Visibility.None)
							.End
				.End();
			}

			UpdateEditor.AddConstructor(Visibility.Public, UpdateEditor.GetNameAsPascal());

			var method = UpdateEditor
				.AddMethod<GenericMethod>()
				.SetVisibility(Visibility.Public)
				.NoMethodArguments()
				.SetName("Update")
				.SetReturnType(this.DomainObjectClass.GetNameAsPascal());

			foreach (var prop in this.Properties)
			{
				method.AddArgument().Name(prop.PropertyName).Type(prop.PropertyType);
			}

			// ex: return this.GetSubjectAs<TestPatient>().Update(FirstName, LastName)
			string body = "return this.GetSubjectAs<" + this.Name + ">().Update(" + string.Join(", ", this.Properties) + ");";
			method.AddBodyLine(body);
		}

		private void CreateSQLiteTableClass()
		{
			this.SQLiteTable = new Class()
				.Namespace(this.Namespace)
				.ClassName(this.Name + "Table")
				.BaseClassName("Table")
				.AddAnnotation("Table(" + "\"" + this.Name.Pluralize().Underscore() + "\"" + ")")
				.AddUsing("SQLite")
				.AddUsing("Beaker.Core")
				.AddUsing("Beaker.Data")
				.AddUsing("Beaker.Data.SQLite");

			foreach (var prop in this.Properties)
			{
				this.SQLiteTable
					.AddProperty<Property>()
					.AddAnnotation("Column(" + "\"" + prop.PropertyName.Underscore() + "\"" + ")")
					.SetVisibility(Visibility.Public)
						.SetType(prop.PropertyType)
						.SetName(prop.PropertyName)
							.Accessor()
								.SetAccessorType(AccessorType.Get)
								.SetVisibility(Visibility.None)
							.End
							.Accessor()
								.SetAccessorType(AccessorType.Set)
				   				.SetVisibility(Visibility.None)
							.End
				.End();


			}
		}

		private void CreateDomainObjectClass()
		{
			this.DomainObjectClass = new Class()
					.Namespace(this.Namespace)
					.ClassName(this.Name)
				.BaseClassName("DomainObject")
				.AddUsing("Beaker.Core");


			// Add properties
			foreach (var prop in this.Properties)
			{
				DomainObjectClass
					.AddProperty<Property>()
						.SetVisibility(Visibility.Internal)
						.SetType(prop.PropertyType)
						.SetName(prop.PropertyName)
							.Accessor()
								.SetAccessorType(AccessorType.Get)
								.SetVisibility(Visibility.None)
							.End
							.Accessor()
								.SetAccessorType(AccessorType.Set)
								.SetVisibility(Visibility.Private)
							.End
				.End();
			}

			Constructor c = DomainObjectClass
				.AddConstructor(Visibility.Private, this.Name)
					.AddArgument("ID", "Guid", true)
					.AddArgument("DomainObjectID", "Guid", true);

			foreach (var prop in this.Properties)
			{
				c.AddArgument(prop.PropertyName, prop.PropertyType);
			}

			DomainObjectClass.AddMethod<LoadMethodGenerator>();
			var method = DomainObjectClass.AddMethod<CreateMethodGenerator>().SetReturnType(this.Name).SetVisibility(Visibility.Internal);

			foreach (var prop in this.Properties)
			{
				method.AddArgument().Name(prop.PropertyName).Type(prop.PropertyType);
			}

			DomainObjectClass.AddMethod<UpdateMethodGenerator>();
		}

		private void CreateStorageEngineClass()
		{
			this.StorageEngine = new Class()
					.Namespace(this.Namespace)
					.ClassName(this.Name + "StorageEngine")
					.BaseClassName("SQLiteStorageEngine<" + this.Name + ", " + this.Name + "Table>")
					.AddUsing("Beaker.Core")
					.AddUsing("Beaker.Data.SQLite");

			//Constructor ex: public PatientStorageEngine(BeakerSQLiteConnection connection) : base(connection)

			this.StorageEngine
				.AddConstructor(Visibility.Public, this.Name + "StorageEngine")
				.AddArgument("connection", "BeakerSQLiteConnection", true);

			// Example MapToDomainObject method
			// protected override Patient MapToDomainObject(PatientTable table)
			// {
			// 	   return Patient.Load(table.ID, table.DomainObjectID, table.FirstName, table.LastName);
			// }

			var method = this.StorageEngine
							 .AddMethod<GenericMethod>();

			method
				.AddBodyLine("return " + this.Name + ".Load(table.ID, table.DomainObjectID, " + string.Join(", ", this.Properties.Select(x => "table." + x.PropertyName).ToList<string>()) + ");")
				.SetOverride(true)
				.SetReturnType(this.Name)
				.SetName("MapToDomainObject")
				.SetVisibility(Visibility.Protected)
			 		.AddArgument()
						.Type(this.Name + "Table")
						.Name("table");

			/*  Exmample MapToTable method:
			 * 
			 *  protected override PatientTable MapToTable(Patient domainObject)
				{
					return new PatientTable()
					{
						ID = domainObject.ID,
						DomainObjectID = domainObject.DomainObjectID,
						FirstName = domainObject.FirstName,
						LastName = domainObject.LastName
					};
				}
			*/

			method = this.StorageEngine.AddMethod<GenericMethod>();
			method
				.SetOverride(true)
				.SetVisibility(Visibility.Protected)
				.SetReturnType(this.Name + "Table")
				.SetName("MapToTable")
				.AddArgument()
					.Type(this.Name)
					.Name("domainObject");

			// now add the lines
			method
				.AddBodyLine("return new " + this.Name + "Table()")
				.AddBodyLine("{")
				.AddBodyLine("\tID = domainObject.ID,")
				.AddBodyLine("\tDomainObjectID = domainObject.DomainObjectID,");

			foreach (var prop in this.Properties)
			{
				method.AddBodyLine("\t" + prop.PropertyName + " = domainObject." + prop.PropertyName + ",");
			}
			method.AddBodyLine("};");
		}
	}
}

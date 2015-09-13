using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Receptsamlingen.Repository;
using Receptsamlingen.Web.Classes;
using Globals = Receptsamlingen.Web.Classes.Globals;

namespace Receptsamlingen.Web.Pages
{
	public partial class Recipe : Page
	{

		#region Members

		protected string DeleteLinkButtonEvent = string.Empty;

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{

			#region Eventlisteners

			addButton.Click += OnAddButtonClick;
			clearButton.Click += OnClearButtonClick;
			cancelButton.Click += OnCancelButtonClick;
			editLinkButton.Click += OnEditLinkButtonClick;
			deleteLinkButton.Click += OnDeleteLinkButtonClick;
			categoryDdl.SelectedIndexChanged += OnCategoryDdlSelectedIndexChanged;
			rateButton.Click += OnRateButtonClick;

			#endregion

			DeleteLinkButtonEvent = Page.ClientScript.GetPostBackEventReference(deleteLinkButton, string.Empty);

			if (!IsPostBack)
			{
				SetupControls();

				var id = Page.RouteData.Values["id"] as string;

				if (!String.IsNullOrEmpty(id)) // If an id exists then we´ll show the recipe in readmode
				{
					LoadRecipe(Convert.ToInt32(id));
					SetView(readView);

					if (SessionHandler.User != null)
					{
						SetRatingInfo();
						if (SessionHandler.User.Role.Equals(20))
						{
							SetupAdminControls();
						}
					}
				}
				else // Else the user wants to add recipe and then we have to check if logged in
				{
					Common.ClearRecipeSessions();
					headerLabel.Text = Globals.HeaderString;
					SetView(SessionHandler.User != null ? addEditView : anonymousView);
				}
			}
		}

		#region Private methods

		private void SetupAdminControls()
		{
			adminButtonsPlaceHolder.Visible = true;
		}

		private void SetupControls()
		{
			var categories = RecipeRepository.Instance.GetAllCategories();
			var dishTypes = RecipeRepository.Instance.GetAllDishTypes();
			var specials = RecipeRepository.Instance.GetAllSpecials();

			categoryDdl.DataSource = categories;
			categoryDdl.DataTextField = Globals.NameProperty;
			categoryDdl.DataValueField = Globals.IdProperty;

			dishTypeDdl.DataSource = dishTypes;
			dishTypeDdl.DataTextField = Globals.NameProperty;
			dishTypeDdl.DataValueField = Globals.IdProperty;
			dishTypeDdl.Enabled = false;

			specialCheckBoxList.DataSource = specials;
			specialCheckBoxList.DataTextField = Globals.NameProperty;
			specialCheckBoxList.DataValueField = Globals.IdProperty;

			DataBindControls();
			AddEmptyItemToControls();
		}

		private void SetView(View view)
		{
			recipeMultiView.SetActiveView(view);
		}

		private void LoadRecipe(int id, bool isEditMode = false)
		{
			var recipe = RecipeRepository.Instance.GetById(id);
			var avarageRating = RatingRepository.Instance.GetAvarage(recipe.Guid);
			var recipeSpecialIdList = RecipeRepository.Instance.GetSpecialsForRecipe(recipe.Guid);
			var allSpecialsList = RecipeRepository.Instance.GetAllSpecials();
			var category = RecipeRepository.Instance.GetCategoryById(recipe.CategoryId);
			var dishType = String.Empty;
			if (recipe.DishTypeId.HasValue)
			{
				dishType = RecipeRepository.Instance.GetDishTypeById(recipe.DishTypeId.Value);
			}
			
			SessionHandler.CurrentId = id;
			SessionHandler.CurrentGuid = recipe.Guid;

			// Set header
			if (isEditMode)
			{
				headerLabel.Text = Globals.HeaderEditString;
				nameTextbox.Text = recipe.Name;
			}
			else
			{
				headerLabel.Text = recipe.Name;
			}

			// Set dishtype
			if (!String.IsNullOrEmpty(dishType) && !isEditMode)
			{
				dishTypeLabel.Text = dishType;
			}
			else if (!String.IsNullOrEmpty(dishType) && isEditMode)
			{
				dishTypeDdl.SelectedValue = Convert.ToString(recipe.DishTypeId);
				dishTypeDdl.Enabled = true;
			}
			else
			{
				dishTypeLabel.Text = "-";
			}

			// Set category
			if (!String.IsNullOrEmpty(category) && !isEditMode)
			{
				categoryLabel.Text = category;
			}
			else if (!String.IsNullOrEmpty(category) && isEditMode)
			{
				categoryDdl.SelectedValue = Convert.ToString(recipe.CategoryId);
			}

			// Set portions
			if (recipe.Portions != 0 && !isEditMode)
			{
				portionsLabel.Text = recipe.Portions.ToString();
			}
			else if (recipe.Portions != 0 && isEditMode)
			{
				portionsDdl.Text = Convert.ToString(recipe.Portions);
			}
			else
			{
				portionsLabel.Text = "-";
			}

			// Set special
			if (recipeSpecialIdList != null && recipeSpecialIdList.Count > 0)
			{
				if (!isEditMode)
				{
					var list = (from item in recipeSpecialIdList 
								from special in allSpecialsList 
								where item.SpecialId == special.Id 
								select special).Aggregate(String.Empty, (current, special) => current + (special.Name + ", "));

					if (list.EndsWith(", "))
					{
						list = list.TrimEnd(',',' ');
					}

					specialLabel.Text = list;
				}
				else
				{
					foreach (ListItem checkBoxItem in specialCheckBoxList.Items)
					{
						foreach (var item in recipeSpecialIdList)
						{
							if (checkBoxItem.Value.Equals(item.SpecialId.ToString()))
							{
								checkBoxItem.Selected = true;
							}
						}
					}
				}
			}
			else
			{
				specialIntroLabel.Visible = false;
			}

			// Set ingredients and description
			if (!isEditMode)
			{
				readIngredientsTextBox.ReadOnly = true;
				readDescriptionTextBox.ReadOnly = true;
				readIngredientsTextBox.Text = recipe.Ingredients;
				readDescriptionTextBox.Text = recipe.Description;
			}
			else
			{
				addEditIngredientsTextBox.ReadOnly = false;
				addEditDescriptionTextBox.ReadOnly = false;
				addEditIngredientsTextBox.Text = recipe.Ingredients;
				addEditDescriptionTextBox.Text = recipe.Description;
			}

			// Set avaragerating
			avarageHidden.Value = Convert.ToString(avarageRating);
			ratingPanel.Visible = true;
		}

		private void SetRatingInfo()
		{
			loggedInHidden.Value = Convert.ToString(true).ToLower();
			rateButton.Enabled = true;
		}

		private void ClearInput()
		{
			nameTextbox.Text = String.Empty;
			categoryDdl.ClearSelection();
			dishTypeDdl.ClearSelection();
			specialCheckBoxList.ClearSelection();
			portionsDdl.ClearSelection();
			addEditDescriptionTextBox.Text = String.Empty;
			addEditIngredientsTextBox.Text = String.Empty;
		}

		private void DataBindControls()
		{
			categoryDdl.DataBind();
			dishTypeDdl.DataBind();
			specialCheckBoxList.DataBind();
		}

		private void AddEmptyItemToControls()
		{
			var listItem = new ListItem("--- Välj ---", "0");
			categoryDdl.Items.Insert(0, listItem);
			dishTypeDdl.Items.Insert(0, listItem);
			portionsDdl.Items.Insert(0, listItem);
		}

		private IList<int> GetChosenSpecials()
		{
			var specials = new List<int>();
			foreach (ListItem item in specialCheckBoxList.Items)
			{
				if (item.Selected)
				{
					specials.Add(Convert.ToInt32(item.Value));
				}
			}
			return specials;
		}

		private void ClearErrorMessages()
		{
			errorLabel.Visible = false;
			errorLabel.Text = String.Empty;
		}

		#endregion

		#region Events

		private void OnRateButtonClick(object sender, EventArgs e)
		{
			if (RatingRepository.Instance.UserHasVoted(SessionHandler.User.Username, SessionHandler.CurrentGuid))
			{
				infoRatingLabel.Text = Globals.ErrorUserHasVoted;
				infoRatingLabel.Visible = true;
			}
			else
			{
				var voted = RatingRepository.Instance.Save(SessionHandler.CurrentGuid, SessionHandler.User.Username, Convert.ToInt32(ratingHidden.Value));
				if (voted)
				{
					infoRatingLabel.Text = Globals.InfoVoteSaved;
					infoRatingLabel.Visible = true;
				}
				else
				{
					infoRatingLabel.Text = Globals.ErrorSavingVote;
					infoRatingLabel.Visible = true;
				}
			}
		}

		private void OnCategoryDdlSelectedIndexChanged(object sender, EventArgs e)
		{
			if (categoryDdl.SelectedIndex != 0)
			{
				if ((categoryDdl.SelectedItem.Text == Globals.AppetizerString) || (categoryDdl.SelectedItem.Text == Globals.MainCourseString))
				{
					dishTypeDdl.Enabled = true;
				}
				else
				{
					dishTypeDdl.Enabled = false;
				}
			}
		}

		private void OnClearButtonClick(object sender, EventArgs e)
		{
			ClearInput();
		}

		private void OnAddButtonClick(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(nameTextbox.Text) && categoryDdl.SelectedValue != "0")
			{
				var guid = String.Empty;
				var name = HttpUtility.HtmlEncode(nameTextbox.Text.Trim());
				var recipeName = Helper.CapitalizeFirstLetter(name);
				var recipe = new Repository.Recipe
									{
										Name = recipeName,
										Ingredients = HttpUtility.HtmlEncode(addEditIngredientsTextBox.Text.Trim()),
										Description = HttpUtility.HtmlEncode(addEditDescriptionTextBox.Text.Trim()),
										Portions = Convert.ToInt32(portionsDdl.SelectedValue),
										CategoryId = Convert.ToInt32(categoryDdl.SelectedItem.Value),
										DishTypeId = Convert.ToInt32(dishTypeDdl.SelectedItem.Value)
									};

				bool result;

				if (SessionHandler.CurrentId != 0 && !String.IsNullOrEmpty(SessionHandler.CurrentGuid)) // Recipe exists from before and should be updated
				{
					recipe.Id = SessionHandler.CurrentId;
					recipe.Guid = SessionHandler.CurrentGuid;
					result = RecipeRepository.Instance.Update(recipe);
				}
				else // New recipe that need to be saved
				{
					guid = Convert.ToString(Guid.NewGuid());
					recipe.Guid = guid;
					result = RecipeRepository.Instance.Save(recipe);
				}

				if (result)
				{
					var specials = GetChosenSpecials();

					if (SessionHandler.CurrentId != 0) // Recipe exits from before and its specials should be updated
					{
						// First delete all specialassigns that exists on this recipe
						RecipeRepository.Instance.DeleteSpecials(SessionHandler.CurrentGuid);

						// Then save new ones (if any)
						if (specials != null && specials.Count > 0)
						{
							foreach (var item in specials)
							{
								RecipeRepository.Instance.SaveSpecial(SessionHandler.CurrentGuid, item);
							}
						}

						savedUpdatedInfoLabel.Text = Globals.InfoRecipeUpdated;
					}
					else // New recipe so save its specials
					{
						if (specials != null && specials.Count > 0)
						{
							foreach (var item in specials)
							{
								RecipeRepository.Instance.SaveSpecial(guid, item);
							}
						}
						savedUpdatedInfoLabel.Text = Globals.InfoRecipeSaved;
					}

					recipeMultiView.SetActiveView(savedUpdatedView);
					ClearErrorMessages();
				}
				else
				{
					errorLabel.Visible = true;
					errorLabel.Text = Globals.ErrorSavingRecipe;
				}
			}
			else
			{
				errorLabel.Visible = true;
				errorLabel.Text = Globals.ErrorValidatingRecipe;
			}
		}

		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			Response.Redirect(Globals.DefaultUrl);
		}

		private void OnDeleteLinkButtonClick(object sender, EventArgs e)
		{
			RecipeRepository.Instance.Delete(SessionHandler.CurrentGuid);
			RecipeRepository.Instance.DeleteSpecials(SessionHandler.CurrentGuid);
			RatingRepository.Instance.Delete(SessionHandler.CurrentGuid);
			Response.Redirect(Globals.DefaultUrl);
		}

		private void OnEditLinkButtonClick(object sender, EventArgs e)
		{
			LoadRecipe(SessionHandler.CurrentId, true);
			SetView(addEditView);
			addButton.Text = "Uppdatera!";
			clearButton.Visible = false;
		}

		#endregion

	}
}
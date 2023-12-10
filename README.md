# ProductPricings
Product Pricing: A case study for OOAD in the study year 2023-24

1-Building the project ProductApi
  > dotnet build

2-Preparing migration
  > dotnet ef migrations add "InitProducts" --context sqldbcontext

3-Updating database
  > dotnet ef database update --context sqldbcontext

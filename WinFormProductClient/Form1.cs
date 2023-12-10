using ProductLib;

namespace WinFormProductClient
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new();
        public Form1()
        {
            InitializeComponent();
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            cboCreateCat.DataSource = Enum.GetValues<Category>();
            cboUpdateCat.DataSource = Enum.GetValues<Category>();
            bs.DataSource = new List<CoffeResponse>();
            dgvProducts.DataSource = bs;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            btnRefresh.Click += DoClickRefresh;

            btnCreateSubmit.Click += DoClickCreateSubmit;

            bs.PositionChanged += DoProductPositionChanged;

            btnUpdateSubmit.Click += DoClickUpdateSubmit;

            btnDelete.Click += DoClickDelete;

            btnCreateClear.Click += DoClickCreateClear;
        }

        private void DoClickCreateClear(object? sender, EventArgs e)
        {
            txtCreateCode.Clear();
            txtCreateName.Clear();
            cboCreateCat.SelectedItem = Category.None;
        }

        private async void DoClickDelete(object? sender, EventArgs e)
        {
            if (bs.Current == null) return;
            if (bs.Current is not CoffeResponse prd) return;
            string endpoint = $"api/products/{prd.Id}";
            var result = await Program.RestClient.DeleteAsync<Result<string?>>(endpoint);
            if (result!.Succeded && prd.Id == result!.Data)
            {
                bs.RemoveCurrent();
                bs.ResetBindings(false);
            }
            MessageBox.Show(result!.Message);
        }

        private async void DoClickUpdateSubmit(object? sender, EventArgs e)
        {
            string endpoint = "api/products";
            Category cat = ((Category)cboUpdateCat.SelectedItem);
            var req = new CoffeUpdateReq()
            {
                Key = txtUpdateCode.Text,
                Name = txtUpdateName.Text,
                Category = cat == Category.None ? null : cat.ToString()
            };
            var result = await Program.RestClient.PutAsync<CoffeUpdateReq, Result<string?>>(endpoint, req);
            Task task = Task.Run(async () =>
            {
                if (result!.Succeded)
                {
                    endpoint = $"api/Products/{result!.Data!}";
                    var foundResult = await Program.RestClient.GetAsync<Result<CoffeResponse?>>(endpoint);
                    if (foundResult!.Succeded && foundResult.Data != null)
                    {
                        var found = (bs.DataSource as List<CoffeResponse>)?.FirstOrDefault(b => b.Id == foundResult.Data.Id);
                        if (found != null)
                        {
                            found.Name = foundResult.Data.Name;
                            found.Category = foundResult.Data.Category;
                            bs.ResetBindings(false);
                        }
                    }
                }
            });
            MessageBox.Show(result!.Message);
            task.Wait();
        }

        private void DoProductPositionChanged(object? sender, EventArgs e)
        {
            if (bs.Current == null)
            {
                txtUpdateCode.Clear();
                txtUpdateName.Clear();
                cboUpdateCat.SelectedItem = Category.None;
            }
            else
            {
                if (bs.Current is CoffeResponse prd)
                {
                    txtUpdateCode.Text = prd.Code.ToString();
                    txtUpdateName.Text = prd.Name;
                    Category cat = Category.None;
                    Enum.TryParse<Category>(prd.Category ?? "", out cat);
                    cboUpdateCat.SelectedItem = cat;
                }
            }
        }

        private async void DoClickCreateSubmit(object? sender, EventArgs e)
        {
            string endpoint = "api/Products";
            Category cat = ((Category)cboCreateCat.SelectedItem);
            var req = new CoffeCreateReq()
            {
                Code = txtCreateCode.Text,
                Name = txtCreateName.Text,
                Category = cat == Category.None ? null : cat.ToString()
            };
            var result = await Program.RestClient.PostAsync<CoffeCreateReq, Result<string?>>(endpoint, req);
            Task task = Task.Run(async () =>
            {
                if (result!.Succeded)
                {
                    endpoint = $"api/Products/{result!.Data}";
                    var foundResult = await Program.RestClient.GetAsync<Result<CoffeResponse?>>(endpoint);
                    if (foundResult!.Succeded && foundResult.Data != null)
                    {
                        (bs.DataSource as List<CoffeResponse>)?.Add(foundResult.Data);
                        bs.ResetBindings(false);
                    }
                }
            });
            MessageBox.Show(result!.Message);
        }

        private async void DoClickRefresh(object? sender, EventArgs e)
        {
            string endpoint = "api/Products";
            var result = await Program.RestClient.GetAsync<Result<List<CoffeResponse>>>(endpoint);
            if (result!.Succeded == true)
            {
                bs.DataSource = result.Data;
                bs.ResetBindings(false);
            }
        }

    }
}
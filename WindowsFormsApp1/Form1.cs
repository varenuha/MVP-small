using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	// вью
	interface IView
	{
		void SetText(string str);
		void SetTree(List<GroupServer> listserver);
	}
	public partial class Form1 : Form, IView
	{
		private Presenter presenter;
		public Form1()
		{
			InitializeComponent();
			presenter = new Presenter(this, new Model());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			presenter.Load();
		}
		void IView.SetText(string str)
		{
			textBox1.Text = str;
		}
		void IView.SetTree(List<GroupServer> listserver)
		{
			for (int i = 0; i < listserver.Count; i++)
			{
				TreeNode newTree = new TreeNode();
				newTree.Name = listserver[i].NameServer + "_item";
				newTree.Text = listserver[i].NameServer;
				newTree.ToolTipText = listserver[i].DescriptionServer;
				treeView1.Nodes.Add(newTree);
			}
		}

	}

	// презентер

	class Presenter
	{
		private readonly IView iview;
		private readonly IModel imodel;

		public Presenter(IView iview, IModel imodel)
		{
			this.iview = iview;
			this.imodel = imodel;
		}
		public void Load()
		{
			string text = imodel.Load();
			List<GroupServer> listserver = imodel.LoadTree();
			iview.SetText(text);
			iview.SetTree(listserver);

		}
	}

	// модел

	interface IModel
	{
		string Load();
		List<GroupServer> LoadTree();
	}
	class GroupServer
	{
		public string NameServer { get; set; }
		public string DescriptionServer { get; set; }
		public GroupServer(string nameserver, string descserver)
		{
			NameServer = nameserver;
			DescriptionServer = descserver;
		}
	}
	class Model : IModel
	{
		string IModel.Load()
		{
			return "Text loaded";
		}
		List<GroupServer> IModel.LoadTree()
		{
			List<GroupServer> listserver = new List<GroupServer>();
			listserver.Add(new GroupServer("0001", "Server 0001"));
			listserver.Add(new GroupServer("0002", "Server 0002"));
			return (listserver);
		}
	}


}


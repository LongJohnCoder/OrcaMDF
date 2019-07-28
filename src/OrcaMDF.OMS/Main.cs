using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OrcaMDF.Core.Engine;
using OrcaMDF.Core.MetaData;
using OrcaMDF.RawCore;
using OrcaMDF.RawCore.Types;
using OrcaMDF.Framework;
using OrcaMDF.RawCore.Records;

namespace OrcaMDF.OMS
{
	public partial class Main : Form
	{
		private Database db;
        private RawDataFile dbf;
        private IEnumerable<dynamic> rawtables;
        private IEnumerable<dynamic> rawcolumns;
        private Dictionary<String, IRawType[]> RawTablesSchemas = new Dictionary<String, IRawType[]>();
        private string file = "";

		public Main()
		{
			InitializeComponent();

			Disposed += Main_Disposed;
		}

		public Main(string[] fileNames)
			: this()
		{
			if (fileNames == null || !fileNames.Any()) 
				return;
			
			var badFileNames = fileNames.Where(fileName => !File.Exists(fileName));
			
			if (badFileNames.Any())
			{
				var msg = new StringBuilder("The following files specified on the command line do not exist:");
				msg.AppendLine();
				foreach (var fileName in badFileNames) { msg.AppendFormat("\t{0}{1}", fileName, Environment.NewLine); }
				msg.Append("OrcaMDF.OMS must edit");
				MessageBox.Show(msg.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				Close();
				Environment.Exit(4);
			}
			
			try
			{
				db = new Database(fileNames);
				refreshTreeview();
			}
			catch (Exception ex)
			{
				logException(ex);
			}
		}

		void Main_Disposed(object sender, EventArgs e)
		{
			if (db != null)
				db.Dispose();
		}

		private void logException(Exception ex, String extrainfo = "", Boolean silent = true)
		{
            if (ex != null)
            {
                File.AppendAllText("ErrorLog.txt",
                    DateTime.Now +
                    Environment.NewLine +
                    "----------" +
                    Environment.NewLine +
                    ex +
                    Environment.NewLine +
                    Environment.NewLine +
                    extrainfo);
            }
            else
            {
                File.AppendAllText("ErrorLog.txt",
                    DateTime.Now +
                    Environment.NewLine +
                    Environment.NewLine +
                    extrainfo);
            }

            if (silent != true)
            {
                string msg =
                    "An exception has occurred:" + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    Environment.NewLine +
                    "To help improve OrcaMDF, I would appreciate if you would send the ErrorLog.txt file to me at mark@improve.dk" + Environment.NewLine +
                    Environment.NewLine +
                    "The error log does not contain any sensitive information, feel free to check it to be 100% certain. The ErrorLog.txt file is located in the same directory as the OrcaMDF Studio application." +
                    Environment.NewLine + extrainfo;

                MessageBox.Show(msg, "Uh oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}

		private void openToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			var result = openDatabaseDialog.ShowDialog();
            file = "";

            if (result == DialogResult.OK)
            {
                try
                {
                    var files = openDatabaseDialog.FileNames;
                    file = files[0];
                    db = new Database(files);

                    refreshTreeview();} catch (Exception ex)
                {
                    logException(ex);

                    try
                    {
                        dbf = new RawDataFile(file);

                        var records = dbf.Pages
                            .Where(x => x.Header.ObjectID == 34 && x.Header.Type == PageType.Data)
                            .SelectMany(x => x.Records);

                        var rows = records.Select(x => RawColumnParser.Parse((RawRecord)x, new IRawType[] {
                            RawType.Int("id"),
                            RawType.NVarchar("name"),
                            RawType.Int("nsid"),
                            RawType.TinyInt("nsclass"),
                            RawType.Int("status"),
                            RawType.Char("type", 2),
                            RawType.Int("pid"),
                            RawType.TinyInt("pclass"),
                            RawType.Int("intprop"),
                            RawType.DateTime("created"),
                            RawType.DateTime("modified")
                        }));

                        rawtables = rows
                                    .Where(x => x.GetColumnValue("type").ToString().Trim() == "U");

                        rawrefreshTreeview();
                    }
                    catch (Exception rawex)
                    {
                        logException(rawex);

                        try
                        {
                            var bestdbf = new RawDataFile(file);

                            var SysschobjsPages = bestdbf.Pages
                                .Where(x => x.Header.ObjectID == 34 && x.Header.Type == PageType.Data);

                            var bestRecords = RawColumnParser.BestEffortParse(SysschobjsPages, new IRawType[] {
                                RawType.Int("id"),
                                RawType.NVarchar("name"),
                                RawType.Int("nsid"),
                                RawType.TinyInt("nsclass"),
                                RawType.Int("status"),
                                RawType.Char("type", 2),
                                RawType.Int("pid"),
                                RawType.TinyInt("pclass"),
                                RawType.Int("intprop"),
                                RawType.DateTime("created"),
                                RawType.DateTime("modified")
                            });

                            rawtables = bestRecords
                                        .Where(x => x.GetColumnValue("type").ToString().Trim() == "U");

                            var syscolparspages = bestdbf.Pages.Where(x => x.Header.ObjectID == 41 && x.Header.Type == PageType.Data);
                            var syscolparsrecords = syscolparspages.SelectMany(x => x.Records).Select(x => (RawPrimaryRecord)x);
                            var syscolparsrows = RawColumnParser.BestEffortParse(syscolparspages, new IRawType[] {
                                RawType.Int("id"),
                                RawType.SmallInt("number"),
                                RawType.Int("colid"),
                                RawType.NVarchar("name"),
                                RawType.TinyInt("xtype"),
                                RawType.Int("utype"),
                                RawType.SmallInt("length"),
                                RawType.TinyInt("prec"),
                                RawType.TinyInt("scale"),
                                RawType.Int("collationid"),
                                RawType.Int("status"),
                                RawType.SmallInt("maxinrow"),
                                RawType.Int("xmlns"),
                                RawType.Int("dflt"),
                                RawType.Int("chk"),
                                RawType.VarBinary("idtval")
                            });

                            rawcolumns = syscolparsrows.Select(x => new
                            {
                                ObjectID = (int?)x.GetColumnValue("id"),
                                ColumnID = (int?)x.GetColumnValue("colid"),
                                Number = (short?)x.GetColumnValue("number"),
                                TypeID = (byte?)x.GetColumnValue("xtype"),
                                Length = (short?)x.GetColumnValue("length"),
                                Name = x.GetColumnValue("name")
                            });

                            rawrefreshTreeview();
                        }
                        catch (Exception bestrawex)
                        {
                            logException(bestrawex);
                        }
                    }
                }
            }
		}

        private static IEnumerable<RawPage> sysschobjspages(IEnumerable<RawPage> pages)
        {
            foreach (var page in pages)
            {
                var pageheader = page.Header;
                if (pageheader.ObjectID == 34)
                {
                    yield return page;
                }
            }
        }

        private void refreshTreeview()
		{
			var rootNode = new TreeNode(db.Name);

			// Add base tables
			addBaseTablesNode(rootNode);

			// Add DMVs
			addDmvNodes(rootNode);

			// Add tables
			addTablesNode(rootNode);

			// Add programmability
			addProgrammabilityNode(rootNode);
			

			// Refresh treeview
			treeview.Nodes.Clear();
			treeview.Nodes.Add(rootNode);
		}

        private void rawrefreshTreeview()
        {
            var rootNode = new TreeNode(file);

            // Add base tables
            // rawaddBaseTablesNode(rootNode);

            // Add DMVs
            // rawaddDmvNodes(rootNode);

            // Add tables
            rawaddTablesNode(rootNode);

            // Add programmability
            // addProgrammabilityNode(rootNode);


            // Refresh treeview
            treeview.Nodes.Clear();
            treeview.Nodes.Add(rootNode);
        }

        private void addProgrammabilityNode(TreeNode rootNode)
		{
			var prgRootNode = rootNode.Nodes.Add("Programmability");

			addStoredProceduresNode(prgRootNode);
			addViewsNode(prgRootNode);
		}

		private void addViewsNode(TreeNode prgRootNode)
		{
			var viewsNode = prgRootNode.Nodes.Add("Views");
			var views = db.Dmvs.Views.OrderBy(v => v.Name);

			foreach (var view in views)
			{
				var viewNode = viewsNode.Nodes.Add(view.Name);
				viewNode.ContextMenu = viewMenu;
			}
		}

		private void addStoredProceduresNode(TreeNode prgRootNode)
		{
			var proceduresNode = prgRootNode.Nodes.Add("Stored Procedures");
			var procedures = db.Dmvs.Procedures.OrderBy(p => p.Name);

			foreach (var proc in procedures)
			{
				var procNode = proceduresNode.Nodes.Add(proc.Name);
				procNode.ContextMenu = procedureMenu;
			}
		}

		private void addBaseTablesNode(TreeNode rootNode)
		{
			var baseTableNode = rootNode.Nodes.Add("Base Tables");
			baseTableNode.Nodes.Add("sys.sysallocunits").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.syscolpars").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysidxstats").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysiscols").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysobjvalues").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysowners").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysrowsets").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysrscols").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysscalartypes").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.sysschobjs").ContextMenu = baseTableMenu;
			baseTableNode.Nodes.Add("sys.syssingleobjrefs").ContextMenu = baseTableMenu;
		}

        private void rawaddBaseTablesNode(TreeNode rootNode)
        {
            var baseTableNode = rootNode.Nodes.Add("Base Tables");
            baseTableNode.Nodes.Add("sys.sysallocunits").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.syscolpars").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysidxstats").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysiscols").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysobjvalues").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysowners").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysrowsets").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysrscols").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysscalartypes").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.sysschobjs").ContextMenu = baseTableMenu;
            baseTableNode.Nodes.Add("sys.syssingleobjrefs").ContextMenu = baseTableMenu;
        }

        private void addDmvNodes(TreeNode rootNode)
		{
			var dmvNode = rootNode.Nodes.Add("DMVs");
			dmvNode.Nodes.Add("sys.columns").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.database_principals").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.foreign_keys").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.indexes").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.index_columns").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.objects").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.objects$").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.partitions").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.procedures").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.sql_modules").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.system_internals_allocation_units").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.system_internals_partitions").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.system_internals_partition_columns").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.tables").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.types").ContextMenu = dmvMenu;
			dmvNode.Nodes.Add("sys.views").ContextMenu = dmvMenu;
		}

        private void rawaddDmvNodes(TreeNode rootNode)
        {
            var dmvNode = rootNode.Nodes.Add("DMVs");
            dmvNode.Nodes.Add("sys.columns").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.database_principals").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.foreign_keys").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.indexes").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.index_columns").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.objects").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.objects$").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.partitions").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.procedures").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.sql_modules").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.system_internals_allocation_units").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.system_internals_partitions").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.system_internals_partition_columns").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.tables").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.types").ContextMenu = dmvMenu;
            dmvNode.Nodes.Add("sys.views").ContextMenu = dmvMenu;
        }

        private void addTablesNode(TreeNode rootNode)
		{
			var tableRootNode = rootNode.Nodes.Add("Tables");
			var tables = db.Dmvs.Tables.OrderBy(t => t.Name);

			foreach (var t in tables)
			{
				var tableNode = tableRootNode.Nodes.Add(t.Name);
				tableNode.ContextMenu = tableMenu;

				// Add columns
				var tableColumnsNode = tableNode.Nodes.Add("Columns");
				var columns = db.Dmvs.Columns
					.Where(c => c.ObjectID == t.ObjectID)
					.OrderBy(c => c.Name);

				foreach (var c in columns)
				{
					var mainColumn = db.Dmvs.Columns.Where(x => x.ColumnID == c.ColumnID && x.ObjectID == c.ObjectID).Single();
					var type = db.Dmvs.Types.Where(x => x.SystemTypeID == mainColumn.SystemTypeID).First();
					
					tableColumnsNode.Nodes.Add(c.Name + " (" + type.Name + "[" + type.MaxLength + "])");
				}

				// Add indexes
				var tableIndexesNode = tableNode.Nodes.Add("Indexes");
				var indexes = db.Dmvs.Indexes
					.Where(i => i.ObjectID == t.ObjectID && i.IndexID > 0)
					.OrderBy(i => i.Name);

				foreach (var i in indexes)
				{
					var indexNode = tableIndexesNode.Nodes.Add(i.Name);

					// Add index columns
					var indexColumns = db.Dmvs.IndexColumns
						.Where(ic => ic.ObjectID == t.ObjectID && ic.IndexID == i.IndexID);

					foreach (var ic in indexColumns)
					{
						var mainColumn = db.Dmvs.Columns.Where(x => x.ColumnID == ic.ColumnID && x.ObjectID == ic.ObjectID).Single();
						var type = db.Dmvs.Types.Where(x => x.SystemTypeID == mainColumn.SystemTypeID).First();

						indexNode.Nodes.Add(columns.Where(c => c.ColumnID == ic.ColumnID).Single().Name + " (" + type.Name + "[" + type.MaxLength + "])");
					}
				}
			}
		}

        private void rawaddTablesNode(TreeNode rootNode)
        {
            var tableRootNode = rootNode.Nodes.Add("Tables");
            var tables = rawtables.OrderBy(t => t.Name);

            foreach (var t in tables)
            {
                var tableNode = tableRootNode.Nodes.Add(t.Name);
                tableNode.ContextMenu = tableMenu;

                logException(null, "Tabla: [" + t.Name + "]");

                // Add columns
                var tableColumnsNode = tableNode.Nodes.Add("Columns");
                var columns = rawcolumns
                    .Where(c => c.ObjectID == t.id)
                    .OrderBy(c => c.Name);

                rawcolumnType[] rawcolumntypes = new rawcolumnType[30];
                rawcolumntypes[0] = new rawcolumnType("image", 34, 16);
                rawcolumntypes[1] = new rawcolumnType("text", 35, 16);
                rawcolumntypes[2] = new rawcolumnType("uniqueidentifier", 36, 16);
                rawcolumntypes[3] = new rawcolumnType("date", 40, 3);
                rawcolumntypes[4] = new rawcolumnType("time", 41, 5);
                rawcolumntypes[5] = new rawcolumnType("datetime2", 42, 8);
                rawcolumntypes[6] = new rawcolumnType("datetimeoffset", 43, 10);
                rawcolumntypes[7] = new rawcolumnType("tinyint", 48, 1);
                rawcolumntypes[8] = new rawcolumnType("smallint", 52, 2);
                rawcolumntypes[9] = new rawcolumnType("int", 56, 4);
                rawcolumntypes[10] = new rawcolumnType("smalldatetime", 58, 4);
                rawcolumntypes[11] = new rawcolumnType("real", 59, 4);
                rawcolumntypes[12] = new rawcolumnType("money", 60, 8);
                rawcolumntypes[13] = new rawcolumnType("datetime", 61, 8);
                rawcolumntypes[14] = new rawcolumnType("float", 62, 8);
                rawcolumntypes[15] = new rawcolumnType("sql_variant", 98, 8016);
                rawcolumntypes[16] = new rawcolumnType("ntext", 99, 16);
                rawcolumntypes[17] = new rawcolumnType("bit", 104, 1);
                rawcolumntypes[18] = new rawcolumnType("decimal", 106, 17);
                rawcolumntypes[19] = new rawcolumnType("numeric", 108, 17);
                rawcolumntypes[20] = new rawcolumnType("smallmoney", 122, 4);
                rawcolumntypes[21] = new rawcolumnType("bigint", 127, 8);
                rawcolumntypes[22] = new rawcolumnType("varbinary", 165, 8000);
                rawcolumntypes[23] = new rawcolumnType("varchar", 167, 8000);
                rawcolumntypes[24] = new rawcolumnType("binary", 173, 8000);
                rawcolumntypes[25] = new rawcolumnType("char", 175, 8000);
                rawcolumntypes[26] = new rawcolumnType("timestamp", 189, 8);
                rawcolumntypes[27] = new rawcolumnType("nvarchar", 231, 8000);
                rawcolumntypes[28] = new rawcolumnType("nchar", 239, 8000);
                rawcolumntypes[29] = new rawcolumnType("xml", 241, -1);


                foreach (var c in columns)
                {
                    try
                    {
                        var mainColumn = columns.Where(x => x.ColumnID == c.ColumnID && x.ObjectID == c.ObjectID).First();
                        var type = rawcolumntypes.Where(x => x.col_System_type_id == mainColumn.TypeID).First();

                        tableColumnsNode.Nodes.Add(c.Name + " (" + type.columnName + "[" + type.max_Length + "])");

                        logException(null, "Column: [" + t.Name + "]." + "[" + c.Name + "]");
                    } catch (Exception ex)
                    {
                        logException(ex, "Table: "+ t.Name + " Column: " + c.Name + " ColumnID: " + c.ColumnID + " ObjectID: " + c.ObjectID);
                    }
                 }

                try
                {
                    var unorderedcolumns = rawcolumns
                    .Where(c => c.ObjectID == t.id);

                    IRawType[] SchemaRawType = new IRawType[] { };

                    var unorderedFirstColumn = unorderedcolumns.First();
                    Boolean firstColumn = true;

                    var unorderedFirstColumntype = rawcolumntypes.Where(x => x.col_System_type_id == unorderedFirstColumn.TypeID).First();

                    Array.Resize(ref SchemaRawType, 1);

                    switch (unorderedFirstColumntype.col_System_type_id)
                    {
                        case 34:
                            SchemaRawType[0] = RawType.Binary(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 35:
                            SchemaRawType[0] = RawType.Binary(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 36:
                            SchemaRawType[0] = RawType.UniqueIdentifier(unorderedFirstColumn.Name);
                            break;
                        case 40:
                            SchemaRawType[0] = RawType.Date(unorderedFirstColumn.Name);
                            break;
                        case 41:
                            SchemaRawType[0] = RawType.DateTime(unorderedFirstColumn.Name);
                            break;
                        case 42:
                            SchemaRawType[0] = RawType.DateTime(unorderedFirstColumn.Name);
                            break;
                        case 43:
                            SchemaRawType[0] = RawType.DateTime(unorderedFirstColumn.Name);
                            break;
                        case 48:
                            SchemaRawType[0] = RawType.TinyInt(unorderedFirstColumn.Name);
                            break;
                        case 52:
                            SchemaRawType[0] = RawType.SmallInt(unorderedFirstColumn.Name);
                            break;
                        case 56:
                            SchemaRawType[0] = RawType.Int(unorderedFirstColumn.Name);
                            break;
                        case 58:
                            SchemaRawType[0] = RawType.DateTime(unorderedFirstColumn.Name);
                            break;
                        case 59:
                            SchemaRawType[0] = RawType.Decimal(unorderedFirstColumn.Name, Convert.ToByte(15), Convert.ToByte(2));
                            break;
                        case 60:
                            SchemaRawType[0] = RawType.Money(unorderedFirstColumn.Name);
                            break;
                        case 61:
                            SchemaRawType[0] = RawType.DateTime(unorderedFirstColumn.Name);
                            break;
                        case 62:
                            if (unorderedFirstColumn.Length <= 24)
                            {
                                SchemaRawType[0] = RawType.Decimal(unorderedFirstColumn.Name, Convert.ToByte(7), Convert.ToByte(2));
                            }
                            else
                            {
                                SchemaRawType[0] = RawType.Decimal(unorderedFirstColumn.Name, Convert.ToByte(15), Convert.ToByte(2));
                            }
                            break;
                        case 98:
                            SchemaRawType[0] = RawType.Binary(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 99:
                            SchemaRawType[0] = RawType.Binary(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 104:
                            SchemaRawType[0] = RawType.Bit(unorderedFirstColumn.Name);
                            break;
                        case 106:
                            SchemaRawType[0] = RawType.Decimal(unorderedFirstColumn.Name, Convert.ToByte(28), Convert.ToByte(6));
                            break;
                        case 108:
                            SchemaRawType[0] = RawType.Decimal(unorderedFirstColumn.Name, Convert.ToByte(28), Convert.ToByte(6));
                            break;
                        case 122:
                            SchemaRawType[0] = RawType.Money(unorderedFirstColumn.Name);
                            break;
                        case 127:
                            SchemaRawType[0] = RawType.BigInt(unorderedFirstColumn.Name);
                            break;
                        case 165:
                            SchemaRawType[0] = RawType.VarBinary(unorderedFirstColumn.Name);
                            break;
                        case 167:
                            SchemaRawType[0] = RawType.Varchar(unorderedFirstColumn.Name);
                            break;
                        case 173:
                            SchemaRawType[0] = RawType.Binary(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 175:
                            SchemaRawType[0] = RawType.Char(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 189:
                            SchemaRawType[0] = RawType.Binary(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 231:
                            SchemaRawType[0] = RawType.NVarchar(unorderedFirstColumn.Name);
                            break;
                        case 239:
                            SchemaRawType[0] = RawType.NChar(unorderedFirstColumn.Name, unorderedFirstColumn.Length);
                            break;
                        case 241:
                            SchemaRawType[0] = RawType.Xml(unorderedFirstColumn.Name);
                            break;
                        default:
                            SchemaRawType[0] = RawType.VarBinary(unorderedFirstColumn.Name);
                            break;
                    }


                    logException(null, "Column: [" + t.Name + "]." + "[" + unorderedFirstColumn.Name + "]");

                    int i = 1;
                    foreach (var c in unorderedcolumns)
                    {
                        if (firstColumn != true)
                        {
                            try
                            {
                                var nextColumn = unorderedcolumns.Where(x => x.ColumnID == c.ColumnID && x.ObjectID == c.ObjectID).First();
                                var type = rawcolumntypes.Where(x => x.col_System_type_id == c.TypeID).First();

                                Array.Resize(ref SchemaRawType, i + 1);
                                SchemaRawType[i] = new RawNVarchar("");
                                Boolean uniqueColumn;
                                try
                                {
                                    uniqueColumn = (SchemaRawType.Where(x => x.Name.StartsWith(nextColumn.Name)).Count() == 0);
                                } catch (Exception ex)
                                {
                                    logException(ex, "Column: [" + t.Name + "].[" + c.Name + "]");
                                    uniqueColumn = true;
                                }
                                if (uniqueColumn)
                                {
                                    switch (type.col_System_type_id)
                                    {
                                        case 34:
                                            SchemaRawType[i] = RawType.Binary(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 35:
                                            SchemaRawType[i] = RawType.Binary(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 36:
                                            SchemaRawType[i] = RawType.UniqueIdentifier(nextColumn.Name);
                                            break;
                                        case 40:
                                            SchemaRawType[i] = RawType.Date(nextColumn.Name);
                                            break;
                                        case 41:
                                            SchemaRawType[i] = RawType.DateTime(nextColumn.Name);
                                            break;
                                        case 42:
                                            SchemaRawType[i] = RawType.DateTime(nextColumn.Name);
                                            break;
                                        case 43:
                                            SchemaRawType[i] = RawType.DateTime(nextColumn.Name);
                                            break;
                                        case 48:
                                            SchemaRawType[i] = RawType.TinyInt(nextColumn.Name);
                                            break;
                                        case 52:
                                            SchemaRawType[i] = RawType.SmallInt(nextColumn.Name);
                                            break;
                                        case 56:
                                            SchemaRawType[i] = RawType.Int(nextColumn.Name);
                                            break;
                                        case 58:
                                            SchemaRawType[i] = RawType.DateTime(nextColumn.Name);
                                            break;
                                        case 59:
                                            SchemaRawType[i] = RawType.Decimal(nextColumn.Name, Convert.ToByte(15), Convert.ToByte(2));
                                            break;
                                        case 60:
                                            SchemaRawType[i] = RawType.Money(nextColumn.Name);
                                            break;
                                        case 61:
                                            SchemaRawType[i] = RawType.DateTime(nextColumn.Name);
                                            break;
                                        case 62:
                                            if (nextColumn.Length <= 24)
                                            {
                                                SchemaRawType[i] = RawType.Decimal(nextColumn.Name, Convert.ToByte(7), Convert.ToByte(2));
                                            } else
                                            {
                                                SchemaRawType[i] = RawType.Decimal(nextColumn.Name, Convert.ToByte(15), Convert.ToByte(2));
                                            }
                                            break;
                                        case 98:
                                            SchemaRawType[i] = RawType.Binary(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 99:
                                            SchemaRawType[i] = RawType.Binary(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 104:
                                            SchemaRawType[i] = RawType.Bit(nextColumn.Name);
                                            break;
                                        case 106:
                                            SchemaRawType[i] = RawType.Decimal(nextColumn.Name, Convert.ToByte(28), Convert.ToByte(6));
                                            break;
                                        case 108:
                                            SchemaRawType[i] = RawType.Decimal(nextColumn.Name, Convert.ToByte(28), Convert.ToByte(6));
                                            break;
                                        case 122:
                                            SchemaRawType[i] = RawType.Money(nextColumn.Name);
                                            break;
                                        case 127:
                                            SchemaRawType[i] = RawType.BigInt(nextColumn.Name);
                                            break;
                                        case 165:
                                            SchemaRawType[i] = RawType.VarBinary(nextColumn.Name);
                                            break;
                                        case 167:
                                            SchemaRawType[i] = RawType.Varchar(nextColumn.Name);
                                            break;
                                        case 173:
                                            SchemaRawType[i] = RawType.Binary(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 175:
                                            SchemaRawType[i] = RawType.Char(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 189:
                                            SchemaRawType[i] = RawType.Binary(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 231:
                                            SchemaRawType[i] = RawType.NVarchar(nextColumn.Name);
                                            break;
                                        case 239:
                                            SchemaRawType[i] = RawType.NChar(nextColumn.Name, nextColumn.Length);
                                            break;
                                        case 241:
                                            SchemaRawType[i] = RawType.Xml(nextColumn.Name);
                                            break;
                                        default:
                                            SchemaRawType[i] = RawType.VarBinary(unorderedFirstColumn.Name);
                                            break;
                                    }
                                    i = i + 1;
                                    logException(null, "Column: [" + t.Name + "]." + "[" + nextColumn.Name + "]");
                                }
                            }
                            catch (Exception ex)  
                            {
                                logException(ex, "Column: [" + t.Name + "].[" + c.Name + "]");
                            }
                        }
                        else
                        {
                            firstColumn = false;
                        }
                    }
                    RawTablesSchemas.Add(t.Name, SchemaRawType);
                } catch (Exception ex)
                {
                    logException(ex, "Table: " + t.name + ". Failed to get schema");
                }

                // Add indexes
                // var tableIndexesNode = tableNode.Nodes.Add("Indexes");
                // var indexes = db.Dmvs.Indexes
                //     .Where(i => i.ObjectID == t.ObjectID && i.IndexID > 0)
                //     .OrderBy(i => i.Name);

                // foreach (var i in indexes)
                // {
                //      var indexNode = tableIndexesNode.Nodes.Add(i.Name);

                //      Add index columns
                //      var indexColumns = db.Dmvs.IndexColumns
                //      .Where(ic => ic.ObjectID == t.ObjectID && ic.IndexID == i.IndexID);

                //     foreach (var ic in indexColumns)
                //     {
                //          var mainColumn = db.Dmvs.Columns.Where(x => x.ColumnID == ic.ColumnID && x.ObjectID == ic.ObjectID).Single();
                //          var type = db.Dmvs.Types.Where(x => x.SystemTypeID == mainColumn.SystemTypeID).First();

                //          indexNode.Nodes.Add(columns.Where(c => c.ColumnID == ic.ColumnID).Single().Name + " (" + type.Name + "[" + type.MaxLength + "])");
                // }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
            if (db != null)
            {
                loadTable(treeview.SelectedNode.Text);
            }
            if (dbf != null)
            {
                loadRawTable(treeview.SelectedNode.Text);
            }
		}

		private void loadTable(string table)
		{
			try
			{
				var scanner = new DataScanner(db);
				var rows = scanner.ScanTable(table).Take(1000);
				showRows(rows);
			}
			catch (Exception ex)
			{
				logException(ex);
			}
		}

        private void loadRawTable(string table)
        {
            try
            {
                var rawTable = rawtables
                               .Where(x => x.name == table).First();

                var rawSchema = RawTablesSchemas[table];

                var records = dbf.Pages
                    .Where(x => x.Header.ObjectID == rawTable.id && x.Header.Type == PageType.Data)
                    .SelectMany(x => x.Records);

                var rawRows = records.Select(x => RawColumnParser.Parse((RawRecord)x, rawSchema));

                showrawRows(rawRows, rawSchema);
            }
            catch (Exception ex)
            {
                logException(ex);
            }
        }

        private void showRows(IEnumerable<Row> rows)
		{
			grid.DataSource = null;

			if (rows.Count() > 0)
			{
				var ds = new DataSet();
				var tbl = new DataTable();
				ds.Tables.Add(tbl);

				var firstRow = rows.First();

				foreach (var col in firstRow.Columns)
					tbl.Columns.Add(col.Name);

				foreach (var scannedRow in rows)
				{
					var row = tbl.NewRow();

					foreach (var col in scannedRow.Columns)
						row[col.Name] = scannedRow[col];

					tbl.Rows.Add(row);
				}

				grid.DataSource = tbl;
			}

			gridStatusRows.Text = grid.Rows.Count + " Rows";
			txtCode.Visible = false;
			grid.Visible = true;
		}

        private void showrawRows(IEnumerable<dynamic> rows, IRawType[] schema)
        {
            grid.DataSource = null;

            if (rows.Count() > 0)
            {
                var ds = new DataSet();
                var tbl = new DataTable();
                ds.Tables.Add(tbl);

                var firstRow = rows.First();

                foreach (var col in schema)
                    tbl.Columns.Add(col.Name);

                foreach (var scannedRow in rows)
                {
                    var row = tbl.NewRow();

                    foreach (var col in scannedRow.Columns)
                        row[col.Name] = scannedRow[col];

                    tbl.Rows.Add(row);
                }

                grid.DataSource = tbl;
            }

            gridStatusRows.Text = grid.Rows.Count + " Rows";
            txtCode.Visible = false;
            grid.Visible = true;
        }

        private void treeview_MouseUp(object sender, MouseEventArgs e)
		{
			// Make sure right clicking a node also selects it
			if (e.Button == MouseButtons.Right)
				treeview.SelectedNode = treeview.GetNodeAt(e.X, e.Y);
		}

		private IEnumerable<Row> getRowsFromDmv(string dmv)
		{
			switch (dmv)
			{
				case "sys.columns":
					return db.Dmvs.Columns.ToList();
				case "sys.database_principals":
					return db.Dmvs.DatabasePrincipals.ToList();
				case "sys.foreign_keys":
					return db.Dmvs.ForeignKeys.ToList();
				case "sys.indexes":
					return db.Dmvs.Indexes.ToList();
				case "sys.index_columns":
					return db.Dmvs.IndexColumns.ToList();
				case "sys.objects":
					return db.Dmvs.Objects.ToList();
				case "sys.objects$":
					return db.Dmvs.ObjectsDollar.ToList();
				case "sys.partitions":
					return db.Dmvs.Partitions.ToList();
				case "sys.procedures":
					return db.Dmvs.Procedures.ToList();
				case "sys.sql_modules":
					return db.Dmvs.SqlModules.ToList();
				case "sys.system_internals_allocation_units":
					return db.Dmvs.SystemInternalsAllocationUnits.ToList();
				case "sys.system_internals_partitions":
					return db.Dmvs.SystemInternalsPartitions.ToList();
				case "sys.system_internals_partition_columns":
					return db.Dmvs.SystemInternalsPartitionColumns.ToList();
				case "sys.tables":
					return db.Dmvs.Tables.ToList();
				case "sys.types":
					return db.Dmvs.Types.ToList();
				case "sys.views":
					return db.Dmvs.Views.ToList();
				default:
					throw new ArgumentOutOfRangeException(dmv);
			}
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
		    try
		    {
		        showRows(getRowsFromDmv(treeview.SelectedNode.Text));
		    }
		    catch (Exception ex)
		    {
		        logException(ex);
		    }
		}

		private IEnumerable<Row> getRowsFromBaseTable(string table)
		{
			switch(table)
			{
				case "sys.sysallocunits":
					return db.BaseTables.sysallocunits;
				case "sys.syscolpars":
					return db.BaseTables.syscolpars;
				case "sys.sysidxstats":
					return db.BaseTables.sysidxstats;
				case "sys.sysiscols":
					return db.BaseTables.sysiscols;
				case "sys.sysobjvalues":
					return db.BaseTables.sysobjvalues;
				case "sys.sysowners":
					return db.BaseTables.sysowners;
				case "sys.sysrowsets":
					return db.BaseTables.sysrowsets;
				case "sys.sysrscols":
					return db.BaseTables.sysrscols;
				case "sys.sysscalartypes":
					return db.BaseTables.sysscalartypes;
				case "sys.sysschobjs":
					return db.BaseTables.sysschobjs;
				case "sys.syssingleobjrefs":
					return db.BaseTables.syssingleobjrefs;
				default:
					throw new ArgumentOutOfRangeException(table);
			}
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
		    try
		    {
		        showRows(getRowsFromBaseTable(treeview.SelectedNode.Text));
		    }
		    catch (Exception ex)
		    {
		        logException(ex);
		    }
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			showProcedureCode(treeview.SelectedNode.Text);
		}

		private void showProcedureCode(string procedureName)
		{
			// Get procedure ID
			int objID = db.Dmvs.Procedures
				.Where(p => p.Name == procedureName)
				.Select(p => p.ObjectID)
				.Single();

			// Get definition from sql_modules
			string definition = db.Dmvs.SqlModules
				.Where(m => m.ObjectID == objID)
				.Select(m => m.Definition)
				.Single();

			// Set code
			txtCode.Text = definition;

			grid.Visible = false;
			txtCode.Visible = true;
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			showViewCode(treeview.SelectedNode.Text);
		}

		private void showViewCode(string viewName)
		{
			// Get view ID
			int objID = db.Dmvs.Views
				.Where(p => p.Name == viewName)
				.Select(p => p.ObjectID)
				.Single();

			// Get definition from sql_modules
			string definition = db.Dmvs.SqlModules
				.Where(m => m.ObjectID == objID)
				.Select(m => m.Definition)
				.Single();

			// Set code
			txtCode.Text = definition;

			grid.Visible = false;
			txtCode.Visible = true;
		}
	}

    public class rawcolumnType
    {
        public String columnName;
        public int col_System_type_id;
        public int max_Length;

        public rawcolumnType(String columnname, int col_system_type_id, int max_length)
        {
            this.columnName = columnname;
            this.col_System_type_id = col_system_type_id;
            this.max_Length = max_length;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OrcaMDF.RawCore;
using OrcaMDF.RawCore.Types;
using OrcaMDF.Framework;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;
using System.Transactions;

namespace LinqPad
{
    class Program
    {
        [Table(Name="Billetes de Empeño")]
        class BilletesdeEmpeño
        {
            [Column(Name = "Id", IsPrimaryKey = true)]
            public Int32 Id { get; set; }
            [Column(Name = "Folio")]
            public Int64? Folio { get; set; }
            [Column(Name = "Serie")]
            public Char? Serie { get; set; }
            [Column(Name = "IdCasadeEmpeño")]
            public Int32? IdCasadeEmpeño { get; set; }
            [Column(Name = "Tipo de Prestamo")]
            public Int16? TipodePrestamo { get; set; }
            [Column(Name = "Texto Tipo de Prestamo")]
            public String TextoTipodePrestamo { get; set; }
            [Column(Name = "Nombre Consecutivo")]
            public String NombreConsecutivo { get; set; }
            [Column(Name = "Foraneo")]
            public Boolean? Foraneo { get; set; }
            [Column(Name = "IdTasadeInteres")]
            public Int32? IdTasadeInteres { get; set; }
            [Column(Name = "Refrendo No")]
            public Int16? RefrendoNo { get; set; }
            [Column(Name = "Fecha")]
            public DateTime? Fecha { get; set; }
            [Column(Name = "Metodo de Pago")]
            public String MetododePago { get; set; }
            [Column(Name = "Cuenta de Pago")]
            public String CuentadePago { get; set; }
            [Column(Name = "Préstamo")]
            public Double? Préstamo { get; set; }
            [Column(Name = "Aportación Social")]
            public Double? AportaciónSocial { get; set; }
            [Column(Name = "Avalúo")]
            public Double? Avalúo { get; set; }
            [Column(Name = "Nombre")]
            public String Nombre { get; set; }
            [Column(Name = "RFC")]
            public String RFC { get; set; }
            [Column(Name = "IdImagen")]
            public Int32? IdImagen { get; set; }
            [Column(Name = "Imagen")]
            public Binary Imagen { get; set; }
            [Column(Name = "Firma")]
            public String Firma { get; set; }
            [Column(Name = "Dirección")]
            public String Dirección { get; set; }
            [Column(Name = "Colonia")]
            public String Colonia { get; set; }
            [Column(Name = "Municipio")]
            public String Municipio { get; set; }
            [Column(Name = "CP")]
            public String CP { get; set; }
            [Column(Name = "Comentarios")]
            public String Comentarios { get; set; }
            [Column(Name = "Sexo")]
            public String Sexo { get; set; }
            [Column(Name = "Identificación")]
            public String Identificación { get; set; }
            [Column(Name = "No Cliente Presta Puntos")]
            public String NoCLientePrestaPuntos { get; set; }
            [Column(Name = "No Cliente Presta Puntos Anterior")]
            public String NoCLientePrestaPuntosAnterior { get; set; }
            [Column(Name = "Usuario que autoriza el cambio")]
            public String Usuarioqueautorizaelcambio { get; set; }
            [Column(Name = "Fecha de Alta Presta Puntos")]
            public DateTime? FechadeAltaPrestaPuntos { get; set; }
            [Column(Name = "Puntos Cargados")]
            public Double? PuntosCargados { get; set; }
            [Column(Name = "Valor Monetario Puntos Cargados")]
            public Double? ValorMonetarioPuntosCargados { get; set; }
            [Column(Name = "Puntos Usados")]
            public Double? PuntosUsados { get; set; }
            [Column(Name = "Valor Monetario Puntos Usados")]
            public Double? ValorMonetarioPuntosUsados { get; set; }
            [Column(Name = "Telefono (s)")]
            public String Telefonos { get; set; }
            [Column(Name = "Correo")]
            public String Correo { get; set; }
            [Column(Name = "Fecha de Nacimiento")]
            public DateTime? FechadeNacimiento { get; set; }
            [Column(Name = "Publicidad")]
            public String Publicidad { get; set; }
            [Column(Name = "Beneficiario")]
            public String Beneficiario { get; set; }
            [Column(Name = "RFC Beneficiario")]
            public String RFCBeneficiario { get; set; }
            [Column(Name = "IdImagen Beneficiario")]
            public Int32? IdImagenBeneficiario { get; set; }
            [Column(Name = "Imagen Beneficiario")]
            public Binary ImagenBeneficiario { get; set; }
            [Column(Name = "Dirección Beneficiario")]
            public String DirecciónBeneficiario { get; set; }
            [Column(Name = "Colonia Beneficiario")]
            public String ColoniaBeneficiario { get; set; }
            [Column(Name = "Municipio Beneficiario")]
            public String MunicipioBeneficiario { get; set; }
            [Column(Name = "CP Beneficiario")]
            public String CPBeneficiario { get; set; }
            [Column(Name = "Comentarios Beneficiario")]
            public String ComentariosBeneficiario { get; set; }
            [Column(Name = "Sexo Beneficiario")]
            public String SexoBeneficiario { get; set; }
            [Column(Name = "Identificación Beneficiario")]
            public String IdentificaciónBeneficiario { get; set; }
            [Column(Name = "Telefono (s) Beneficiario")]
            public String TelefonosBeneficiario { get; set; }
            [Column(Name = "Fecha de Nacimiento Beneficiario")]
            public DateTime? FechadeNacimientoBeneficiario { get; set; }
            [Column(Name = "Parentesco Beneficiario")]
            public String ParentescoBeneficiario { get; set; }
            [Column(Name = "Cotitular")]
            public String Cotitular { get; set; }
            [Column(Name = "RFC Cotitular")]
            public String RFCCotitular { get; set; }
            [Column(Name = "IdImagen Cotitular")]
            public Int32? IdImagenCotitular { get; set; }
            [Column(Name = "Imagen Cotitular")]
            public Binary ImagenCotitular { get; set; }
            [Column(Name = "Dirección Cotitular")]
            public String DirecciónCotitular { get; set; }
            [Column(Name = "Colonia Cotitular")]
            public String ColoniaCotitular { get; set; }
            [Column(Name = "Municipio Cotitular")]
            public String MunicipioCotitular { get; set; }
            [Column(Name = "CP Cotitular")]
            public String CPCotitular { get; set; }
            [Column(Name = "Comentarios Cotitular")]
            public String ComentariosCotitular { get; set; }
            [Column(Name = "Sexo Cotitular")]
            public String SexoCotitular { get; set; }
            [Column(Name = "Identificación Cotitular")]
            public String IdentificaciónCotitular { get; set; }
            [Column(Name = "Telefono (s) Cotitular")]
            public String TelefonosCotitular { get; set; }
            [Column(Name = "Fecha de Nacimiento Cotitular")]
            public DateTime? FechadeNacimientoCotitular { get; set; }
            [Column(Name = "Parentesco Cotitular")]
            public String ParentescoCotitular { get; set; }
            [Column(Name = "Folio Original")]
            public Int64? FolioOriginal { get; set; }
            [Column(Name = "Tasa de Interés")]
            public Single? TasadeInterés { get; set; }
            [Column(Name = "Fecha de Vencimiento")]
            public DateTime? FechadeVencimiento { get; set; }
            [Column(Name = "Intereses")]
            public Double? Intereses { get; set; }
            [Column(Name = "Liquidación de desempeño")]
            public Double? Liquidacióndedesempeño { get; set; }
        }
        [Table(Name="Billetes de Empeño Detalle")]
        class BilletesdeEmpeñoDetalle
        {
            [Column(Name = "Id", IsPrimaryKey = true)]
            public Int32 Id {get; set; }
            [Column(Name="Folio")]
            public Int64? Folio {get; set;}
            [Column(Name="Serie")]
            public Char? Serie {get; set;}
            [Column(Name="IdCasadeEmpeño")]
            public Int32? IdCasadeEmpeño {get; set;}
            [Column(Name="Tipo de Prestamo")]
            public Int16? TipodePrestamo {get; set;}
            [Column(Name="Texto Tipo de Prestamo")]
            public String TextoTipodePrestamo {get; set;}
            [Column(Name="Nombre Consecutivo")]
            public String NombreConsecutivo {get; set;}
            [Column(Name="Folio Auditoria Detalle")]
            public Int32? FolioAuditoriaDetalle {get; set;}
            [Column(Name="Cantidad")]
            public Int32? Cantidad {get; set;}
            [Column(Name="Saldo Cantidad")]
            public Int32? SaldoCantidad {get; set;}
            [Column(Name="Cantidad Separada")]
            public Int32? CantidadSeparada {get; set;}
            [Column(Name="Descripción")]
            public String Descripción {get; set;}
            [Column(Name="Comentarios")]
            public String Comentarios {get; set;}
            [Column(Name="Notas")]
            public String Notas {get; set;}
            [Column(Name="IdImagen")]
            public Int32? IdImagen {get; set;}
            [Column(Name="Imagen")]
            public Binary Imagen {get; set;}
            [Column(Name="Marca")]
            public String Marca {get; set;}
            [Column(Name="Modelo")]
            public String Modelo {get; set;}
            [Column(Name="Serie No")]
            public String SerieNo {get; set;}
            [Column(Name="Año")]
            public Int16? Año {get; set;}
            [Column(Name="Placa")]
            public String Placa {get; set;}
            [Column(Name="Metal")]
            public Int32? Metal {get; set;}
            [Column(Name="Gramos")]
            public Double? Gramos {get; set;}
            [Column(Name="Saldo Gramos")]
            public Double? SaldoGramos {get; set;}
            [Column(Name="Gramos Separados")]
            public Double? GramosSeparados {get; set;}
            [Column(Name="Cotización")]
            public Double? Cotización {get; set;}
            [Column(Name="Categoría")]
            public string Categoría {get; set;}
            [Column(Name = "IdProducto")]
            public Int32? IdProducto { get; set; }
            [Column(Name = "IdPresentación")]
            public Int32? IdPresentación {get; set;}
            [Column(Name="Avaluo")]
            public Double? Avaluo {get; set;}
            [Column(Name="Prestamo")]
            public Double? Prestamo {get; set;}
            [Column(Name="Color")]
            public String Color {get; set;}
            [Column(Name="Peso")]
            public Double? Peso {get; set;}
            [Column(Name="Calidad")]
            public String Calidad {get; set;}
            [Column(Name="Prestamo Invitado")]
            public Double? PrestamoInvitado {get; set;}
            [Column(Name="SesionActual")]
            public Int32? SesionActual {get; set;}
        }

        static void insertBilletesdeEmpeñoDetalleRows(IEnumerable<dynamic> list)
        {
            Int32 i = 0;
            foreach (dynamic item in list)
            {
                using (System.Transactions.TransactionScope trans = new TransactionScope())
                {
                    DataContext dataContext = new DataContext("Data Source=SISTEMAS05\\SQLEXPRESS_X86;User ID=sa;Password=ale-040804-vale;Initial Catalog=SIC Boletas de Empeño Javier Lopez Recuperada 5;app=LINQPad");
                    dataContext.CommandTimeout = 300;

                    Table<BilletesdeEmpeñoDetalle> BilletesdeEmpeñoDetalles = dataContext.GetTable<BilletesdeEmpeñoDetalle>();

                    dataContext.ExecuteCommand("SET IDENTITY_INSERT [Billetes de Empeño Detalle] ON");

                    i = i + 1;
                    try
                    {
                        BilletesdeEmpeñoDetalles.InsertOnSubmit(new BilletesdeEmpeñoDetalle
                        {
                            Id = i,
                            Folio = (Int64?)item.Folio,
                            Serie = (Char?)item.Serie[0],
                            IdCasadeEmpeño = (Int32?)item.IdCasaDeEmpeño,
                            TipodePrestamo = (Int16?)item.TipodePrestamo,
                            TextoTipodePrestamo = ((String)item.TextoTipodePrestamo).Length < 50 ? ((String)item.TextoTipodePrestamo) : ((String)item.TextoTipodePrestamo).Substring(0, 50),
                            NombreConsecutivo = ((String)item.NombreConsecutivo).Length < 50 ? ((String)item.NombreConsecutivo) : ((String)item.NombreConsecutivo).Substring(0, 50),
                            FolioAuditoriaDetalle = (Int32?)item.FolioAuditoriaDetalle,
                            Cantidad = (Int32?)item.Cantidad,
                            SaldoCantidad = (Int32?)item.SaldoCantidad,
                            CantidadSeparada = (Int32?)item.CantidadSeparada,
                            Descripción = ((String)item.Descripción).Length < 350 ? ((String)item.Descripción) : ((String)item.Descripción).Substring(0, 350),
                            Notas = ((String)item.Notas).Length < 255 ? ((String)item.Notas) : ((String)item.Notas).Substring(0, 255),
                            IdImagen = (Int32?)item.IdImagen,
                            Imagen = (Binary)item.Imagen,
                            Marca = ((String)item.Marca).Length < 50 ? ((String)item.Marca) : ((String)item.Marca).Substring(0, 50),
                            Modelo = ((String)item.Modelo).Length < 50 ? ((String)item.Modelo) : ((String)item.Modelo).Substring(0, 50),
                            SerieNo = ((String)item.SerieNo).Length < 50 ? ((String)item.SerieNo) : ((String)item.SerieNo).Substring(0, 50),
                            Año = (Int16?)item.Año,
                            Placa = ((String)item.Placa).Length < 25 ? ((String)item.Placa) : ((String)item.Placa).Substring(0, 25),
                            Metal = (Int32?)item.Metal,
                            Gramos = (Double?)item.Gramos,
                            SaldoGramos = (Double?)item.SaldoGramos,
                            GramosSeparados = (Double?)item.GramosSeparados,
                            Cotización = (Double?)item.Cotización,
                            Categoría = ((string)item.Categoría).Length < 50 ? ((string)item.Categoría) : ((string)item.Categoría).Substring(0, 50),
                            IdProducto = (Int32?)item.IdProducto,
                            IdPresentación = (Int32?)item.IdPresentación,
                            Avaluo = (Double?)item.Avaluo,
                            Prestamo = (Double?)item.Prestamo,
                            Color = ((String)item.Color).Length < 20 ? ((String)item.Color) : ((String)item.Color).Substring(0, 20),
                            Peso = (Double?)item.Peso,
                            Calidad = ((String)item.Calidad).Length < 20 ? ((String)item.Calidad) : ((String)item.Calidad).Substring(0, 20),
                            PrestamoInvitado = (Double?)item.PrestamoInvitado,
                            SesionActual = (Int32?)item.SesionActual
                        });

                        // Submit the change to the database.
                        try
                        {
                            dataContext.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            // Make some adjustments.
                            // ...
                            // Try again.
                        }

                    }
                    catch { Exception ex; }

                    dataContext.ExecuteCommand("SET IDENTITY_INSERT [Billetes de Empeño Detalle] OFF");

                    trans.Complete();
                }
            }
        }

        static void insertBilletesdeEmpeñoRows(IEnumerable<dynamic> list)
        {
            Int32 i = 0;
            foreach (dynamic item in list)
            {
                using (System.Transactions.TransactionScope trans = new TransactionScope())
                {
                    DataContext dataContext = new DataContext("Data Source=SISTEMAS05\\SQLEXPRESS_X86;User ID=sa;Password=ale-040804-vale;Initial Catalog=SIC Boletas de Empeño Javier Lopez Recuperada 5;app=LINQPad");
                    dataContext.CommandTimeout = 300;

                    Table<BilletesdeEmpeño> BilletesdeEmpeños = dataContext.GetTable<BilletesdeEmpeño>();

                    dataContext.ExecuteCommand("SET IDENTITY_INSERT [Billetes de Empeño] ON");

                    i = i + 1;
                    try
                    {
                        BilletesdeEmpeños.InsertOnSubmit(new BilletesdeEmpeño
                        {
                            Id = i,
                            Folio = (Int64?)item.Folio,
                            Serie = (Char?)item.Serie[0],
                            IdCasadeEmpeño = (Int32?)item.IdCasaDeEmpeño,
                            TipodePrestamo = (Int16?)item.TipodePrestamo,
                            TextoTipodePrestamo = ((String)item.TextoTipodePrestamo).Length < 50 ? ((String)item.TextoTipodePrestamo) : ((String)item.TextoTipodePrestamo).Substring(0, 50),
                            NombreConsecutivo = ((String)item.NombreConsecutivo).Length < 50 ? ((String)item.NombreConsecutivo) : ((String)item.NombreConsecutivo).Substring(0, 50),
                            Foraneo = (Boolean?)(item.Foraneo>0 ? true : false),
                            IdTasadeInteres = (Int32?)item.IdTasadeInteres,
                            RefrendoNo = (Int16?)item.RefrendoNo,
                            Fecha = (DateTime?)item.Fecha,
                            MetododePago = ((String)item.MetododePago).Length < 50 ? ((String)item.MetododePago) : ((String)item.MetododePago).Substring(0, 50),
                            CuentadePago = ((String)item.CuentadePago).Length < 50 ? ((String)item.CuentadePago) : ((String)item.CuentadePago).Substring(0, 50),
                            Préstamo = (Double?)item.Préstamo,
                            Avalúo = (Double?)item.Avalúo,
                            Nombre = ((String)item.Nombre).Length < 50 ? ((String)item.Nombre) : ((String)item.Nombre).Substring(0, 50),
                            RFC = ((String)item.RFC).Length < 50 ? ((String)item.RFC) : ((String)item.RFC).Substring(0, 50),
                            IdImagen = (Int32?)item.IdImagen,
                            Imagen = (Binary)item.Imagen,
                            Dirección = ((String)item.Dirección).Length < 60 ? ((String)item.Dirección) : ((String)item.Dirección).Substring(0, 60),
                            Colonia = ((String)item.Colonia).Length < 50 ? ((String)item.Colonia) : ((String)item.Colonia).Substring(0, 50),
                            Municipio = ((String)item.Municipio).Length < 50 ? ((String)item.Municipio) : ((String)item.Municipio).Substring(0, 50),
                            CP = ((String)item.CP).Length < 20 ? ((String)item.CP) : ((String)item.CP).Substring(0, 20),
                            Comentarios = ((String)item.Comentarios).Length < 254 ? ((String)item.Comentarios) : ((String)item.Comentarios).Substring(0, 254),
                            Sexo = ((String)item.Sexo).Length < 10 ? ((String)item.Sexo) : ((String)item.Sexo).Substring(0, 10),
                            Identificación = ((String)item.Identificación).Length < 50 ? ((String)item.Identificación) : ((String)item.Identificación).Substring(0, 50),
                            NoCLientePrestaPuntos = ((String)item.NoCLientePrestaPuntos).Length < 50 ? ((String)item.NoCLientePrestaPuntos) : ((String)item.NoCLientePrestaPuntos).Substring(0, 50),
                            Usuarioqueautorizaelcambio = ((String)item.Usuarioqueautorizaelcambio).Length < 50 ? ((String)item.Usuarioqueautorizaelcambio) : ((String)item.Usuarioqueautorizaelcambio).Substring(0, 50),
                            FechadeAltaPrestaPuntos = (DateTime?)item.FechadeAltaPrestaPuntos,
                            PuntosCargados = (Double?)item.PuntosCargados,
                            ValorMonetarioPuntosCargados = (Double?)item.ValorMonetarioPuntosCargados,
                            PuntosUsados = (Double?)item.PuntosUsados,
                            ValorMonetarioPuntosUsados = (Double?)item.ValorMonetarioPuntosUsados,
                            Telefonos = ((String)item.Telefonos).Length < 50 ? ((String)item.Telefonos) : ((String)item.Telefonos).Substring(0, 50),
                            Correo = ((String)item.Correo).Length < 50 ? ((String)item.Correo) : ((String)item.Correo).Substring(0, 50),
                            FechadeNacimiento = (DateTime?)item.FechadeNacimiento,
                            Publicidad = ((String)item.Publicidad).Length < 50 ? ((String)item.Publicidad) : ((String)item.Publicidad).Substring(0, 50),
                            Beneficiario = ((String)item.Beneficiario).Length < 50 ? ((String)item.Beneficiario) : ((String)item.Beneficiario).Substring(0, 50),
                            RFCBeneficiario = ((String)item.RFCBeneficiario).Length < 50 ? ((String)item.RFCBeneficiario) : ((String)item.RFCBeneficiario).Substring(0, 50),
                            IdImagenBeneficiario = (Int32?)item.IdImagenBeneficiario,
                            ImagenBeneficiario = (Binary)item.ImagenBeneficiario,
                            DirecciónBeneficiario = ((String)item.DirecciónBeneficiario).Length < 60 ? ((String)item.DirecciónBeneficiario) : ((String)item.DirecciónBeneficiario).Substring(0, 60),
                            ColoniaBeneficiario = ((String)item.ColoniaBeneficiario).Length < 50 ? ((String)item.ColoniaBeneficiario) : ((String)item.ColoniaBeneficiario).Substring(0, 50),
                            MunicipioBeneficiario = ((String)item.MunicipioBeneficiario).Length < 50 ? ((String)item.MunicipioBeneficiario) : ((String)item.MunicipioBeneficiario).Substring(0, 50),
                            CPBeneficiario = ((String)item.CPBeneficiario).Length < 20 ? ((String)item.CPBeneficiario) : ((String)item.CPBeneficiario).Substring(0, 20),
                            ComentariosBeneficiario = ((String)item.ComentariosBeneficiario).Length < 254 ? ((String)item.ComentariosBeneficiario) : ((String)item.ComentariosBeneficiario).Substring(0, 254),
                            SexoBeneficiario = ((String)item.SexoBeneficiario).Length < 10 ? ((String)item.SexoBeneficiario) : ((String)item.SexoBeneficiario).Substring(0, 10),
                            IdentificaciónBeneficiario = ((String)item.IdentificaciónBeneficiario).Length < 50 ? ((String)item.IdentificaciónBeneficiario) : ((String)item.IdentificaciónBeneficiario).Substring(0, 50),
                            TelefonosBeneficiario = ((String)item.TelefonosBeneficiario).Length < 50 ? ((String)item.TelefonosBeneficiario) : ((String)item.TelefonosBeneficiario).Substring(0, 50),
                            FechadeNacimientoBeneficiario = (DateTime?)item.FechadeNacimientoBeneficiario,
                            ParentescoBeneficiario = ((String)item.ParentescoBeneficiario).Length < 50 ? ((String)item.ParentescoBeneficiario) : ((String)item.ParentescoBeneficiario).Substring(0, 50),
                            Cotitular = ((String)item.Cotitular).Length < 50 ? ((String)item.Cotitular) : ((String)item.Cotitular).Substring(0, 50),
                            RFCCotitular = ((String)item.RFCCotitular).Length < 50 ? ((String)item.RFCCotitular) : ((String)item.RFCCotitular).Substring(0, 50),
                            IdImagenCotitular = (Int32?)item.IdImagenCotitular,
                            ImagenCotitular = (Binary)item.ImagenCotitular,
                            DirecciónCotitular = ((String)item.DirecciónCotitular).Length < 60 ? ((String)item.DirecciónCotitular) : ((String)item.DirecciónCotitular).Substring(0, 60),
                            ColoniaCotitular = ((String)item.ColoniaCotitular).Length < 50 ? ((String)item.ColoniaCotitular) : ((String)item.ColoniaCotitular).Substring(0, 50),
                            MunicipioCotitular = ((String)item.MunicipioCotitular).Length < 50 ? ((String)item.MunicipioCotitular) : ((String)item.MunicipioCotitular).Substring(0, 50),
                            CPCotitular = ((String)item.CPCotitular).Length < 20 ? ((String)item.CPCotitular) : ((String)item.CPCotitular).Substring(0, 20),
                            ComentariosCotitular = ((String)item.ComentariosCotitular).Length < 254 ? ((String)item.ComentariosCotitular) : ((String)item.ComentariosCotitular).Substring(0, 254),
                            SexoCotitular = ((String)item.SexoCotitular).Length < 10 ? ((String)item.SexoCotitular) : ((String)item.SexoCotitular).Substring(0, 10),
                            IdentificaciónCotitular = ((String)item.IdentificaciónCotitular).Length < 50 ? ((String)item.IdentificaciónCotitular) : ((String)item.IdentificaciónCotitular).Substring(0, 50),
                            TelefonosCotitular = ((String)item.TelefonosCotitular).Length < 50 ? ((String)item.TelefonosCotitular) : ((String)item.TelefonosCotitular).Substring(0, 50),
                            FechadeNacimientoCotitular = (DateTime?)item.FechadeNacimientoCotitular,
                            ParentescoCotitular = ((String)item.ParentescoCotitular).Length < 50 ? ((String)item.ParentescoCotitular) : ((String)item.ParentescoCotitular).Substring(0, 50),
                            FolioOriginal = (Int64?)item.FolioOriginal,
                            TasadeInterés = (Single?)item.TasadeInterés,
                            FechadeVencimiento = (DateTime?)item.FechadeVencimiento,
                            Intereses = (Double?)item.Intereses,
                            Liquidacióndedesempeño = (Double?)item.Liquidacióndedesempeño,
                        });

                        // Submit the change to the database.
                        try
                        {
                            dataContext.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            // Make some adjustments.
                            // ...
                            // Try again.
                        }

                    }
                    catch { Exception ex; }

                    dataContext.ExecuteCommand("SET IDENTITY_INSERT [Billetes de Empeño] OFF");

                    trans.Complete();
                }
            }
        }

        static void Main(string[] args)
        {
            var dbf = new RawDataFile(@"C:\Temp\Javier Lopez\f231768624.mdf");
            var detallerows = RawColumnParser.BestEffortParse(dbf.Pages.Where(x => (x.Header.ObjectID == 1802 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2338 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2339 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2340 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2341 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2342 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2343 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2344 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2345 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                                || (x.Header.ObjectID == 2347 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)), new IRawType[] {
            //var dbf = new RawDataFile(@"C:\Temp\Javier Lopez\SIC Boletas de Empeño  Javier Lopez.mdf"); 
            //var detallerows = RawColumnParser.BestEffortParse(dbf.Pages.Where(x => (x.Header.ObjectID == 13953 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13412 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13954 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13955 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13956 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13957 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13958 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13959 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
			//						                                            || (x.Header.ObjectID == 13960 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
            //                                                                  || (x.Header.ObjectID == 13962 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)), new IRawType[] {
              RawType.BigInt("Folio"),
              RawType.Char("Serie",1),
              RawType.Int("FolioAuditoriaDetalle"),
              RawType.SmallInt("TipodePrestamo"),
              RawType.NVarchar("Notas"),
              RawType.NVarchar("NombreConsecutivo"),
              RawType.Int("IdCasadeEmpeño"),
              RawType.Int("Cantidad"),
              RawType.Int("SaldoCantidad"),
              RawType.Int("CantidadSeparada"),
              RawType.NVarchar("TextoTipodePrestamo"),
              RawType.NVarchar("Descripción"),
              RawType.NVarchar("Comentarios"),
              RawType.Int("IdImagen"),
              RawType.VarBinary("Imagen"),
              RawType.NVarchar("Marca"),
              RawType.NVarchar("Modelo"),
              RawType.NVarchar("SerieNo"),
              RawType.SmallInt("Año"),
              RawType.NVarchar("Placa"),
              RawType.Int("Metal"),
              RawType.Float("Gramos"),
              RawType.Float("SaldoGramos"),
              RawType.Float("GramosSeparados"),
              RawType.Float("Cotización"),
              RawType.NVarchar("Color"),
              RawType.Int("IdProducto"),
              RawType.Int("IdPresentación"),
              RawType.Float("Avaluo"),
              RawType.Float("Prestamo"),
              RawType.NVarchar("Categoría"),
              RawType.Float("Peso"),
              RawType.NVarchar("Calidad"),
              RawType.Float("PrestamoInvitado"),
              RawType.Int("SesionActual")/*,
              RawType.NVarchar("Ubicación"),
              RawType.NVarchar("Ubicación2"),
              RawType.NVarchar("Ubicación3"),
              RawType.NVarchar("Ubicación4"),
              RawType.Float("PreciodeVenta"),
              RawType.Float("PrecioAjustado"),
              RawType.Bit("Vendido"),
              RawType.DateTime("FechaVenta"),
              RawType.Float("PreciodeVentaReal"),
              RawType.Bit("Separado"),
              RawType.DateTime("FechaSeparación"),
              RawType.DateTime("FechaMáximapararetirar"),
              RawType.Float("MontoAbonado"),
              RawType.Float("Saldo"),
              RawType.DateTime("FechaUltimoAbono"),
              RawType.Float("MontoUltimoAbono"),
              RawType.Bit("Facturado"),
              RawType.DateTime("FechaFactura"),
              RawType.Int("NúmFactura"),
              RawType.NVarchar("Nombre"),
              RawType.NVarchar("Dirección"),
              RawType.NVarchar("Identificación"),
              RawType.NVarchar("Telefonos"),
              RawType.NVarchar("RFC"),
              RawType.Int("NoEjecutarDesencadenadores"),
              RawType.Int("NoValidarUsuario"),
              RawType.NVarchar("Usuario"),
              RawType.NVarchar("Usuario_Baja"),
              RawType.UniqueIdentifier("rowguid"),
              RawType.BigInt("Folio Origen"),
              RawType.Char("Serie Origen",1),
              RawType.Int("IdCasadeEmpeño Origen"),
              RawType.SmallInt("Tipo de Prestamo Origen"),
              RawType.NVarchar("Texto Tipo de Prestamo Origen"),
              RawType.NVarchar("Nombre Consecutivo Origen"),
              RawType.Int("Folio Auditoria Detalle Origen"),
              RawType.Float("Prestamo Folio Origen"),
              RawType.Float("Precio Venta Folio Origen"),
              RawType.Float("Anticipo Recibido Folio Origen"),
              RawType.Int("Cantidad Bloqueada"),
              RawType.NVarchar("Placas"),
              RawType.NVarchar("No. Motor"),
              RawType.NVarchar("No. Serie Chasis"),
              RawType.NVarchar("No. Licencia Manejo"),
              RawType.NVarchar("P.G.R. Robo"),
              RawType.NVarchar("Aseguradora"),
              RawType.NVarchar("No. Poliza"),
              RawType.NVarchar("Gasolina"),
              RawType.NVarchar("Kilometraje"),
              RawType.NVarchar("Daños"),
              RawType.NVarchar("Numero Factura"),
              RawType.NVarchar("Emisor"),
              RawType.Float("Valor de la factura"),
              RawType.DateTime("Fecha de la Factura"),
              RawType.Bit("Instrumentos tablero"),
              RawType.Bit("Calefaccion"),
              RawType.Bit("Limpiadores"),
              RawType.Bit("Radio/Tipo"),
              RawType.Bit("Bocinas"),
              RawType.Bit("Encendedor"),
              RawType.Bit("Espejo Retrovisor"),
              RawType.Bit("Cinturones"),
              RawType.Bit("Botones de Interior"),
              RawType.Bit("Manijas Interiores"),
              RawType.Bit("Tapetes"),
              RawType.Bit("Vestiduras"),
              RawType.Bit("Funcionalidad Motor"),
              RawType.Bit("Cenicero"),
              RawType.Bit("Viseras"),
              RawType.Bit("Unidad de Luces"),
              RawType.Bit("1/4 de Luces"),
              RawType.Bit("Espejo Lateral"),
              RawType.Bit("Cristales"),
              RawType.Bit("Emblema"),
              RawType.Bit("Llantas(4)"),
              RawType.Bit("Bocinas de Claxon"),
              RawType.Bit("Tapon de Gasolina"),
              RawType.Bit("Carroce sin golpes"),
              RawType.Bit("Bocinas de aire"),
              RawType.Bit("Defensas"),
              RawType.Bit("Parrilla"),
              RawType.Bit("Rhines"),
              RawType.Bit("Calaveras"),
              RawType.Bit("Molduras"),
              RawType.Bit("Biseles"),
              RawType.Bit("Tapones de rueda"),
              RawType.Bit("Manijas externas"),
              RawType.Bit("Gato"),
              RawType.Bit("Maneral del gato"),
              RawType.Bit("Llave de ruedas"),
              RawType.Bit("Caja Herramientas"),
              RawType.Bit("Triangulo Seguridad"),
              RawType.Bit("Llanta de refaccion"),
              RawType.Bit("Extinguidor"),
              RawType.Bit("Tapon de Aceite"),
              RawType.Bit("Tapon de Radiador"),
              RawType.Bit("Varilla de Aceite"),
              RawType.Bit("Filtro de Aire"),
              RawType.NVarchar("Pasaporte"),
              RawType.Bit("Bateria"),
              RawType.NVarchar("Documentación"),
              RawType.NVarchar("Observaciones"),
              RawType.NVarchar("Numero Identificación Vehicular")*/
            });

            var dbf2 = new RawDataFile(@"C:\Temp\Javier Lopez\f231768624.mdf");
            var billeterows = RawColumnParser.BestEffortParse(dbf2.Pages.Where(x => (x.Header.ObjectID == 1790 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2312 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2313 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2314 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2315 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2316 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2318 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2319 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2320 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2321 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2322 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2323 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2324 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2325 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2327 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2328 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)
                                                                               || (x.Header.ObjectID == 2329 && x.Header.IndexID == 256 && x.Header.Type == PageType.Data)), new IRawType[] {
	            RawType.BigInt("Folio"),
	            RawType.Char("Serie",1),
	            RawType.Int("IdCasadeEmpeño"),
	            RawType.SmallInt("TipodePrestamo"),
	            RawType.NVarchar("Metododepago"),
	            RawType.NVarchar("NombreConsecutivo"),
	            RawType.Int("Foraneo"),
	            RawType.Int("IdTasadeInteres"),
	            RawType.SmallInt("RefrendoNo"),
	            RawType.DateTime("Fecha"),
	            RawType.NVarchar("TextoTipodePrestamo"),
	            RawType.NVarchar("Cuentadepago"),
	            RawType.Float("Préstamo"),
	            RawType.Float("AportaciónSocial"),
	            RawType.Float("Avalúo"),
	            RawType.NVarchar("Nombre"),
	            RawType.NVarchar("RFC"),
	            RawType.Int("IdImagen"),
	            RawType.VarBinary("Imagen"),
	            RawType.VarBinary("Firma"),
	            RawType.NVarchar("Dirección"),
	            RawType.NVarchar("Colonia"),
	            RawType.NVarchar("Municipio"),
	            RawType.NVarchar("CP"),
	            RawType.NVarchar("Comentarios"),
	            RawType.NVarchar("Sexo"),
	            RawType.NVarchar("Identificación"),
	            RawType.NVarchar("NoClientePrestaPuntos"),
	            RawType.NVarchar("NoClientePrestaPuntosanterior"),
	            RawType.NVarchar("Usuarioqueautorizaelcambio"),
	            RawType.DateTime("FechadeAltaPrestaPuntos"),
	            RawType.Float("PuntosCargados"),
	            RawType.Float("ValorMonetarioPuntosCargados"),
	            RawType.Float("PuntosUsados"),
	            RawType.Float("ValorMonetarioPuntosUsados"),
	            RawType.NVarchar("Telefonos"),
	            RawType.NVarchar("Correo"),
	            RawType.DateTime("FechadeNacimiento"),
	            RawType.NVarchar("Publicidad"),
	            RawType.NVarchar("Beneficiario"),
	            RawType.NVarchar("RFCBeneficiario"),
	            RawType.Int("IdImagenBeneficiario"),
	            RawType.VarBinary("ImagenBeneficiario"),
	            RawType.NVarchar("DirecciónBeneficiario"),
	            RawType.NVarchar("ColoniaBeneficiario"),
	            RawType.NVarchar("MunicipioBeneficiario"),
	            RawType.NVarchar("CPBeneficiario"),
	            RawType.NVarchar("ComentariosBeneficiario"),
	            RawType.NVarchar("SexoBeneficiario"),
	            RawType.NVarchar("IdentificaciónBeneficiario"),
	            RawType.NVarchar("TelefonosBeneficiario"),
	            RawType.DateTime("FechadeNacimientoBeneficiario"),
	            RawType.NVarchar("ParentescoBeneficiario"),
	            RawType.NVarchar("Cotitular"),
	            RawType.NVarchar("RFCCotitular"),
	            RawType.Int("IdImagenCotitular"),
	            RawType.VarBinary("ImagenCotitular"),
	            RawType.NVarchar("DirecciónCotitular"),
	            RawType.NVarchar("ColoniaCotitular"),
	            RawType.NVarchar("MunicipioCotitular"),
	            RawType.NVarchar("CPCotitular"),
	            RawType.NVarchar("ComentariosCotitular"),
	            RawType.NVarchar("SexoCotitular"),
	            RawType.NVarchar("IdentificaciónCotitular"),
	            RawType.NVarchar("TelefonosCotitular"),
	            RawType.DateTime("FechadeNacimientoCotitular"),
	            RawType.NVarchar("ParentescoCotitular"),
	            RawType.BigInt("FolioOriginal"),
	            RawType.Float("Tasadeinterés",24),
	            RawType.DateTime("FechadeVencimiento"),
	            RawType.Float("Intereses"),
	            RawType.Float("Liquidacióndedesempeño")/*,
	            RawType.Bit("CréditoaLargoPlazo"),
	            RawType.Float("PlazoCréditoaLargoPlazo",24),
	            RawType.Int("TipoCréditoaLargoPlazo"),
	            RawType.Int("BaseparaelcálculodeinteresesCréditoaLargoPlazo"),
	            RawType.Float("TasadeinterésCréditoaLargoPlazo",24),
	            RawType.Int("NoCréditoaLargoPlazo"),
	            RawType.Float("MontoPrimerPagoCréditoaLargoPlazo"),
	            RawType.DateTime("FechadeTerminaciónCréditoaLargoPlazo"),
	            RawType.Int("FolioAuditoria"),
	            RawType.Float("SaldoTotal"),
	            RawType.Float("SaldoCapital"),
	            RawType.Float("SaldoAportaciónSocial"),
	            RawType.Float("SaldoIntereses"),
	            RawType.Float("BonificaciónIntereses"),
	            RawType.Float("BonificaciónIva"),
	            RawType.Float("RecargosCalculados"),
	            RawType.Float("Recargos"),
	            RawType.NVarchar("Usuario"),
	            RawType.NVarchar("UsuarioOriginal"),
	            RawType.DateTime("FechaSistema"),
	            RawType.BigInt("Foliorefrendado"),
	            RawType.NVarchar("Usuario_Baja"),
	            RawType.Bit("Reportado"),
	            RawType.Bit("Boletarobada"),
	            RawType.SmallInt("RefrendoNoseguncajas"),
	            RawType.Bit("NoEnajenar"),
	            RawType.DateTime("FechaNoEnajenar"),
	            RawType.Float("PrestamoInvitado"),
	            RawType.Float("PorcientoInteresInvitado",24),
	            RawType.Float("PorcientoConcepto 1"),
	            RawType.Float("PorcientoConcepto 2"),
	            RawType.Float("PorcientoConcepto 3"),
	            RawType.Float("PorcientoConcepto 4"),
	            RawType.Float("PorcientoConcepto 5"),
	            RawType.Float("PorcientoConcepto Postventa 1"),
	            RawType.Float("PorcientoConcepto Postventa 2"),
	            RawType.Float("PorcientoConcepto Postventa 3"),
	            RawType.Float("InteresesInvitado"),
	            RawType.Float("Iva"),
	            RawType.Float("SubTotal"),
	            RawType.Int("Folio_Aux"),
	            RawType.Int("SesionActual"),
	            RawType.Int("DuracionInicial"),
	            RawType.Bit("Enajenado"),
	            RawType.DateTime("FechaEnajenación"),
	            RawType.Int("NoEjecutarDesencadenadores"),
	            RawType.Int("Socio"),
	            RawType.Bit("ReestructurarSaldos"),
	            RawType.Bit("NoValidarUsuario"),
	            RawType.NVarchar("NombreAval"),
	            RawType.NVarchar("DirecciónAval"),
	            RawType.NVarchar("ColoniaAval"),
	            RawType.NVarchar("MunicipioAval"),
	            RawType.NVarchar("IdentificacionAval"),
	            RawType.NVarchar("TelefonoAval")*/
            });

            /*
            TextWriter writer7 = File.CreateText("detallerows.txt");

            ObjectDumper.Write(
                detallerows
                    .Select(x => new
                    {   Folio = (long?)x.GetColumnValue("Folio"),
                        Serie = x.GetColumnValue("Serie"),
                        IdCasaDeEmpeño = (int?)x.GetColumnValue("IdCasadeEmpeño"),
                        TipoDePrestamo = (Int16?)x.GetColumnValue("TipodePrestamo"),
                        TextoTipoDePrestamo = x.GetColumnValue("TextoTipodePrestamo"),
                        NombreConsecutivo = x.GetColumnValue("NombreConsecutivo"),
                        FolioAuditoriaDetalle = (int?)x.GetColumnValue("FolioAuditoriaDetalle"),
                        Cantidad = (int?)x.GetColumnValue("Cantidad"),
                        SaldoCantidad = (int?)x.GetColumnValue("SaldoCantidad"),
                        CantidadSeparada = (int?)x.GetColumnValue("CantidadSeparada"),
                        Comentarios = x.GetColumnValue("Comentarios"),
                        Descripción = x.GetColumnValue("Descripción"),
                        Notas = x.GetColumnValue("Notas"),
                        IdImagen = (int?)x.GetColumnValue("IdImagen"),
                        Imagen = (Byte[])x.GetColumnValue("Imagen"),
                        Marca = x.GetColumnValue("Marca"),
                        Modelo = x.GetColumnValue("Modelo"),
                        SerieNo = x.GetColumnValue("SerieNo"),
                        Año = (Int16?)x.GetColumnValue("Año"),
                        Placa = x.GetColumnValue("Placa"),
                        Metal = (int?)x.GetColumnValue("Metal"),
                        Gramos = (double?)x.GetColumnValue("Gramos"),
                        SaldoGramos = (double?)x.GetColumnValue("SaldoGramos"),
                        GramosSeparados = (double?)x.GetColumnValue("GramosSeparados"),
                        Cotización = (double?)x.GetColumnValue("Cotización"),
                        Categoría = x.GetColumnValue("Categoría"),
                        IdProducto = (int?)x.GetColumnValue("IdProducto"),
                        IdPresentación = (int?)x.GetColumnValue("IdPresentación"),
                        Avaluo = (double?)x.GetColumnValue("Avaluo"),
                        Prestamo = (double?)x.GetColumnValue("Prestamo"),
                        Color = x.GetColumnValue("Color"),
                        Peso = (double?)x.GetColumnValue("Peso"),
                        Calidad = x.GetColumnValue("Calidad"),
                        PrestamoInvitado = (double?)x.GetColumnValue("PrestamoInvitado"),
                        SesionActual = (int?)x.GetColumnValue("SesionActual"),
                        Ubicación = x.GetColumnValue("Ubicación"),
                        Ubicación2 = x.GetColumnValue("Ubicación2"),
                        Ubicación3 = x.GetColumnValue("Ubicación3"),
                        Ubicación4 = x.GetColumnValue("Ubicación4"),
                        PreciodeVenta = (double?)x.GetColumnValue("PreciodeVenta"),
                        PrecioAjustado = (double?)x.GetColumnValue("PrecioAjustado"),
                        Vendido = (Boolean?)x.GetColumnValue("Vendido"),
                        FechaVenta = (DateTime?)x.GetColumnValue("FechaVenta"),
                        PreciodeVentaReal = (double?)x.GetColumnValue("PreciodeVentaReal"),
                        Separado = (Boolean?)x.GetColumnValue("Separado"),
                        FechaSeparación = (DateTime?)x.GetColumnValue("FechaSeparación"),
                        FechaMáximapararetirar = (DateTime?)x.GetColumnValue("FechaMáximapararetirar"),
                        MontoAbonado = (double?)x.GetColumnValue("MontoAbonado"),
                        Saldo = (double?)x.GetColumnValue("Saldo"),
                        FechaUltimoAbono = (DateTime?)x.GetColumnValue("FechaUltimoAbono"),
                        MontoUltimoAbono = (double?)x.GetColumnValue("MontoUltimoAbono"),
                        Facturado = (Boolean?)x.GetColumnValue("Facturado"),
                        FechaFactura = (DateTime?)x.GetColumnValue("FechaFactura"),
                        NúmFactura = (int?)x.GetColumnValue("NúmFactura"),
                        Nombre = x.GetColumnValue("Nombre"),
                        Dirección = x.GetColumnValue("Dirección"),
                        Identificación = x.GetColumnValue("Identificación"),
                        Telefono_s_ = x.GetColumnValue("Telefonos"),
                        RFC = x.GetColumnValue("RFC"),
                        NoEjecutarDesencadenadores = (int?)x.GetColumnValue("NoEjecutarDesencadenadores"),
                        NoValidarUsuario = (int?)x.GetColumnValue("NoValidarUsuario"),
                        Usuario =x.GetColumnValue("Usuario"),
                        Usuario_Baja = x.GetColumnValue("Usuario_Baja"),
                        rowguid = (Guid?)x.GetColumnValue("rowguid")*/
            /*,
FolioOrigen = (long?)x.GetColumnValue("Folio Origen"),
SerieOrigen = x.GetColumnValue("Serie Origen"),
IdCasadeEmpeñoOrigen = (int?)x.GetColumnValue("IdCasadeEmpeño Origen"),
TipodePrestamoOrigen = (Int16?)x.GetColumnValue("Tipo de Prestamo Origen"),
TextoTipodePrestamoOrigen = x.GetColumnValue("Texto Tipo de Prestamo Origen"),
NombreConsecutivoOrigen = x.GetColumnValue("Nombre Consecutivo Origen"),
FolioAuditoriaDetalleOrigen = (int?)x.GetColumnValue("Folio Auditoria Detalle Origen"),
PrestamoFolioOrigen = (double?)x.GetColumnValue("Prestamo Folio Origen"),
PrecioVentaFolioOrigen = (double?)x.GetColumnValue("Precio Venta Folio Origen"),
AnticipoRecibidoFolioOrigen = (double?)x.GetColumnValue("Anticipo Recibido Folio Origen"),
CantidadBloqueada = (int?)x.GetColumnValue("Cantidad Bloqueada"),
Placas = x.GetColumnValue("Placas"),
No_Motor = x.GetColumnValue("No. Motor"),
No_SerieChasis = x.GetColumnValue("No. Serie Chasis"),
No_LicenciaManejo = x.GetColumnValue("No. Licencia Manejo"),
P_G_R_Robo = x.GetColumnValue("P.G.R. Robo"),
Aseguradora = x.GetColumnValue("Aseguradora"),
No_Poliza = x.GetColumnValue("No. Poliza"),
Gasolina = x.GetColumnValue("Gasolina"),
Kilometraje = x.GetColumnValue("Kilometraje"),
Daños = x.GetColumnValue("Daños"),
NumeroFactura = x.GetColumnValue("Numero Factura"),
Emisor = x.GetColumnValue("Emisor"),
Valordelafactura = (double?)x.GetColumnValue("Valor de la factura"),
FechadelaFactura = (DateTime?)x.GetColumnValue("Fecha de la Factura"),
Instrumentostablero = (Boolean?)x.GetColumnValue("Instrumentos tablero"),
Calefaccion = (Boolean?)x.GetColumnValue("Calefaccion"),
Limpiadores = (Boolean?)x.GetColumnValue("Limpiadores"),
Radio_Tipo = (Boolean?)x.GetColumnValue("Radio/Tipo"),
Bocinas = (Boolean?)x.GetColumnValue("Bocinas"),
Encendedor = (Boolean?)x.GetColumnValue("Encendedor"),
EspejoRetrovisor = (Boolean?)x.GetColumnValue("Espejo Retrovisor"),
Cinturones = (Boolean?)x.GetColumnValue("Cinturones"),
BotonesdeInterior = (Boolean?)x.GetColumnValue("Botones de Interior"),
ManijasInteriores = (Boolean?)x.GetColumnValue("Manijas Interiores"),
Tapetes = (Boolean?)x.GetColumnValue("Tapetes"),
Vestiduras = (Boolean?)x.GetColumnValue("Vestiduras"),
FuncionalidadMotor = (Boolean?)x.GetColumnValue("Funcionalidad Motor"),
Cenicero = (Boolean?)x.GetColumnValue("Cenicero"),
Viseras = (Boolean?)x.GetColumnValue("Viseras"),
UnidaddeLuces = (Boolean?)x.GetColumnValue("Unidad de Luces"),
_1_4deLuces = (Boolean?)x.GetColumnValue("1/4 de Luces"),
EspejoLateral = (Boolean?)x.GetColumnValue("Espejo Lateral"),
Cristales = (Boolean?)x.GetColumnValue("Cristales"),
Emblema = (Boolean?)x.GetColumnValue("Emblema"),
Llantas_4_ = (Boolean?)x.GetColumnValue("Llantas(4)"),
BocinasdeClaxon = (Boolean?)x.GetColumnValue("Bocinas de Claxon"),
TapondeGasolina = (Boolean?)x.GetColumnValue("Tapon de Gasolina"),
Carrocesingolpes = (Boolean?)x.GetColumnValue("Carroce sin golpes"),
Bocinasdeaire = (Boolean?)x.GetColumnValue("Bocinas de aire"),
Defensas = (Boolean?)x.GetColumnValue("Defensas"),
Parrilla = (Boolean?)x.GetColumnValue("Parrilla"),
Rhines = (Boolean?)x.GetColumnValue("Rhines"),
Calaveras = (Boolean?)x.GetColumnValue("Calaveras"),
Molduras = (Boolean?)x.GetColumnValue("Molduras"),
Biseles = (Boolean?)x.GetColumnValue("Biseles"),
Taponesderueda = (Boolean?)x.GetColumnValue("Tapones de rueda"),
Manijasexternas = (Boolean?)x.GetColumnValue("Manijas externas"),
Gato = (Boolean?)x.GetColumnValue("Gato"),
Maneraldelgato = (Boolean?)x.GetColumnValue("Maneral del gato"),
Llavederuedas = (Boolean?)x.GetColumnValue("Llave de ruedas"),
CajaHerramientas = (Boolean?)x.GetColumnValue("Caja Herramientas"),
TrianguloSeguridad = (Boolean?)x.GetColumnValue("Triangulo Seguridad"),
Llantaderefaccion = (Boolean?)x.GetColumnValue("Llanta de refaccion"),
Extinguidor = (Boolean?)x.GetColumnValue("Extinguidor"),
TapondeAceite = (Boolean?)x.GetColumnValue("Tapon de Aceite"),
TapondeRadiador = (Boolean?)x.GetColumnValue("Tapon de Radiador"),
VarilladeAceite = (Boolean?)x.GetColumnValue("Varilla de Aceite"),
FiltrodeAire = (Boolean?)x.GetColumnValue("Filtro de Aire"),
Pasaporte = x.GetColumnValue("Pasaporte"),
Bateria = (Boolean?)x.GetColumnValue("Bateria"),
Documentación = x.GetColumnValue("Documentación"),
Observaciones = x.GetColumnValue("Observaciones"),
NumeroIdentificaciónVehicular = x.GetColumnValue("Numero Identificación Vehicular")*//*
                    }), 1, writer7
            ); */

            insertBilletesdeEmpeñoRows(billeterows);
            insertBilletesdeEmpeñoDetalleRows(detallerows);
        }
    }
}
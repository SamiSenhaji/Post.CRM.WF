using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.CRM.WF.MODEL.SAP
{
   public class quote_
    {

        public ORDERS05 ORDERS05 { get; set; }
    }

    public class ORDERS05
    {
        public IDOC IDOC { get; set; }
    }

    public class IDOC
    {
        public string BEGIN { get; set; }
        public EDI_DC40 EDI_DC40 { get; set; }
        public E1EDK01 E1EDK01 { get; set; }
        public E1EDK14[] E1EDK14 { get; set; }
        public E1EDK03[] E1EDK03 { get; set; }
        public E1EDK04 E1EDK04 { get; set; }
        public E1EDK05 E1EDK05 { get; set; }
        public E1EDKA1[] E1EDKA1 { get; set; }
        public E1EDK02 E1EDK02 { get; set; }
        public E1EDK17[] E1EDK17 { get; set; }
        public E1EDK18 E1EDK18 { get; set; }
        public E1EDP01 E1EDP01 { get; set; }
        public E1EDS01[] E1EDS01 { get; set; }
    }

    public class EDI_DC40
    {
        public string SEGMENT { get; set; }
        public string TABNAM { get; set; }
        public string MANDT { get; set; }
        public string DOCNUM { get; set; }
        public string DOCREL { get; set; }
        public string STATUS { get; set; }
        public string DIRECT { get; set; }
        public string OUTMOD { get; set; }
        public string IDOCTYP { get; set; }
        public string MESTYP { get; set; }
        public string SNDPOR { get; set; }
        public string SNDPRT { get; set; }
        public string SNDPRN { get; set; }
        public string RCVPOR { get; set; }
        public string RCVPRT { get; set; }
        public string RCVPFC { get; set; }
        public string RCVPRN { get; set; }
        public string CREDAT { get; set; }
        public string CRETIM { get; set; }
        public string SERIAL { get; set; }
    }

    public class E1EDK01
    {
        public string SEGMENT { get; set; }
        public string CURCY { get; set; }
        public string WKURS { get; set; }
        public string ZTERM { get; set; }
        public string BELNR { get; set; }
        public string VSART { get; set; }
        public string VSART_BEZ { get; set; }
        public string RECIPNT_NO { get; set; }
    }

    public class E1EDK04
    {
        public string SEGMENT { get; set; }
        public string MWSKZ { get; set; }
        public string MSATZ { get; set; }
        public string MWSBT { get; set; }
    }

    public class E1EDK05
    {
        public string SEGMENT { get; set; }
        public string ALCKZ { get; set; }
        public string KOTXT { get; set; }
        public string BETRG { get; set; }
        public string KRATE { get; set; }
        public string KOEIN { get; set; }
    }

    public class E1EDK02
    {
        public string SEGMENT { get; set; }
        public string QUALF { get; set; }
        public string BELNR { get; set; }
        public string DATUM { get; set; }
    }

    public class E1EDK18
    {
        public string SEGMENT { get; set; }
        public string QUALF { get; set; }
        public string TAGE { get; set; }
        public string PRZNT { get; set; }
        public string ZTERM_TXT { get; set; }
    }

    public class E1EDP01
    {
        public string SEGMENT { get; set; }
        public string POSEX { get; set; }
        public string MENGE { get; set; }
        public string MENEE { get; set; }
        public string VPREI { get; set; }
        public string PEINH { get; set; }
        public string NETWR { get; set; }
        public string CURCY { get; set; }
        public string MATKL { get; set; }
        public string PSTYV { get; set; }
        public string WERKS { get; set; }
        public string VSTEL { get; set; }
        public E1EDP03[] E1EDP03 { get; set; }
        public E1EDP05 E1EDP05 { get; set; }
        public E1EDP20 E1EDP20 { get; set; }
        public E1EDP19 E1EDP19 { get; set; }
    }

    public class E1EDP05
    {
        public string SEGMENT { get; set; }
        public string ALCKZ { get; set; }
        public string KOTXT { get; set; }
        public string BETRG { get; set; }
        public string KRATE { get; set; }
        public string UPRBS { get; set; }
        public string MEAUN { get; set; }
        public string KOEIN { get; set; }
    }

    public class E1EDP20
    {
        public string SEGMENT { get; set; }
        public string WMENG { get; set; }
        public string EDATU { get; set; }
    }

    public class E1EDP19
    {
        public string SEGMENT { get; set; }
        public string QUALF { get; set; }
        public string IDTNR { get; set; }
        public string KTEXT { get; set; }
    }

    public class E1EDP03
    {
        public string SEGMENT { get; set; }
        public string IDDAT { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
    }

    public class E1EDK14
    {
        public string SEGMENT { get; set; }
        public string QUALF { get; set; }
        public string ORGID { get; set; }
    }

    public class E1EDK03
    {
        public string SEGMENT { get; set; }
        public string IDDAT { get; set; }
        public string DATUM { get; set; }
        public string UZEIT { get; set; }
    }

    public class E1EDKA1
    {
        public string SEGMENT { get; set; }
        public string PARVW { get; set; }
        public string PARTN { get; set; }
        public string NAME1 { get; set; }
        public string STRAS { get; set; }
        public string ORT01 { get; set; }
        public string PSTLZ { get; set; }
        public string LAND1 { get; set; }
        public string SPRAS { get; set; }
        public string HAUSN { get; set; }
        public string SPRAS_ISO { get; set; }
    }

    public class E1EDK17
    {
        public string SEGMENT { get; set; }
        public string QUALF { get; set; }
        public string LKOND { get; set; }
        public string LKTEXT { get; set; }
    }

    public class E1EDS01
    {
        public string SEGMENT { get; set; }
        public string SUMID { get; set; }
        public string SUMME { get; set; }
        public string SUNIT { get; set; }
    }

}


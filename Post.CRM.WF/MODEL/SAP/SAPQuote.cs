using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Post.CRM.WF.MODEL.SAP.Quotes
{
	[XmlRoot(ElementName = "EDI_DC40")]
	public class EDI_DC40
	{
		[XmlElement(ElementName = "TABNAM")]
		public string TABNAM { get; set; }
		[XmlElement(ElementName = "MANDT")]
		public string MANDT { get; set; }
		[XmlElement(ElementName = "DOCNUM")]
		public string DOCNUM { get; set; }
		[XmlElement(ElementName = "DOCREL")]
		public string DOCREL { get; set; }
		[XmlElement(ElementName = "STATUS")]
		public string STATUS { get; set; }
		[XmlElement(ElementName = "DIRECT")]
		public string DIRECT { get; set; }
		[XmlElement(ElementName = "OUTMOD")]
		public string OUTMOD { get; set; }
		[XmlElement(ElementName = "IDOCTYP")]
		public string IDOCTYP { get; set; }
		[XmlElement(ElementName = "MESTYP")]
		public string MESTYP { get; set; }
		[XmlElement(ElementName = "SNDPOR")]
		public string SNDPOR { get; set; }
		[XmlElement(ElementName = "SNDPRT")]
		public string SNDPRT { get; set; }
		[XmlElement(ElementName = "SNDPRN")]
		public string SNDPRN { get; set; }
		[XmlElement(ElementName = "RCVPOR")]
		public string RCVPOR { get; set; }
		[XmlElement(ElementName = "RCVPRT")]
		public string RCVPRT { get; set; }
		[XmlElement(ElementName = "RCVPFC")]
		public string RCVPFC { get; set; }
		[XmlElement(ElementName = "RCVPRN")]
		public string RCVPRN { get; set; }
		[XmlElement(ElementName = "CREDAT")]
		public string CREDAT { get; set; }
		[XmlElement(ElementName = "CRETIM")]
		public string CRETIM { get; set; }
		[XmlElement(ElementName = "SERIAL")]
		public string SERIAL { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK01")]
	public class E1EDK01
	{
		[XmlElement(ElementName = "CURCY")]
		public string CURCY { get; set; }
		[XmlElement(ElementName = "WKURS")]
		public string WKURS { get; set; }
		[XmlElement(ElementName = "ZTERM")]
		public string ZTERM { get; set; }
		[XmlElement(ElementName = "BELNR")]
		public string BELNR { get; set; }
		[XmlElement(ElementName = "VSART")]
		public string VSART { get; set; }
		[XmlElement(ElementName = "VSART_BEZ")]
		public string VSART_BEZ { get; set; }
		[XmlElement(ElementName = "RECIPNT_NO")]
		public string RECIPNT_NO { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK14")]
	public class E1EDK14
	{
		[XmlElement(ElementName = "QUALF")]
		public string QUALF { get; set; }
		[XmlElement(ElementName = "ORGID")]
		public string ORGID { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK03")]
	public class E1EDK03
	{
		[XmlElement(ElementName = "IDDAT")]
		public string IDDAT { get; set; }
		[XmlElement(ElementName = "DATUM")]
		public string DATUM { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
		[XmlElement(ElementName = "UZEIT")]
		public string UZEIT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK04")]
	public class E1EDK04
	{
		[XmlElement(ElementName = "MWSKZ")]
		public string MWSKZ { get; set; }
		[XmlElement(ElementName = "MSATZ")]
		public string MSATZ { get; set; }
		[XmlElement(ElementName = "MWSBT")]
		public string MWSBT { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK05")]
	public class E1EDK05
	{
		[XmlElement(ElementName = "ALCKZ")]
		public string ALCKZ { get; set; }
		[XmlElement(ElementName = "KOTXT")]
		public string KOTXT { get; set; }
		[XmlElement(ElementName = "BETRG")]
		public string BETRG { get; set; }
		[XmlElement(ElementName = "KRATE")]
		public string KRATE { get; set; }
		[XmlElement(ElementName = "KOEIN")]
		public string KOEIN { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDKA1")]
	public class E1EDKA1
	{
		[XmlElement(ElementName = "PARVW")]
		public string PARVW { get; set; }
		[XmlElement(ElementName = "PARTN")]
		public string PARTN { get; set; }
		[XmlElement(ElementName = "NAME1")]
		public string NAME1 { get; set; }
		[XmlElement(ElementName = "STRAS")]
		public string STRAS { get; set; }
		[XmlElement(ElementName = "ORT01")]
		public string ORT01 { get; set; }
		[XmlElement(ElementName = "PSTLZ")]
		public string PSTLZ { get; set; }
		[XmlElement(ElementName = "LAND1")]
		public string LAND1 { get; set; }
		[XmlElement(ElementName = "SPRAS")]
		public string SPRAS { get; set; }
		[XmlElement(ElementName = "HAUSN")]
		public string HAUSN { get; set; }
		[XmlElement(ElementName = "SPRAS_ISO")]
		public string SPRAS_ISO { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK02")]
	public class E1EDK02
	{
		[XmlElement(ElementName = "QUALF")]
		public string QUALF { get; set; }
		[XmlElement(ElementName = "BELNR")]
		public string BELNR { get; set; }
		[XmlElement(ElementName = "DATUM")]
		public string DATUM { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK17")]
	public class E1EDK17
	{
		[XmlElement(ElementName = "QUALF")]
		public string QUALF { get; set; }
		[XmlElement(ElementName = "LKOND")]
		public string LKOND { get; set; }
		[XmlElement(ElementName = "LKTEXT")]
		public string LKTEXT { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDK18")]
	public class E1EDK18
	{
		[XmlElement(ElementName = "QUALF")]
		public string QUALF { get; set; }
		[XmlElement(ElementName = "TAGE")]
		public string TAGE { get; set; }
		[XmlElement(ElementName = "PRZNT")]
		public string PRZNT { get; set; }
		[XmlElement(ElementName = "ZTERM_TXT")]
		public string ZTERM_TXT { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDP03")]
	public class E1EDP03
	{
		[XmlElement(ElementName = "IDDAT")]
		public string IDDAT { get; set; }
		[XmlElement(ElementName = "DATUM")]
		public string DATUM { get; set; }
		[XmlElement(ElementName = "UZEIT")]
		public string UZEIT { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDP05")]
	public class E1EDP05
	{
		[XmlElement(ElementName = "ALCKZ")]
		public string ALCKZ { get; set; }
		[XmlElement(ElementName = "KOTXT")]
		public string KOTXT { get; set; }
		[XmlElement(ElementName = "BETRG")]
		public string BETRG { get; set; }
		[XmlElement(ElementName = "KRATE")]
		public string KRATE { get; set; }
		[XmlElement(ElementName = "UPRBS")]
		public string UPRBS { get; set; }
		[XmlElement(ElementName = "MEAUN")]
		public string MEAUN { get; set; }
		[XmlElement(ElementName = "KOEIN")]
		public string KOEIN { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDP20")]
	public class E1EDP20
	{
		[XmlElement(ElementName = "WMENG")]
		public string WMENG { get; set; }
		[XmlElement(ElementName = "EDATU")]
		public string EDATU { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDP19")]
	public class E1EDP19
	{
		[XmlElement(ElementName = "QUALF")]
		public string QUALF { get; set; }
		[XmlElement(ElementName = "IDTNR")]
		public string IDTNR { get; set; }
		[XmlElement(ElementName = "KTEXT")]
		public string KTEXT { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDP01")]
	public class E1EDP01
	{
		[XmlElement(ElementName = "POSEX")]
		public string POSEX { get; set; }
		[XmlElement(ElementName = "MENGE")]
		public string MENGE { get; set; }
		[XmlElement(ElementName = "MENEE")]
		public string MENEE { get; set; }
		[XmlElement(ElementName = "VPREI")]
		public string VPREI { get; set; }
		[XmlElement(ElementName = "PEINH")]
		public string PEINH { get; set; }
		[XmlElement(ElementName = "NETWR")]
		public string NETWR { get; set; }
		[XmlElement(ElementName = "CURCY")]
		public string CURCY { get; set; }
		[XmlElement(ElementName = "MATKL")]
		public string MATKL { get; set; }
		[XmlElement(ElementName = "PSTYV")]
		public string PSTYV { get; set; }
		[XmlElement(ElementName = "WERKS")]
		public string WERKS { get; set; }
		[XmlElement(ElementName = "VSTEL")]
		public string VSTEL { get; set; }
		[XmlElement(ElementName = "E1EDP03")]
		public List<E1EDP03> E1EDP03 { get; set; }
		[XmlElement(ElementName = "E1EDP05")]
		public E1EDP05 E1EDP05 { get; set; }
		[XmlElement(ElementName = "E1EDP20")]
		public E1EDP20 E1EDP20 { get; set; }
		[XmlElement(ElementName = "E1EDP19")]
		public E1EDP19 E1EDP19 { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
	}

	[XmlRoot(ElementName = "E1EDS01")]
	public class E1EDS01
	{
		[XmlElement(ElementName = "SUMID")]
		public string SUMID { get; set; }
		[XmlElement(ElementName = "SUMME")]
		public string SUMME { get; set; }
		[XmlAttribute(AttributeName = "SEGMENT")]
		public string SEGMENT { get; set; }
		[XmlElement(ElementName = "SUNIT")]
		public string SUNIT { get; set; }
	}

	[XmlRoot(ElementName = "IDOC")]
	public class IDOC
	{
		[XmlElement(ElementName = "EDI_DC40")]
		public EDI_DC40 EDI_DC40 { get; set; }
		[XmlElement(ElementName = "E1EDK01")]
		public E1EDK01 E1EDK01 { get; set; }
		[XmlElement(ElementName = "E1EDK14")]
		public List<E1EDK14> E1EDK14 { get; set; }
		[XmlElement(ElementName = "E1EDK03")]
		public List<E1EDK03> E1EDK03 { get; set; }
		[XmlElement(ElementName = "E1EDK04")]
		public E1EDK04 E1EDK04 { get; set; }
		[XmlElement(ElementName = "E1EDK05")]
		public E1EDK05 E1EDK05 { get; set; }
		[XmlElement(ElementName = "E1EDKA1")]
		public List<E1EDKA1> E1EDKA1 { get; set; }
		[XmlElement(ElementName = "E1EDK02")]
		public E1EDK02 E1EDK02 { get; set; }
		[XmlElement(ElementName = "E1EDK17")]
		public List<E1EDK17> E1EDK17 { get; set; }
		[XmlElement(ElementName = "E1EDK18")]
		public E1EDK18 E1EDK18 { get; set; }
		[XmlElement(ElementName = "E1EDP01")]
		public E1EDP01 E1EDP01 { get; set; }
		[XmlElement(ElementName = "E1EDS01")]
		public List<E1EDS01> E1EDS01 { get; set; }
		[XmlAttribute(AttributeName = "BEGIN")]
		public string BEGIN { get; set; }
	}

	[XmlRoot(ElementName = "ORDERS05")]
	public class ORDERS05
	{
		[XmlElement(ElementName = "IDOC")]
		public IDOC IDOC { get; set; }
	}
}